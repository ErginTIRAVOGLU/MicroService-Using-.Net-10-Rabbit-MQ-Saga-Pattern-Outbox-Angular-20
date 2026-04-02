namespace OrderService.Exceptions;

public sealed class OrderNotFoundException : ApplicationException
{
    public OrderNotFoundException(string name, object key):base($"Entity {name} - {key} is not found.")
    {
        
    }
}
