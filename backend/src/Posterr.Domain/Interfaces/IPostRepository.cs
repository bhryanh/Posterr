using Posterr.Domain.Entities;

namespace Posterr.Domain.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(Guid id);
    Task<Post> AddAsync(Post post);
}