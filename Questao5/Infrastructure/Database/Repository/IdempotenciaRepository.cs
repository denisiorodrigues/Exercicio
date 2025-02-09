using Dapper;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Database.Repository;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly IDbConnection _dbConnection;

    public IdempotenciaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task AtualizarAsync(Idempotencia idempotencia)
    {
        var query = @"UPDATE idempotencia SET (requisicao = @Requisicao, resultado = @Resultado) 
                      WHERE chave_idempotencia = @Chave";

        await _dbConnection.ExecuteAsync(query, new
        {
            Chave = idempotencia.Chave,
            Requisicao = idempotencia.Requisicao,
            Resultado = idempotencia.Resultado
        });
    }

    public async Task CadastrarAsync(Idempotencia idempotencia)
    {
        var query = @"INSERT INTO idempotencia (chave_idempotencia , requisicao , resultado) 
                          VALUES (@Chave, @Requisicao, @Resultado)";

        await _dbConnection.ExecuteAsync(query, new
        {
            Chave = idempotencia.Chave,
            Requisicao = idempotencia.Requisicao,
            Resultado = idempotencia.Resultado
        });
    }

    public async Task<Idempotencia> ObterPorChaveAsync(string chave)
    {
        var query = @"SELECT chave_idempotencia as Chave, 
                             requisicao as Requisicao, 
                             resultado as Resultado
                      FROM idempotencia  
                      WHERE chave_idempotencia = @Chave";

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Idempotencia>(query, new { Chave = chave });
        return result;
    }
}
