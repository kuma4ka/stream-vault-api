namespace StreamVault.Domain.Constants;

public static class UserRoles
{
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";
    public const string Creator = "Creator";
    public const string User = "User";

    private static readonly HashSet<string> _allRoles = [Admin, Moderator, Creator, User];
    public static bool IsValid(string role) => _allRoles.Contains(role);
}