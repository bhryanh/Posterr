namespace Posterr.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation property
    public virtual ICollection<Post> Posts { get; private set; }
}
