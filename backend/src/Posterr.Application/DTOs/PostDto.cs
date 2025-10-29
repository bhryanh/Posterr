namespace Posterr.Application.DTOs;

public class PostDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRepost { get; set; }
    public UserDto Author { get; set; }
    public PostDto? OriginalPost { get; set; }
}