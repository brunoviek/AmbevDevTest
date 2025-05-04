using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Ambev.DeveloperEvaluation.Application.Products.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a product is created
    /// </summary>
    public class ProductCreatedEventHandler : IHandleMessages<ProductCreatedEvent>
    {
        public Task Handle(ProductCreatedEvent message)
        {

            Console.WriteLine($"Product created: {message.Product.Title}");

            return Task.CompletedTask;
        }
    }
}
