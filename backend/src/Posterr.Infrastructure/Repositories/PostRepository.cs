using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Entities;
using Posterr.Domain.Interfaces;
using Posterr.Infrastructure.Data;

namespace Posterr.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PosterrDbContext _context;
    public PostRepository(PosterrDbContext context)
    {
        _context = context;
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await _context.Posts
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }
}