using FitCore_API.Abstractions.Services;
using FitCoreAPI.Abstractions.Repositories;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;

namespace FitCoreAPI.Services;

public class GroupTrainingSessionService: IGroupTrainingSessionService
{
    private readonly IGroupTrainingSessionRepository _groupTrainingSessionRepository;

    public GroupTrainingSessionService(IGroupTrainingSessionRepository groupTrainingSessionRepository)
    {
        _groupTrainingSessionRepository = groupTrainingSessionRepository;
    }

    public async Task<List<GroupSessionDto>> GetAllGroupSessions(CancellationToken ct)
    {
        var groupSessions = await _groupTrainingSessionRepository.GetAllAsync(ct);
        return groupSessions.Select(groupSession => new GroupSessionDto(
            groupSession.Id,
            groupSession.CoachId,
            groupSession.RoomId,
            groupSession.Name,
            groupSession.Description,
            groupSession.Capacity,
            "Group",
            groupSession.StartTime,
            groupSession.EndTime,
            []
        )).ToList();
    }

    public async Task<GroupSessionDto?> GetGroupSessionById(Guid id, CancellationToken ct)
    {
        var groupSession = await _groupTrainingSessionRepository.GetByIdAsync(id, ct);
        if(groupSession == null) return null;
        return new GroupSessionDto(
            groupSession.Id,
            groupSession.CoachId,
            groupSession.RoomId,
            groupSession.Name,
            groupSession.Description,
            groupSession.Capacity,
            "Group",
            groupSession.StartTime,
            groupSession.EndTime,
            groupSession.ClientGroupSessions.Select(client => client.ClientId).ToList()
        );
    }

    public async Task<List<GroupSessionDto>> GetGroupSessionsByClientIdAsync(Guid clientId, CancellationToken ct)
    {
        var groupSessions = await _groupTrainingSessionRepository.GetByClientIdAsync(clientId, ct);
        return groupSessions.Select(groupSession => new GroupSessionDto(
            groupSession.Id,
            groupSession.CoachId,
            groupSession.RoomId,
            groupSession.Name,
            groupSession.Description,
            groupSession.Capacity,
            "Group",
            groupSession.StartTime,
            groupSession.EndTime,
            groupSession.ClientGroupSessions.Select(client => client.ClientId).ToList()
        )).ToList();
    }

    public async Task<List<GroupSessionDto>> GetGroupSessionsByCoachIdAsync(Guid coachId, CancellationToken ct)
    {
        var groupSessions = await _groupTrainingSessionRepository.GetByCoachIdAsync(coachId, ct);
        return groupSessions.Select(groupSession => new GroupSessionDto(
            groupSession.Id,
            groupSession.CoachId,
            groupSession.RoomId,
            groupSession.Name,
            groupSession.Description,
            groupSession.Capacity,
            "Group",
            groupSession.StartTime,
            groupSession.EndTime,
            groupSession.ClientGroupSessions.Select(client => client.ClientId).ToList()
        )).ToList();
    }

    public async Task<List<GroupSessionDto>> GetGroupSessionByLocation(string locationName, CancellationToken ct)
    {
        var sessions = await _groupTrainingSessionRepository.GetByLocationAsync(locationName, ct);
        return sessions.Select(s => new GroupSessionDto(
            s.Id,
            s.CoachId,
            s.RoomId,
            s.Name,
            s.Description,
            s.Capacity,
            "Group",
            s.StartTime,
            s.EndTime,
            s.ClientGroupSessions.Select(client => client.ClientId).ToList()
            )).ToList();
    }

    public async Task<bool> IsGroupFullAsync(Guid sessionId, CancellationToken ct)
    {
        //додати перевірку чи сесія ще не минула
        return await _groupTrainingSessionRepository.IsFullAsync(sessionId, ct);
    }
}