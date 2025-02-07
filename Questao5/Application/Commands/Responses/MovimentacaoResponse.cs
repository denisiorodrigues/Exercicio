namespace Questao5.Application.Commands.Responses;

public class MovimentacaoResponse
{
    public MovimentacaoResponse(string movimentoId)
    {
        MovimentoId = movimentoId;
    }

    public string MovimentoId { get; set; }
}
