using MediatR;
using Questao5.Application.Exceptions;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Abstraction;

namespace Questao5.Application.Handlers
{
    public class SaldoContaCorrenteQueryHandle : IRequestHandler<SaldoContaCorrenteQuery, SaldoContaCorrenteResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public SaldoContaCorrenteQueryHandle(IContaCorrenteRepository contaCorrenteRepository, IMovimentoRepository movimentoRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
            _movimentoRepository = movimentoRepository;
        }

        public async Task<SaldoContaCorrenteResponse> Handle(SaldoContaCorrenteQuery request, CancellationToken cancellationToken)
        {
            if (request.NumeroConta <= 0)
                throw new InvalidAccountException();

            var contaCorrente = await _contaCorrenteRepository.ObterPorNumeroAsync(request.NumeroConta);

            if(contaCorrente is null)
                throw new InvalidAccountException();

            if (!contaCorrente.EhAtivo)
                throw new IncativeAccountException();

            var saldo = await _movimentoRepository.ObterSaldoAsync(contaCorrente.Id);

            return new SaldoContaCorrenteResponse(contaCorrente.Numero, contaCorrente.Nome, DateTime.UtcNow.ToString(), saldo);
        }
    }
}
