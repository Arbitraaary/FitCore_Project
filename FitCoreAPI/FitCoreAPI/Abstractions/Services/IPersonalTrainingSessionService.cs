using FitCore_API.DTOs;
using FitCoreAPI.DTOs;

namespace FitCore_API.Abstractions.Services;

public interface IPersonalTrainingSessionService
{
    Task<List<PersonalSessionDto>> GetPersonalSessionsByClientIdAsync(Guid clientId, CancellationToken ct);
    Task<PersonalSessionDto?> GetPersonalSessionById(Guid id, CancellationToken ct);
    Task<List<PersonalSessionDto>> GetPersonalSessionsByCoachIdAsync(Guid coachId, CancellationToken ct);
    Task<List<PersonalSessionDto>> GetPersonalSessionByLocation(string locationName, CancellationToken ct);
    Task CreatePersonalSession(CreatePersonalSessionDto dto, CancellationToken ct);
}