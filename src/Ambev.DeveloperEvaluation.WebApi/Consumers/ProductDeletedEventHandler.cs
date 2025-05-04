using Ambev.DeveloperEvaluation.Application.Products.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a product is deleted
    /// </summary>
    public class ProductDeletedEventHandler : IHandleMessages<ProductDeletedEvent>
    {
        public Task Handle(ProductDeletedEvent message)
        {
            Console.WriteLine($"Product deleted: {message.Product.Title}");

            return Task.CompletedTask;
        }
    }
}
