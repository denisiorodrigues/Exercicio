namespace Questao5.Domain.Entities;

public class Movimento
{
    public Guid Id { get; set; }
    public Guid IdContaCorrente { get; set; }
    public Guid IdIdepotencia{ get; set; }
    public char TipoMovimento { get; set; }
    public double Valor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
