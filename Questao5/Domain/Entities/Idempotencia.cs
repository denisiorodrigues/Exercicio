namespace Questao5.Domain.Entities;

public class Idempotencia
{
    public Idempotencia(string chave, string requisicao)
    {
        Chave = chave;
        Requisicao = requisicao;
    }

    public Idempotencia(string chave, string requisicao, string resultado)
    {
        Chave = chave;
        Requisicao = requisicao;
        Resultado = resultado;
    }

    public string Chave { get; set; }
    public string Requisicao { get; set; }
    public string Resultado { get; set; }
}
