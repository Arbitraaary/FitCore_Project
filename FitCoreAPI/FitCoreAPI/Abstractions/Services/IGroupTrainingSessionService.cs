using FitCoreAPI.DTOs;

namespace FitCoreAPI.Abstractions.Services;

public interface IGroupTrainingSessionService
{
    public Task<List<GroupSessionDto>> GetAllGroupSessions(CancellationToken ct);
    public Task<GroupSessionDto?> GetGroupSessionById(Guid id, CancellationToken ct);
    Task<List<GroupSessionDto>> GetGroupSessionsByClientIdAsync(Guid clientId, CancellationToken ct);
    Task<List<GroupSessionDto>> GetGroupSessionsByCoachIdAsync(Guid coachId, CancellationToken ct);
    Task<List<GroupSessionDto>> GetGroupSessionByLocation(string locationName, CancellationToken ct);
    Task<bool> IsGroupFullAsync(Guid sessionId, CancellationToken ct);
    Task<List<GroupSessionWithCoachAndRoomDto>> GetAllGroupSessionWithCoachAndRoom(CancellationToken ct);
    Task EnrollClient(CreateGroupEnrollmentDto dto, CancellationToken ct);
    Task<List<GroupSessionWithCoachAndRoomDto>> GetAllGroupSessionWithCoachAndRoomById(Guid id, CancellationToken ct);
}