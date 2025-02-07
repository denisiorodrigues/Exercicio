using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries;

public class GetAccountQuery : IRequest<IEnumerable<ContaCorrente>>
{
}
