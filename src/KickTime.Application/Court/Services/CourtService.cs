using KickTime.Application.Court.Interfaces;
using KickTime.Application.Court.Mappings;
using KickTime.Application.Results;
using KickTime.Application.Stadium.Interfaces;
using KickTime.Core.DTOs.Court;
using Microsoft.Extensions.Logging;

namespace KickTime.Application.Court.Services;

public sealed class CourtService : ICourtService
{
    private readonly ICourtRepository _courtRepository;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly ILogger<CourtService> _logger;

    public CourtService(
        ICourtRepository courtRepository,
        IStadiumRepository stadiumRepository,
        ILogger<CourtService> logger)
    {
        _courtRepository =
            courtRepository
            ?? throw new ArgumentNullException(nameof(courtRepository));

        _stadiumRepository =
            stadiumRepository
            ?? throw new ArgumentNullException(nameof(stadiumRepository));

        _logger =
            logger
            ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<CourtResponse>> CreateAsync(
        CreateCourtRequest request,
        CancellationToken cancellationToken)
    {
        var stadium = await _stadiumRepository.GetByIdAsync(
            request.StadiumId,
            cancellationToken);

        if (stadium is null)
        {
            return Result<CourtResponse>.NotFound(
                "Stadium was not found.");
        }

        var exists = await _courtRepository.ExistsByNameAsync(request.StadiumId,request.Name,cancellationToken);

        if (exists)
        {
            return Result<CourtResponse>.Conflict(
                "A court with the same name already exists in this stadium.");
        }

        var court = request.ToEntity();

        court.Id = await _courtRepository.CreateAsync(
            court,
            cancellationToken);

        _logger.LogInformation(
            "Court {CourtId} created successfully for Stadium {StadiumId}.",
            court.Id,
            court.StadiumId);

        return Result<CourtResponse>.Success(
            court.ToResponse(),
            "Court created successfully.");
    }

    public async Task<Result<CourtResponse>> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        var court = await _courtRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (court is null)
        {
            return Result<CourtResponse>.NotFound(
                "Court was not found.");
        }

        return Result<CourtResponse>.Success(
            court.ToResponse());
    }

    public async Task<Result<IEnumerable<CourtResponse>>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var courts = await _courtRepository.GetAllAsync(
            cancellationToken);

        var response = courts
            .Select(court => court.ToResponse())
            .ToList();

        return Result<IEnumerable<CourtResponse>>.Success(
            response);
    }

    public async Task<Result<IEnumerable<CourtResponse>>> GetByStadiumIdAsync(
        long stadiumId,
        CancellationToken cancellationToken)
    {
        var stadium = await _stadiumRepository.GetByIdAsync(
            stadiumId,
            cancellationToken);

        if (stadium is null)
        {
            return Result<IEnumerable<CourtResponse>>.NotFound(
                "Stadium was not found.");
        }

        var courts = await _courtRepository.GetByStadiumIdAsync(
            stadiumId,
            cancellationToken);

        var response = courts
            .Select(court => court.ToResponse())
            .ToList();

        return Result<IEnumerable<CourtResponse>>.Success(
            response);
    }

    public async Task<Result<CourtResponse>> UpdateAsync(
        long id,
        UpdateCourtRequest request,
        CancellationToken cancellationToken)
    {
        var court = await _courtRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (court is null)
        {
            return Result<CourtResponse>.NotFound(
                "Court was not found.");
        }

        court.ApplyUpdate(request);

        var updated = await _courtRepository.UpdateAsync(
            court,
            cancellationToken);

        if (!updated)
        {
            return Result<CourtResponse>.NotFound(
                "Court was not found.");
        }

        _logger.LogInformation(
            "Court {CourtId} updated successfully.",
            court.Id);

        return Result<CourtResponse>.Success(
            court.ToResponse(),
            "Court updated successfully.");
    }

    public async Task<Result<bool>> DeleteAsync(
        long id,
        CancellationToken cancellationToken)
    {
        var deleted = await _courtRepository.DeleteAsync(
            id,
            cancellationToken);

        if (!deleted)
        {
            return Result<bool>.NotFound(
                "Court was not found.");
        }

        _logger.LogInformation(
            "Court {CourtId} deleted successfully.",
            id);

        return Result<bool>.Success(
            true,
            "Court deleted successfully.");
    }
}