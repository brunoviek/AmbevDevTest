using Ambev.DeveloperEvaluation.Domain.Entities.Users;

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
