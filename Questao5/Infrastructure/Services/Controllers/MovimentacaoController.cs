using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands;
using Questao5.Application.Exceptions;
using Questao5.Domain.Helpers;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarMovimentacaoCommand criarMovimentacaoCommand, [FromHeader(Name = "idempotency-Key")] string idempotencyKey)
        {
            if (string.IsNullOrEmpty(idempotencyKey))
            {
                return BadRequest("Idempotency-Key header is required.");
            }

            if (criarMovimentacaoCommand.Valor <= 0)
                throw new InvalidValueException();

            var tipoMovimentacao = criarMovimentacaoCommand.TipoMovimento.ToUpper()[0];

            if (tipoMovimentacao != TipoMovimento.Debito && tipoMovimentacao != TipoMovimento.Credito)
                throw new InvalidTypeException();

            var result = await _mediator.Send(criarMovimentacaoCommand);

            return Ok(result);
        }


        [HttpGet("teste")]
        public IActionResult Teste()
        {
            return Ok(new {Status = true, Message = "testado com sucesso."});
        }
    }
}
