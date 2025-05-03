using Ambev.DeveloperEvaluation.Application.Users.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Consumers
{
    /// <summary>
    /// Event handler when a Products is registered
    /// </summary>
    public class UserCreatedEventHandler : IHandleMessages<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent message)
        {

            Console.WriteLine($"Products created: {message.User.Username}");

            return Task.CompletedTask;
        }
    }
}
