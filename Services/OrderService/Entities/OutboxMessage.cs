namespace OrderService.Entities;

public sealed class OutboxMessage : BaseEntity
{
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string CorrelationId { get; set; } = string.Empty;
    public DateTime OccouredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public bool isProcessed => ProcessedOn.HasValue;
    public string? Error { get; set; }
}
