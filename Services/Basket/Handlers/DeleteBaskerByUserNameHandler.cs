using Basket.Commands;
using Basket.Repositories;
using MediatR;

namespace Basket.Handlers;

public sealed class DeleteBaskerByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBaskerByUserNameHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }


    public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasket(request.userName);
        return Unit.Value;
    }

}
