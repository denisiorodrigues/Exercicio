using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstraction;

public interface IMovimentoRepository
{
    Task AddAsync(Movimento movimento);
}
