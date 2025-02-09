using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContaCorrenteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContaCorrenteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Accounts()
    {
        var accounts = await _mediator.Send(new GetAccountQuery());
        return Ok(accounts);
    }

    [HttpPost("saldo")]
    public async Task<IActionResult> ObterSaldo(SaldoContaCorrenteQuery query)
    {
        SaldoContaCorrenteResponse saldo = await _mediator.Send(query);
        return Ok(saldo);
    }
}
