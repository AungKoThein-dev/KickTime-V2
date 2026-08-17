using KickTime.Core.Entities;

namespace KickTime.Application.Court.Interfaces;

public interface ICourtRepository
{
    Task<long> CreateAsync(
        CourtEntity CourtEntity,
        CancellationToken cancellationToken);

    Task<CourtEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken);

    Task<IEnumerable<CourtEntity>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<CourtEntity>> GetByStadiumIdAsync(
        long stadiumId,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        CourtEntity CourtEntity,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        long id,
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
    long stadiumId,
    string name,
    CancellationToken cancellationToken);
}