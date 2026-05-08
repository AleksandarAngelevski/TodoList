namespace Domain.Models;

public class BaseAuditableEntity<TU> : BaseEntity
{
    public string?CreatedById { get; set; } 
    public DateTime?DateCreated { get; set; } 
    
    public string? LastModifiedBy { get; set; } 
    public DateTime? DateLastModified { get; set; }
}