using StadiumEntity = KickTime.Core.Entities.Stadium;
namespace KickTime.Application.Stadium.Interfaces;

public interface IStadiumRepository
{
    Task<long> CreateAsync(
        StadiumEntity stadium,
        CancellationToken cancellationToken);

    Task<StadiumEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken);

    Task<IEnumerable<StadiumEntity>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        StadiumEntity stadium,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        long id,
        CancellationToken cancellationToken);
}