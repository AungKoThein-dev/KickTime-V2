using KickTime.Application.Results;
using KickTime.Application.Stadium.Interfaces;
using KickTime.Application.Stadium.Mappings;
using KickTime.Core.DTOs.Stadium;
using Microsoft.Extensions.Logging;

namespace KickTime.Application.Stadium.Services;

public sealed class StadiumService : IStadiumService
{
    private readonly IStadiumRepository _stadiumRepository;
    private readonly ILogger<StadiumService> _logger;

    public StadiumService(
        IStadiumRepository stadiumRepository,
        ILogger<StadiumService> logger)
    {
        _stadiumRepository =
            stadiumRepository
            ?? throw new ArgumentNullException(nameof(stadiumRepository));

        _logger =
            logger
            ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<StadiumResponse>> CreateAsync(CreateStadiumRequest request,CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Request cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Stadium name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Location))
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Stadium location is required.");
        }

        var stadium = request.ToEntity();

        stadium.Id = await _stadiumRepository.CreateAsync(stadium,cancellationToken);

        var response = stadium.ToResponse();

        _logger.LogInformation(
            "Stadium {StadiumId} created successfully.",
            stadium.Id);

        return Result<StadiumResponse>.Success(response,
            "Stadium created successfully.");
    }

    public async Task<Result<StadiumResponse>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Invalid stadium id.");
        }

        var stadium = await _stadiumRepository.GetByIdAsync(id, cancellationToken);

        if (stadium is null)
        {
            return Result<StadiumResponse>.NotFound(
                "Stadium was not found.");
        }

        return Result<StadiumResponse>.Success(
            stadium.ToResponse());
    }

    public async Task<Result<IEnumerable<StadiumResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var stadiums = await _stadiumRepository.GetAllAsync(cancellationToken);

        var response = stadiums
            .Select(stadium => stadium.ToResponse())
            .ToList();

        return Result<IEnumerable<StadiumResponse>>.Success(
            response);
    }

    public async Task<Result<StadiumResponse>> UpdateAsync(long id,UpdateStadiumRequest request,CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Invalid stadium id.");
        }

        if (request is null)
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Request cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Stadium name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Location))
        {
            return Result<StadiumResponse>.ValidationFailure(
                "Stadium location is required.");
        }

        var stadium = await _stadiumRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (stadium is null)
        {
            return Result<StadiumResponse>.NotFound(
                "Stadium was not found.");
        }

        stadium.ApplyUpdate(request);

        var updated = await _stadiumRepository.UpdateAsync(
            stadium,
            cancellationToken);

        if (!updated)
        {
            return Result<StadiumResponse>.NotFound(
                "Stadium was not found.");
        }

        _logger.LogInformation(
            "Stadium {StadiumId} updated successfully.",
            stadium.Id);

        return Result<StadiumResponse>.Success(
            stadium.ToResponse(),
            "Stadium updated successfully.");
    }

    public async Task<Result<bool>> DeleteAsync(long id,CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return Result<bool>.ValidationFailure(
                "Invalid stadium id.");
        }

        var deleted = await _stadiumRepository.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return Result<bool>.NotFound(
                "Stadium was not found.");
        }

        _logger.LogInformation(
            "Stadium {StadiumId} deleted successfully.",
            id);

        return Result<bool>.Success(
            true,
            "Stadium deleted successfully.");
    }
}