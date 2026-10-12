using Domain.Entities.Auth;

namespace Domain.Entities.Agg;

public class AggregateRoot
{
    public bool IsDeleted { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; }

    public int? EditorId { get; set; }
    public User? Editor { get; set; }
    public DateTime? EditedAt { get; set; }
}