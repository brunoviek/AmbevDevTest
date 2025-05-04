using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a Cart is deleted
    /// </summary>
    public class CartDeletedEventHandler : IHandleMessages<CartDeletedEvent>
    {
        public Task Handle(CartDeletedEvent message)
        {

            Console.WriteLine($"Cart deleted: {message.Cart.Id}");

            return Task.CompletedTask;
        }
    }
}
