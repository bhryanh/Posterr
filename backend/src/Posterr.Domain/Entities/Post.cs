namespace Posterr.Domain.Entities;

public class Post
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsRepost { get; private set; }

    //Foreign Keys
    public Guid AuthorId { get; private set; }
    public Guid? OriginalPostId { get; private set; }

    // Navigation properties
    public virtual User Author { get; private set; }
    public virtual Post? OriginalPost { get; private set; }
    public virtual ICollection<Post> Reposts { get; private set; }
}
