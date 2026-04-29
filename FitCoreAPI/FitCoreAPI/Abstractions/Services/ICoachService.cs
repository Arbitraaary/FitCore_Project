using FitCoreAPI.DTOs;

namespace FitCoreAPI.Abstractions.Services;

public interface ICoachService
{
    public Task<List<CoachDto>> GetAllCoaches(CancellationToken ct);
    public Task<CoachDto?> GetCoachById(Guid id, CancellationToken ct);
    public Task<List<CoachDto>> GetAllCoachesByLocation(string locationName, CancellationToken ct);
    Task<List<CoachWithSessionCountDto>> GetAllCoachesByLocationWithSessionCount(string locationName, CancellationToken ct);
}