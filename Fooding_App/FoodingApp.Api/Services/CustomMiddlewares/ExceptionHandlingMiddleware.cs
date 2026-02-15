using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Services.CustomMiddlewares;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.Api.Services.CustomMiddlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CategoryException ex)
            {
                _logger.LogError(ex, "A CategoryException occurred.");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                var errorResponse = new { Message = ex.Message };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }

            catch (FoodItemException ex)
            {
                _logger.LogError(ex, "A FoodItemException occurred.");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                var errorResponse = new { Message = ex.Message };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }

            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(ex, "Request was canceled by the client.");
                context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                var errorResponse = new { Message = "The request was canceled by the client." };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (DbUpdateException ex) when (ex.InnerException.Message.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase) ||
            ex.InnerException.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError(ex, "A database update exception occurred.");

                var message = ex.InnerException?.Message ?? ex.Message;


                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsJsonAsync(new { Message = "Duplicate entry detected." });
                return;
            }
            catch (DbUpdateException ex) when (ex.Message.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase))
            {

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { Message = "Invalid foreign key reference." });
                return;

            }
            catch (DbUpdateException ex) 
            { 
                // Other SQL errors
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { Message = "A database error occurred." });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var errorResponse = new { Message = "An unexpected error occurred. Please try again later." };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
