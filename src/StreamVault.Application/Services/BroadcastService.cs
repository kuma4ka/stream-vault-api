using Microsoft.AspNetCore.Identity;
using StreamVault.Application.DTOs;
using StreamVault.Application.Interfaces.Repositories;
using StreamVault.Application.Interfaces.Services;
using StreamVault.Domain.Entities;

namespace StreamVault.Application.Services;

public class BroadcastService(
    IBroadcastRepository broadcastRepository,
    ICategoryRepository categoryRepository,
    UserManager<User> userManager 
    ) : IBroadcastService
{
    public async Task<BroadcastDto> CreateBroadcastAsync(string creatorId, CreateBroadcastDto dto)
    {
        var user = await userManager.FindByIdAsync(creatorId);
        if (user is null)
        {
            throw new Exception($"User with ID {creatorId} not found");
        }

        var categories = await categoryRepository.GetByIdsAsync(dto.CategoryIds);
        if (categories.Count != dto.CategoryIds.Count)
        {
            throw new ArgumentException("One or more category IDs are invalid");
        }

        var broadcast = new Broadcast
        {
            Id = Guid.NewGuid(),
            BroadcastTitle = dto.Title,
            BroadcastLink = dto.Link,
            CreatorId = creatorId,
            Creator = user,
            IsLive = true
        };

        foreach (var category in categories)
        {
            broadcast.BroadcastCategories.Add(new BroadcastCategory
            {
                BroadcastId = broadcast.Id,
                CategoryId = category.Id
            });
        }

        await broadcastRepository.AddAsync(broadcast);

        return new BroadcastDto(
            broadcast.Id,
            broadcast.BroadcastTitle,
            broadcast.BroadcastLink,
            broadcast.IsLive,
            broadcast.ViewCount,
            categories.Select(c => c.Name).ToList(),
            broadcast.CreatedAt
        );
    }
}