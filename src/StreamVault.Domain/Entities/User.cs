using Microsoft.AspNetCore.Identity;

namespace StreamVault.Domain.Entities;

public class User : IdentityUser
{
    public ICollection<Broadcast>? Broadcasts { get; init; }
}