using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a Cart is updated
    /// </summary>
    public class CartUpdatedEventHandler : IHandleMessages<CartUpdatedEvent>
    {
        public Task Handle(CartUpdatedEvent message)
        {

            Console.WriteLine($"Cart updated: {message.Cart.Id}");

            return Task.CompletedTask;
        }
    }
}
