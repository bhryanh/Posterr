using FluentValidation;
using Posterr.Application.DTOs;
using Posterr.Application.Interfaces;
using Posterr.Domain.Entities;
using Posterr.Domain.Interface;
using Posterr.Domain.Interfaces;

namespace Posterr.Application.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreatePostDto> _createPostValidator;

    public PostService(
        IPostRepository postRepository,
        IUserRepository userRepository,
        IValidator<CreatePostDto> createPostValidator)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _createPostValidator = createPostValidator;
    }

    public async Task<PostDto?> GetByIdAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null) return null;

        return MapToDto(post);
    }

    public async Task<PostDto> CreatePostAsync(CreatePostDto createPostDto)
    {
        await _createPostValidator.ValidateAndThrowAsync(createPostDto);

        var user = await _userRepository.GetByIdAsync(createPostDto.AuthorId) ?? throw new ArgumentException("User not found");


        var post = new Post(createPostDto.Content, user);
        await _postRepository.AddAsync(post);

        var createdPost = await _postRepository.GetByIdAsync(post.Id);

        if (createdPost == null)
            throw new Exception("Post not created");
            
        return MapToDto(createdPost);
    }

    private PostDto MapToDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            IsRepost = post.IsRepost,
            Author = new UserDto
            {
                Id = post.Author.Id,
                UserName = post.Author.UserName,
                CreatedAt = post.Author.CreatedAt,
            },
            OriginalPost = post.OriginalPost != null ? new PostDto
            {
                Id = post.OriginalPost.Id,
                Content = post.OriginalPost.Content,
                CreatedAt = post.OriginalPost.CreatedAt,
                IsRepost = false,
                Author = new UserDto
                {
                    Id = post.OriginalPost.Author.Id,
                    UserName = post.OriginalPost.Author.UserName,
                    CreatedAt = post.OriginalPost.Author.CreatedAt,
                },
            } : null,
        };
    }
}