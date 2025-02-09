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
        var originalBodyStream = context.Response.Body;
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        try
        {
            using (var memoryStream = new MemoryStream())
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

                    var idempotencia = new Idempotencia(chaveIdempotencia, requestBody);

                    await idempotenciaRepository.CadastrarAsync(idempotencia);

                    context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
                }

                //Next pipeline
                await _next(context);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error MESSAGE >> : {ex.Message}"); // Aqui você pode armazenar/logar o conteúdo
        }
        finally
        {
            // Restaura o stream original
            context.Response.Body = originalBodyStream;
        }
    }
}

