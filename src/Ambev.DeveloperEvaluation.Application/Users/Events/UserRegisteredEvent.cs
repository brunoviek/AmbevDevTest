using Ambev.DeveloperEvaluation.Domain.Entities.User;

namespace Ambev.DeveloperEvaluation.Application.Users.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
