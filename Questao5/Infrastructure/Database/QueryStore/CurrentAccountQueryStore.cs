using Dapper;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore;

public class CurrentAccountQueryStore : ICurrentAccountQueryStore
{
    private readonly IDbConnection _dbConnection;

    public CurrentAccountQueryStore(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ContaCorrente>> GetAllAccountsAsync()
    {
        var query = @"SELECT idcontacorrente as Id,
                                        numero as Number,
                                        nome as Name
                                        
                                FROM contacorrente";

        return await _dbConnection.QueryAsync<ContaCorrente>(query);
    }

    public async Task<ContaCorrente> GetBalanceByIdAsync(Guid accountId)
    {
        var query = @"SELECT idcontacorrente as Id,
                                        numero as Number,
                                        nome as Name,
                                        ativo as Active
                                FROM contacorrente 
                                WHERE idcontacorrente = @Id";

        return await _dbConnection.QueryFirstAsync<ContaCorrente>(query);
    }
}
