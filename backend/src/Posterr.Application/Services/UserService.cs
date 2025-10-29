using Posterr.Application.DTOs;
using Posterr.Application.Interfaces;
using Posterr.Domain.Interface;

namespace Posterr.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserDto?> GetByUserNameAsync(string userName)
    {
        var user = await _userRepository.GetByUsernameAsync(userName);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            CreatedAt = user.CreatedAt
        };
    }
}