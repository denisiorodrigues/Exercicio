using Dapper;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Database.Repository;

public class MovimentoRepository : IMovimentoRepository
{
    private readonly IDbConnection _dbConnection;

    public MovimentoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task AddAsync(Movimento movimento)
    {
        var query = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                          VALUES (@Id, @ContaCorrenteId, @Data, @Tipo, @Valor)";

        await _dbConnection.ExecuteAsync(query, new
        {
            Id = movimento.Id,
            ContaCorrenteId = movimento.ContaCorrenteId,
            Data = movimento.Data,
            Tipo = movimento.Tipo,
            Valor = movimento.Valor
        });
    }

    public async Task<decimal> ObterSaldoAsync(string contaCorrenteId)
    {
        var query = @"
                SELECT 
                    SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END) - SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END) as SaldoTotal
                FROM movimento
                WHERE idcontacorrente = @ContaCorrenteId";

        return await _dbConnection.QueryFirstOrDefaultAsync<decimal>(query, new { ContaCorrenteId = contaCorrenteId });
    }
}
