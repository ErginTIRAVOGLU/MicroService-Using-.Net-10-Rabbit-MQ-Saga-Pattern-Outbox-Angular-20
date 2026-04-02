namespace OrderService.Entities;

public abstract class BaseEntity
{
    // protected set is made to use in the derived class
    public int Id { get; protected set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }

}
