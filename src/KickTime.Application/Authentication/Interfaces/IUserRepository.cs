using KickTime.Core.Entities;
namespace KickTime.Application.Authentication.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);

        Task<long> CreateAsync(User user, CancellationToken cancellationToken);

        Task UpdateAsync(User user, CancellationToken cancellationToken);
    }
}


