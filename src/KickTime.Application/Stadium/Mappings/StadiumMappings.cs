using KickTime.Core.DTOs.Stadium;
using StadiumEntity = KickTime.Core.Entities.Stadium;

namespace KickTime.Application.Stadium.Mappings;

public static class StadiumMappings
{
    public static StadiumResponse ToResponse(this StadiumEntity stadium)
    {
        return new StadiumResponse
        {
            Id = stadium.Id,
            Name = stadium.Name,
            Location = stadium.Location,
            Description = stadium.Description,
            CreatedAt = stadium.CreatedAt
        };
    }

    public static StadiumEntity ToEntity(this CreateStadiumRequest request)
    {
        return new StadiumEntity
        {
            Name = request.Name,
            Location = request.Location,
            Description = request.Description
        };
    }

    public static void ApplyUpdate(
        this StadiumEntity stadium,
        UpdateStadiumRequest request)
    {
        stadium.Name = request.Name;
        stadium.Location = request.Location;
        stadium.Description = request.Description;
    }
}