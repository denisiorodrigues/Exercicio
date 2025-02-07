using Dapper;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Data;
using System.Transactions;

namespace Questao5.Infrastructure.Database.Repository;

public class MovimentoRepository : IMovimentoRepository
{
    private readonly IDbConnection _dbConnection;

    public MovimentoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    /*
     idmovimento TEXT(37) PRIMARY KEY, -- identificacao unica do movimento
	    idcontacorrente INTEGER(10) NOT NULL, -- identificacao unica da conta corrente
	    datamovimento TEXT(25) NOT NULL, -- data do movimento no formato DD/MM/YYYY
	    tipomovimento TEXT(1) NOT NULL, -- tipo do movimento. (C = Credito, D = Debito).
	    valor REAL NOT NULL*/
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
}
