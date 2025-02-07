using MediatR;
using Questao5.Application.Queries;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers;

public class GetAccountQueryHandle : IRequestHandler<GetAccountQuery, IEnumerable<ContaCorrente>>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public GetAccountQueryHandle(IContaCorrenteRepository contaCorrenteRepository)
    {
        this._contaCorrenteRepository = contaCorrenteRepository;
    }

    public async Task<IEnumerable<ContaCorrente>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _contaCorrenteRepository.GetAllAsync();
        return accounts;
    }
}
