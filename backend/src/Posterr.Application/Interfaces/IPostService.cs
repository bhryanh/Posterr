using Posterr.Application.DTOs;

namespace Posterr.Application.Interfaces;

public interface IPostService
{
    Task<PostDto?> GetByIdAsync(Guid id);
    Task<PostDto> CreatePostAsync(CreatePostDto createPostDto);
}