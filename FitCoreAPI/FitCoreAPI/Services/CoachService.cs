using FitCore_API.Abstractions.Repositories;
using FitCore_API.Abstractions.Services;
using FitCoreAPI.Abstractions.Repositories;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;

namespace FitCoreAPI.Services;

public class CoachService: ICoachService
{
    private readonly ICoachRepository _coachRepository;

    public CoachService(ICoachRepository coachRepository)
    {
        _coachRepository = coachRepository;
    }

    public async Task<List<CoachDto>> GetAllCoaches(CancellationToken ct)
    {
        var coaches = await _coachRepository.GetAllAsync(ct);
        return coaches.Select(coach => new CoachDto(
            coach.User.Id,
            coach.User.FirstName,      
            coach.User.LastName,
            coach.User.Email,
            coach.User.PhoneNumber,
            coach.User.UserType.ToString(),
            coach.Specialization.ToString(),
            ((Func<LocationDto>)(() =>
                    {
                        var locationModel = coach.Location;
                        var locationDto = new LocationDto(
                            locationModel.Name,
                            locationModel.Address);
                        return locationDto;
                    })
            )()
        )).ToList();
    }
    
    public async Task<CoachDto?> GetCoachById(Guid id, CancellationToken ct)
    {
        var coach = await _coachRepository.GetByIdAsync(id, ct);
        if(coach == null) return null;
        return new CoachDto(
            coach.User.Id,
            coach.User.FirstName,
            coach.User.LastName,
            coach.User.Email,
            coach.User.PhoneNumber,
            coach.User.UserType.ToString(),
            coach.Specialization.ToString(),
            ((Func<LocationDto>)(() =>
                {
                    var locationModel = coach.Location;
                    var locationDto = new LocationDto(
                        locationModel.Name,
                        locationModel.Address);
                    return locationDto;
                })
            )()
        );
    }

    public async Task<List<CoachDto>> GetAllCoachesByLocation(string locationName, CancellationToken ct)
    {
        var coaches = await _coachRepository.GetAllByLocationAsync(locationName, ct);
        
        return coaches.Select(c => new CoachDto(
            c.UserId,
            c.User.FirstName,
            c.User.LastName,
            c.User.Email,
            c.User.PhoneNumber,
            c.User.UserType.ToString(),
            c.Specialization.ToString(),
            ((Func<LocationDto>)(() =>
                {
                    var locationModel = c.Location;
                    var locationDto = new LocationDto(
                        locationModel.Name,
                        locationModel.Address);
                    return locationDto;
                })
            )()
            )).ToList();
    }

    public async Task<List<CoachWithSessionCountDto>> GetAllCoachesByLocationWithSessionCount(string locationName, CancellationToken ct)
    {
        var coaches = await _coachRepository.GetAllByLocationWithSessionsAsync(locationName, ct);
        
        return coaches.Select(c => new CoachWithSessionCountDto(
            c.UserId,
            c.User.FirstName,
            c.User.LastName,
            c.User.Email,
            c.User.PhoneNumber,
            c.User.UserType.ToString(),
            c.Specialization.ToString(),
            ((Func<LocationDto>)(() =>
                {
                    var locationModel = c.Location;
                    var locationDto = new LocationDto(
                        locationModel.Name,
                        locationModel.Address);
                    return locationDto;
                })
            )(),
            c.GroupTrainingSessions.Count + c.PersonalTrainingSessions.Count
        )).ToList();
    }
}