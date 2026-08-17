using KickTime.Application.Results;
using KickTime.Core.DTOs.Court;

namespace KickTime.Application.Court.Interfaces;

public interface ICourtService
{
    Task<Result<CourtResponse>> CreateAsync(
        CreateCourtRequest request,
        CancellationToken cancellationToken);

    Task<Result<CourtResponse>> GetByIdAsync(
        long id,
        CancellationToken cancellationToken);

    Task<Result<IEnumerable<CourtResponse>>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Result<IEnumerable<CourtResponse>>> GetByStadiumIdAsync(
        long stadiumId,
        CancellationToken cancellationToken);

    Task<Result<CourtResponse>> UpdateAsync(
        long id,
        UpdateCourtRequest request,
        CancellationToken cancellationToken);

    Task<Result<bool>> DeleteAsync(
        long id,
        CancellationToken cancellationToken);
}