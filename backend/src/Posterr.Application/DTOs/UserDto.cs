namespace Posterr.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<PostDto> Posts { get; set; }
}