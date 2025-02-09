namespace Questao5.Application.Queries.Responses;

public record SaldoContaCorrenteResponse(int NumeroConta, string NomeTitular, string DataHoraConsulta, decimal SaldoAtual)
{ 
}
