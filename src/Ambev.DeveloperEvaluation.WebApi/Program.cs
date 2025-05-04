using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Ambev.DeveloperEvaluation.Application.Products.Events;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.Events;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Consumers;
using Ambev.DeveloperEvaluation.WebApi.Extensions;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using Ambev.DeveloperEvaluation.WebApi.Seed;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Serilog;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                c.OperationFilter<DynamicFilterOperationFilter>();
            });

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions.ConfigureDefaults()
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            var rabbitMqConnectionString = builder.Configuration["RabbitMq:ConnectionString"];
            var rabbitMqInputQueue = builder.Configuration["RabbitMq:InputQueue"];

            builder.Services.AutoRegisterHandlersFromAssemblyOf<UserCreatedEventHandler>();

            builder.Services.AddRebus(
                configure => configure
                    .Transport(t => t.UseRabbitMq(rabbitMqConnectionString, rabbitMqInputQueue))
                    .Logging(l => l.Console()),
                onCreated: async bus => {
                    await bus.Subscribe<CartCreatedEvent>();
                    await bus.Subscribe<CartDeletedEvent>();
                    await bus.Subscribe<CartUpdatedEvent>();
                    await bus.Subscribe<ProductCreatedEvent>();
                    await bus.Subscribe<ProductDeletedEvent>();
                    await bus.Subscribe<ProductUpdatedEvent>();
                    await bus.Subscribe<UserRegisteredEvent>();
                }
            );

            builder.Services.AddTransient<DataSeederService>();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            var appAssembly = typeof(DeleteUserValidator).Assembly;
            builder.Services.AddValidatorsFromAssembly(appAssembly);

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                await app.ApplyMigrationsAndSeedAsync();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

}
