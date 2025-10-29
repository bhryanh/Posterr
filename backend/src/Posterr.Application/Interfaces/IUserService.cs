using Posterr.Application.DTOs;

namespace Posterr.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<UserDto?> GetByUserNameAsync(string userName);
}