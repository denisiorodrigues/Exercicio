using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Questao5.Application.Exceptions;

namespace Questao5.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CustomErrorException)
        {
            var customException = context.Exception as CustomErrorException;
            context.Result = new ObjectResult(new 
            { 
                Tipo = customException.Type,
                Message = customException.Message
            })
            {
                StatusCode = 400
            };
            context.ExceptionHandled = true;
        }

        if (context.Exception is HttpRequestException || context.Exception is InvalidOperationException)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            context.ExceptionHandled = true;
        }
    }
}
