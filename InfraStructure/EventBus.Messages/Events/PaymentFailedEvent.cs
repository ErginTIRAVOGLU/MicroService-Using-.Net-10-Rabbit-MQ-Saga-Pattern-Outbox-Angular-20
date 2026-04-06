namespace EventBus.Messages.Events;

public sealed class PaymentFailedEvent : BaseIntegrationEvent
{
    public int OrderId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}
