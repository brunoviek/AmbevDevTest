using Ambev.DeveloperEvaluation.Application.Users.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a Product is registered
    /// </summary>
    public class UserCreatedEventHandler : IHandleMessages<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent message)
        {

            Console.WriteLine($"Product created: {message.User.Username}");

            return Task.CompletedTask;
        }
    }
}
