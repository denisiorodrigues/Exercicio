using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands;

public class CriarMovimentacaoCommand : IRequest<MovimentacaoResponse>
{
    public int NumeroConta { get; set; }
    /// <summary>
    /// Os tipos de movimentção podem ser
    /// C - Crédito
    /// D - Débito
    /// </summary>
    public string TipoMovimento { get; set; }  
    public double Valor { get; set; }
}
