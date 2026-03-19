namespace MINI_AGENDA.Models.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                switch (ex)
                {
                    case BadRequestException:
                        context.Response.StatusCode = 400;
                        break;

                    case NotFoundException:
                        context.Response.StatusCode = 404;
                        break;

                    case ConflictException:
                        context.Response.StatusCode = 409;
                        break;

                    default:
                        context.Response.StatusCode = 500;
                        break;
                }

                await context.Response.WriteAsync(new
                {
                    error = ex.Message
                }.ToString());
            }
        }
    }
}
