using FitCore_API.Abstractions.Repositories;
using FitCore_API.Abstractions.Services;
using FitCore_API.DTOs;
using FitCore_API.Entities;
using FitCoreAPI.DTOs;

namespace FitCore_API.Services;

public class PersonalTrainingSessionService: IPersonalTrainingSessionService
{
    private readonly IPersonalTrainingSessionRepository _personalTrainingSessionRepository;

    public PersonalTrainingSessionService(IPersonalTrainingSessionRepository personalTrainingSessionRepository)
    {
        _personalTrainingSessionRepository = personalTrainingSessionRepository;
    }
    
    public async Task<List<PersonalSessionDto>> GetPersonalSessionsByClientIdAsync(Guid clientId, CancellationToken ct)
    {
        var personalSessions = await _personalTrainingSessionRepository.GetByClientIdAsync(clientId, ct);
        return personalSessions.Select(ps => new PersonalSessionDto(
            ps.Id,
            ps.ClientId,
            ps.CoachId,
            $"{ps.Coach.User.FirstName}  {ps.Coach.User.LastName}",
            ps.RoomId,
            "Personal",
            ps.Name,
            ps.StartDate,
            ps.EndDate
        )).ToList();    
    }

    public async Task<PersonalSessionDto?> GetPersonalSessionById(Guid id, CancellationToken ct)
    {
        var personalSession = await _personalTrainingSessionRepository.GetByIdAsync(id, ct);
        if(personalSession == null) return null;
        return new PersonalSessionDto(
            personalSession.Id,
            personalSession.ClientId,
            personalSession.CoachId,
            $"{personalSession.Coach.User.FirstName}  {personalSession.Coach.User.LastName}",
            personalSession.RoomId,
            "Personal",
            personalSession.Name,
            personalSession.StartDate,
            personalSession.EndDate
        );
    }

    public async Task<List<PersonalSessionDto>> GetPersonalSessionsByCoachIdAsync(Guid coachId, CancellationToken ct)
    {
        var personalSessions = await _personalTrainingSessionRepository.GetByCoachIdAsync(coachId, ct);
        return personalSessions.Select(ps => new PersonalSessionDto(
            ps.Id,
            ps.ClientId,
            ps.CoachId,
            $"{ps.Coach.User.FirstName}  {ps.Coach.User.LastName}",
            ps.RoomId,
            "Personal",
            ps.Name,
            ps.StartDate,
            ps.EndDate
        )).ToList();
    }

    public async Task<List<PersonalSessionDto>> GetPersonalSessionByLocation(string locationName, CancellationToken ct)
    {
        var sessions = await _personalTrainingSessionRepository.GetByLocationAsync(locationName, ct);
        return sessions.Select(s => new PersonalSessionDto(
            s.Id,
            s.ClientId,
            s.CoachId,
            $"{s.Coach.User.FirstName}  {s.Coach.User.LastName}",
            s.RoomId,
            "Personal",
            s.Name,
            s.StartDate,
            s.EndDate
            )).ToList();
    }

    public async Task CreatePersonalSession(CreatePersonalSessionDto dto, CancellationToken ct)
    {
        var personalModel = new PersonalTrainingSessionModel
        {
            Id = Guid.NewGuid(),
            ClientId = dto.ClientId,
            CoachId = dto.CoachId,
            RoomId = dto.RoomId,
            Name = dto.Name,
            StartDate = dto.StartTime,
            EndDate = dto.EndTime
        };
        await _personalTrainingSessionRepository.AddAsync(personalModel, ct);
    }

    public async Task<List<PersonalSessionWithCoachAndRoomDto>> GetAllPersonalSessionWithCoachAndRoomById(Guid id, CancellationToken ct)
    {
        var personalSessions = await _personalTrainingSessionRepository.GetAllWithCoachAndRoomById(id, ct);
        return personalSessions.Select(personalSession =>
        {
            var locationModel = personalSession.Coach.Location;
            var locationDto = new LocationDto(
                locationModel.Name,
                locationModel.Address
            );

            var coachModel = personalSession.Coach;
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

            var roomModel = personalSession.Room;
            var roomDto = new RoomDto(
                roomModel.Id,
                roomModel.LocationName,
                roomModel.RoomType.ToString(),
                roomModel.Capacity
            );

            return new PersonalSessionWithCoachAndRoomDto(
                personalSession.Id,
                personalSession.ClientId,
                personalSession.CoachId,
                $"{coachDto.FirstName} {coachDto.LastName}",
                personalSession.RoomId,
                "Personal",
                personalSession.Name,
                personalSession.StartDate,
                personalSession.EndDate,
                coachDto,
                roomDto);
        }).ToList();
    }
}