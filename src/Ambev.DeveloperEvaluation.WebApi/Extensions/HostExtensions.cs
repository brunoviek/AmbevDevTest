using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Seed;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        /// Applies any pending database migrations and seeds initial data.
        /// </summary>
        public static async Task ApplyMigrationsAndSeedAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            await context.Database.MigrateAsync();

            var seeder = scope.ServiceProvider.GetRequiredService<DataSeederService>();
            await seeder.SeedAsync();
        }
    }
}
