using Questao5.Domain.Abstraction;

namespace Questao5.Middleware;

public class ResponseCaptureMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ResponseCaptureMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        // Backup do stream original
        var originalBodyStream = context.Response.Body;

        try
        {
            if (context.Request.Headers.TryGetValue("idempotency-Key", out var idempotencyKey))
            {
                using (var memoryStream = new MemoryStream())
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var idempotenciaRepository = scope.ServiceProvider.GetRequiredService<IIdempotenciaRepository>();

                    // Substitui o Body da Response pelo MemoryStream
                    context.Response.Body = memoryStream;

                    // Continua o pipeline
                    await _next(context);

                    // Reseta a posição do stream para leitura
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Lê o conteúdo do response
                    var responseText = await new StreamReader(memoryStream).ReadToEndAsync();

                    string chaveIdempotencia = idempotencyKey.ToString();
                    var idempotencia = await idempotenciaRepository.ObterPorChaveAsync(idempotencyKey.ToString());
                    if (idempotencia != null)
                    {
                        idempotencia.Resultado = responseText;
                        await idempotenciaRepository.AtualizarAsync(idempotencia);
                    }

                    // Reenvia o conteúdo original para o response
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBodyStream);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Restaura o stream original
            context.Response.Body = originalBodyStream;
        }
    }
}

