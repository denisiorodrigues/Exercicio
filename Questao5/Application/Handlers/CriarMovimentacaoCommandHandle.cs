using MediatR;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Exceptions;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using Questao5.Domain.Helpers;

namespace Questao5.Application.Handlers;

public class CriarMovimentacaoCommandHandle : IRequestHandler<CriarMovimentacaoCommand, MovimentacaoResponse>
{
    IMovimentoRepository _movimentoRepository;
    IContaCorrenteRepository _contaCorrenteRepository;

    public CriarMovimentacaoCommandHandle(IMovimentoRepository movimentoRepository, IContaCorrenteRepository contaCorrenteRepository)
    {
        _movimentoRepository = movimentoRepository;
        _contaCorrenteRepository = contaCorrenteRepository;
    }

    public async Task<MovimentacaoResponse> Handle(CriarMovimentacaoCommand request, CancellationToken cancellationToken)
    {
        var conta = await _contaCorrenteRepository.ObterPorNumeroAsync(request.NumeroConta);
        char tipoMovimentacao = request.TipoMovimento.ToUpper()[0];

        if (conta is null)
            throw new InvalidAccountException();

        if (!conta.EhAtivo)
            throw new IncativeAccountException();

        string dataMovimentacao = DateTime.Now.ToString("dd/MM/yyyy");
        var requisicaoId = Guid.NewGuid().ToString();

        var movimentacaoContaOrigem = new Movimento(conta.Id, dataMovimentacao, tipoMovimentacao, request.Valor);

        await _movimentoRepository.AddAsync(movimentacaoContaOrigem);

        return new MovimentacaoResponse(movimentacaoContaOrigem.Id);
    }
}
