using KickTime.Core.DTOs.Court;
using KickTime.Core.Entities;

namespace KickTime.Application.Court.Mappings;

public static class CourtMappings
{
    public static CourtEntity ToEntity(
        this CreateCourtRequest request)
    {
        return new CourtEntity
        {
            StadiumId = request.StadiumId,
            Name = request.Name,
            Description = request.Description,
            IsActive = true
        };
    }

    public static CourtResponse ToResponse(
        this CourtEntity court)
    {
        return new CourtResponse
        {
            Id = court.Id,
            StadiumId = court.StadiumId,
            Name = court.Name,
            Description = court.Description,
            IsActive = court.IsActive
        };
    }

    public static void ApplyUpdate(
        this CourtEntity court,
        UpdateCourtRequest request)
    {
        court.Name = request.Name;
        court.Description = request.Description;
        court.IsActive = request.IsActive;
    }
}