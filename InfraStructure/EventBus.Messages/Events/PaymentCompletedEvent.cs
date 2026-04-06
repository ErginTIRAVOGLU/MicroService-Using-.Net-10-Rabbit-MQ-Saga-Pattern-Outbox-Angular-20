namespace EventBus.Messages.Events;

public sealed class PaymentCompletedEvent : BaseIntegrationEvent
{
    public int OrderId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    
}
