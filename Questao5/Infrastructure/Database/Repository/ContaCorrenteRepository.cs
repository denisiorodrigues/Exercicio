using Dapper;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Database.Repository;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly IDbConnection _dbConnection;

    public ContaCorrenteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ContaCorrente>> GetAllAsync()
    {
        string query = @"SELECT idcontacorrente as Id, 
                                numero as Numero, 
                                nome as Nome, 
                                ativo as EhAtivo
                        FROM contacorrente";

        return await _dbConnection.QueryAsync<ContaCorrente>(query);
    }

    public async Task<ContaCorrente> ObterPorNumeroAsync(int numero)
    {
        string query = @"SELECT idcontacorrente as Id, 
                                numero as Numero, 
                                nome as Nome, 
                                ativo as EhAtivo
                        FROM contacorrente 
                        WHERE numero = @numero";

        return await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { numero = numero });
    }
}
