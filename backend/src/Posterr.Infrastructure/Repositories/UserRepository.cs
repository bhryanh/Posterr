using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Entities;
using Posterr.Domain.Interface;
using Posterr.Infrastructure.Data;

namespace Posterr.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PosterrDbContext _context;

    public UserRepository(PosterrDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(p => p.Posts)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        return await _context.Users
            .Include(p => p.Posts)
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}