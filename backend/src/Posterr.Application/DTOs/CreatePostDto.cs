namespace Posterr.Application.DTOs;

public class CreatePostDto
{
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
}