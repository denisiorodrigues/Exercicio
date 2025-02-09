using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Text;

namespace Questao5.Middleware;

public class IdempotencyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public IdempotencyMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var idempotenciaRepository = scope.ServiceProvider.GetRequiredService<IIdempotenciaRepository>();
            if (context.Request.Headers.TryGetValue("idempotency-Key", out var idempotencyKey))
            {
                string chaveIdempotencia = idempotencyKey.ToString();
                var existingRequest = await idempotenciaRepository.ObterPorChaveAsync(chaveIdempotencia);
                if (existingRequest != null)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(existingRequest.Resultado);
                    return;
                }

                var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var idempotencia = new Idempotencia(chaveIdempotencia, requestBody);

                await idempotenciaRepository.CadastrarAsync(idempotencia);

                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            }

            await _next(context);

            //if (context.Request.Headers.TryGetValue("Idempotency-Key", out idempotencyKey))
            //{
            //    var idempotencia = await idempotenciaRepository.ObterPorChaveAsync(idempotencyKey.ToString());
            //    if (idempotencia != null)
            //    {
            //        idempotencia.Resultado = context.Response.ToString();
            //        await idempotenciaRepository.AtualizarAsync(idempotencia);
            //    }
            //}

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                try
                {
                    //context.Response.Body = responseBody;

                    //context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    //context.Response.Body.Seek(0, SeekOrigin.Begin);

                    if (context.Request.Headers.TryGetValue("Idempotency-Key", out idempotencyKey))
                    {
                        var idempotencia = await idempotenciaRepository.ObterPorChaveAsync(idempotencyKey.ToString());
                        if (idempotencia != null)
                        {
                            idempotencia.Resultado = string.IsNullOrWhiteSpace(responseText) ? "response não capturado" : responseText;
                            await idempotenciaRepository.AtualizarAsync(idempotencia);
                        }
                    }

                    //await responseBody.CopyToAsync(originalBodyStream);
                }
                catch (Exception ex)
                {
                    throw;
                }
                
            }
        }
    }
}

