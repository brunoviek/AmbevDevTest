using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Ambev.DeveloperEvaluation.Application.Users.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a Products is registered
    /// </summary>
    public class CartCreatedEventHandler : IHandleMessages<CartCreatedEvent>
    {
        public Task Handle(CartCreatedEvent message)
        {

            Console.WriteLine($"Cart created: {message.Cart.Id}");

            return Task.CompletedTask;
        }
    }
}
