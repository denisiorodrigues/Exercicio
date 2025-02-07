using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstraction;

public interface IContaCorrenteRepository
{
    Task<IEnumerable<ContaCorrente>> GetAllAsync();
    Task<ContaCorrente> ObterPorNumeroAsync(int numero);
}
