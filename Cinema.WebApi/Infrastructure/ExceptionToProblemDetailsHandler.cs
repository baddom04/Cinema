using Cinema.DataAccess.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Infrastructure
{
    /// <summary>
    /// ExceptionToProblemDetailsHandler
    /// </summary>
    public class ExceptionToProblemDetailsHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

        /// <summary>
        /// Map the extensions to status codes and create problemDetails
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            return exception switch
            {
                EntityNotFoundException => await CreateProblemDetails(httpContext, exception, 
                    StatusCodes.Status404NotFound),
                ArgumentOutOfRangeException => await CreateProblemDetails(httpContext, exception,
                    StatusCodes.Status400BadRequest),
                ArgumentNullException => await CreateProblemDetails(httpContext, exception,
                    StatusCodes.Status400BadRequest),
                ArgumentException => await CreateProblemDetails(httpContext, exception,
                    StatusCodes.Status409Conflict),
                InvalidDataException => await CreateProblemDetails(httpContext, exception,
                    StatusCodes.Status409Conflict),
                InvalidOperationException => await CreateProblemDetails(httpContext, exception,
                    StatusCodes.Status409Conflict),
                _ => false
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private async Task<bool> CreateProblemDetails(HttpContext httpContext, Exception exception, int statusCode)
        {
            httpContext.Response.StatusCode = statusCode;

            ProblemDetails problemDetails = new()
            {
                Title = "An error occured",
                Type = exception.GetType().Name,
                Detail = exception.Message,
            };

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
            {
                Exception = exception,
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            });
        }
    }
}
