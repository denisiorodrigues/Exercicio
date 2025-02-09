using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstraction;

public interface IIdempotenciaRepository
{
    Task CadastrarAsync(Idempotencia idempotencia);

    Task AtualizarAsync(Idempotencia idempotencia);

    Task<Idempotencia> ObterPorChaveAsync(string chave);
}
