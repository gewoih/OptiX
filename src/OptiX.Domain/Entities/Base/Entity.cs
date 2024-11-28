namespace OptiX.Domain.Entities.Base;

public class Entity
{
    public Guid Id { get; set; }
    public string? Caption { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}