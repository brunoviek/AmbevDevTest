namespace Ambev.DeveloperEvaluation.Domain.Entities.User;

/// <summary>
/// Represents the name of a user, including first name and last name.
/// </summary>
public class Name
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string Lastname { get; set; } = string.Empty;

    /// <summary>
    /// Returns the full name.
    /// </summary>
    public override string ToString()
    {
        return $"{Firstname} {Lastname}";
    }
}
