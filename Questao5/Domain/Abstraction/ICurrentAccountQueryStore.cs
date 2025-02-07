using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstraction;

public interface ICurrentAccountQueryStore
{
    Task<IEnumerable<ContaCorrente>> GetAllAccountsAsync();
    Task<ContaCorrente> GetBalanceByIdAsync(Guid accountId);
}
