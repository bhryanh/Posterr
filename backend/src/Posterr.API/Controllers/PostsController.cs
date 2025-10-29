using Microsoft.AspNetCore.Mvc;
using Posterr.Application.DTOs;
using Posterr.Application.Interfaces;

namespace Posterr.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ILogger<PostsController> _logger;
    public PostsController(IPostService postService, ILogger<PostsController> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    /// <summary>
    /// Get post by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
                return NotFound(new { message = "Post not found" });

            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting post by ID: {PostId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the posts" });
        }
    }

    /// <summary>
    /// Create post
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostDto post)
    {
        try
        {
            var createdPost = await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetById), new { id = createdPost.Id }, createdPost);
        }
        // catch (ValidationException ex)
        // {
        //     var errors = ex.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
        //                 return BadRequest(new { message = "Validation failed", errors });
        // }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating post");
            return StatusCode(500, new { message = "An error occurred while creating the post" });
        }
    }
}