using KickTime.Core.Entities;
namespace KickTime.Application.Authentication.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}

