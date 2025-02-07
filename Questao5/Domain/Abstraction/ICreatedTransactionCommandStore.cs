using System.Transactions;

namespace Questao5.Domain.Abstraction;

public interface ICreatedTransactionCommandStore
{
    Task AddAsync(Transaction transaction);
}
