using Basket.Commands;
using Basket.Mappers;
using Basket.Queries;
using MassTransit;
using MediatR;

namespace Basket.Handlers;

public sealed class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<CheckoutBasketCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutBasketCommandHandler(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<CheckoutBasketCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;

    }


    public async Task<Unit> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var dto = request.BasketCheckoutDto;
        var basketResponse = await _mediator.Send(new GetBasketByUserNameQuery(dto.UserName), cancellationToken);
        if (basketResponse is null || !basketResponse.Items.Any())
        {
            throw new InvalidOperationException("Basket not found or empty");
        }
        var basket = basketResponse.ToEntity();

        var evt = dto.ToBasketCheckoutEvent(basket);

        var correlationIdHeader = _httpContextAccessor.HttpContext?.Request.Headers["CorrelationId"];
        if (!string.IsNullOrEmpty(correlationIdHeader) && Guid.TryParse(correlationIdHeader, out Guid correlationId))
        {
            evt.CorrelationId = correlationId;
        }
        _logger.LogInformation("Publishing BasketCheckoutEvent for {User} with CorrelationId {CorrelationId}", basket.UserName, evt.CorrelationId);
        await _publishEndpoint.Publish(evt, cancellationToken);

        await _mediator.Send(new DeleteBasketByUserNameCommand(dto.UserName), cancellationToken);
        return Unit.Value;
    }

}