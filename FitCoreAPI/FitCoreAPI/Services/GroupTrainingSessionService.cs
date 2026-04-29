using FitCore_API.Abstractions.Services;
using FitCoreAPI.Abstractions.Repositories;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using FitCoreAPI.Entities.Auxiliary;

namespace FitCoreAPI.Services;

public class GroupTrainingSessionService: IGroupTrainingSessionService
{
    private readonly IGroupTrainingSessionRepository _groupTrainingSessionRepository;
    private readonly IGroupTrainingSessionClientRepository _groupTrainingSessionClientRepository;

    public GroupTrainingSessionService(IGroupTrainingSessionRepository groupTrainingSessionRepository, IGroupTrainingSessionClientRepository groupTrainingSessionClientRepository)
    {
        _groupTrainingSessionRepository = groupTrainingSessionRepository;
        _groupTrainingSessionClientRepository = groupTrainingSessionClientRepository;
    }

    public async Task<List<GroupSessionDto>> GetAllGroupSessions(CancellationToken ct)
    {
        var groupSessions = await _groupTrainingSessionRepository.GetAllAsync(ct);
        return groupSessions.Select(groupSession => new GroupSessionDto(
            groupSession.Id,
            groupSession.CoachId,
            $"{groupSession.Coach.User.FirstName} {groupSession.Coach.User.LastName}", 
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

    public async Task<GroupSessionDto?> GetGroupSessionById(Guid id, CancellationToken ct)
    {
        var groupSession = await _groupTrainingSessionRepository.GetByIdAsync(id, ct);
        if(groupSession == null) return null;
        return new GroupSessionDto(
            groupSession.Id,
            groupSession.CoachId,
            $"{groupSession.Coach.User.FirstName} {groupSession.Coach.User.LastName}", 
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
            $"{groupSession.Coach.User.FirstName} {groupSession.Coach.User.LastName}", 
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
            $"{groupSession.Coach.User.FirstName} {groupSession.Coach.User.LastName}", 
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
        return sessions.Select(groupSession => new GroupSessionDto(
            groupSession.Id,
            groupSession.CoachId,
            $"{groupSession.Coach.User.FirstName} {groupSession.Coach.User.LastName}", 
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

    public async Task<bool> IsGroupFullAsync(Guid sessionId, CancellationToken ct)
    {
        //додати перевірку чи сесія ще не минула
        return await _groupTrainingSessionRepository.IsFullAsync(sessionId, ct);
    }

    public async Task<List<GroupSessionWithCoachAndRoomDto>> GetAllGroupSessionWithCoachAndRoom(CancellationToken ct)
    {
        var groupSessions = await _groupTrainingSessionRepository.GetAllWithCoachAndRoom(ct);
        return groupSessions.Select(groupSession =>
        {
            var enrolledClients = groupSession.ClientGroupSessions.Select(client => client.ClientId).ToList();
            var locationModel = groupSession.Coach.Location;
            var locationDto = new LocationDto(
                locationModel.Name,
                locationModel.Address
            );

            var coachModel = groupSession.Coach;
            var coachDto = new CoachDto(
                coachModel.UserId,
                coachModel.User.FirstName,
                coachModel.User.LastName,
                coachModel.User.Email,
                coachModel.User.PhoneNumber,
                coachModel.User.UserType.ToString(),
                coachModel.Specialization.ToString(),
                locationDto
            );

            var roomModel = groupSession.Room;
            var roomDto = new RoomDto(
                roomModel.Id,
                roomModel.LocationName,
                roomModel.RoomType.ToString(),
                roomModel.Capacity
            );

            return new GroupSessionWithCoachAndRoomDto(
                groupSession.Id,
                groupSession.CoachId,
                $"{coachDto.FirstName} {coachDto.LastName}",
                groupSession.RoomId,
                groupSession.Name,
                groupSession.Description,
                groupSession.Capacity,
                "Group",
                groupSession.StartTime,
                groupSession.EndTime,
                enrolledClients,
                coachDto,
                roomDto);
        }).ToList();
    }

    public async Task<List<GroupSessionWithCoachAndRoomDto>> GetAllGroupSessionWithCoachAndRoomById(Guid id, CancellationToken ct)
    {
        var groupSessions = await _groupTrainingSessionRepository.GetAllWithCoachAndRoomById(id, ct);
        return groupSessions.Select(groupSession =>
        {
            var enrolledClients = groupSession.ClientGroupSessions.Select(client => client.ClientId).ToList();
            var locationModel = groupSession.Coach.Location;
            var locationDto = new LocationDto(
                locationModel.Name,
                locationModel.Address
            );

            var coachModel = groupSession.Coach;
            var coachDto = new CoachDto(
                coachModel.UserId,
                coachModel.User.FirstName,
                coachModel.User.LastName,
                coachModel.User.Email,
                coachModel.User.PhoneNumber,
                coachModel.User.UserType.ToString(),
                coachModel.Specialization.ToString(),
                locationDto
            );

            var roomModel = groupSession.Room;
            var roomDto = new RoomDto(
                roomModel.Id,
                roomModel.LocationName,
                roomModel.RoomType.ToString(),
                roomModel.Capacity
            );

            return new GroupSessionWithCoachAndRoomDto(
                groupSession.Id,
                groupSession.CoachId,
                $"{coachDto.FirstName} {coachDto.LastName}",
                groupSession.RoomId,
                groupSession.Name,
                groupSession.Description,
                groupSession.Capacity,
                "Group",
                groupSession.StartTime,
                groupSession.EndTime,
                enrolledClients,
                coachDto,
                roomDto);
        }).ToList();
    }

    public async Task EnrollClient(CreateGroupEnrollmentDto dto, CancellationToken ct)
    {
        var groupClientModel = new GroupTrainingSessionClient()
        {
            ClientId = dto.ClientId,
            GroupTrainingSessionId = dto.SessionId
        };
        await _groupTrainingSessionClientRepository.AddAsync(groupClientModel, ct);
    }
}