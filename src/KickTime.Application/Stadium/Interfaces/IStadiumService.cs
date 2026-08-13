using KickTime.Application.Results;
using KickTime.Core.DTOs.Stadium;

namespace KickTime.Application.Stadium.Interfaces;

public interface IStadiumService
{
    Task<Result<StadiumResponse>> CreateAsync(
        CreateStadiumRequest request,
        CancellationToken cancellationToken);

    Task<Result<StadiumResponse>> GetByIdAsync(
        long id,
        CancellationToken cancellationToken);

    Task<Result<IEnumerable<StadiumResponse>>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Result<StadiumResponse>> UpdateAsync(
        long id,
        UpdateStadiumRequest request,
        CancellationToken cancellationToken);

    Task<Result<bool>> DeleteAsync(
        long id,
        CancellationToken cancellationToken);
}