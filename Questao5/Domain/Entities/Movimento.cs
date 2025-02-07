namespace Questao5.Domain.Entities;

public class Movimento : Entity
{
    public Movimento(string id, string contaCorrenteId, string data, char tipo, double valor)
    {
        Id = id;
        ContaCorrenteId = contaCorrenteId;
        Data = data;
        Tipo = tipo;
        Valor = valor;
    }

    public Movimento(string contaCorrenteId, string data, char tipo, double valor)
    {
        Id = Guid.NewGuid().ToString();
        ContaCorrenteId = contaCorrenteId;
        Data = data;
        Tipo = tipo;
        Valor = valor;
    }

    public string ContaCorrenteId { get; set; }
    public string Data { get; set; }
    public char Tipo { get; set; }
    public double Valor { get; set; }

    public ContaCorrente ContaCorrente { get; set; }
}
