using Posterr.Domain.Entities;

namespace Posterr.Domain.Interface;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string userName);
}