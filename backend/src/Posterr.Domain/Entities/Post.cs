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

    private Post() 
    {
        Reposts = new List<Post>();
    }

    public Post(string content, User author)
    {
        Id = Guid.NewGuid();
        Content = content ?? throw new ArgumentNullException(nameof(content));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        AuthorId = author.Id;
        CreatedAt = DateTime.UtcNow;
        IsRepost = false;
        OriginalPostId = null;
        Reposts = new List<Post>();

        Validate();
    }
    
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Content))
            throw new ArgumentException("Post content cannot be empty", nameof(Content));
        
        if (Content.Length > 777)
            throw new ArgumentException("Post content cannot exceed 777 characters", nameof(Content));
    }
}
