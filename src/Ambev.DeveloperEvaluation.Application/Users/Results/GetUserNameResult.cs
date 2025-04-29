namespace Ambev.DeveloperEvaluation.Application.Users.Results
{
    /// <summary>
    /// Represents the name details (firstname and lastname) of the user.
    /// </summary>
    public class GetUserNameResult
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
    }
}
