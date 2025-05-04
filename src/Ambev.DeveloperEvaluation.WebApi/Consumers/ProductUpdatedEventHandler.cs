using Ambev.DeveloperEvaluation.Application.Products.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a product is deleted
    /// </summary>
    public class ProductUpdatedEventHandler : IHandleMessages<ProductUpdatedEvent>
    {
        public Task Handle(ProductUpdatedEvent message)
        {
            Console.WriteLine($"Product updated: {message.Product.Title}");

            return Task.CompletedTask;
        }
    }
}
