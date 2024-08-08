using Application.Exceptions;
using Core.Exceptions;
using DataAccess.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Filters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly IWebHostEnvironment _env;
        private readonly BookManagemenContext _dbContext;
        private IDbContextTransaction? Transaction => _dbContext.Database.CurrentTransaction;

        public ApiExceptionFilterAttribute(IWebHostEnvironment env, BookManagemenContext dbContext)
        {
            _env = env;
            _dbContext = dbContext;
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(UnprocessableRequestException), HandleInvalidModelStateException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(BadRequestException), HandleBadRequestException },
                { typeof(ResourceNotFoundException), HandleResourceNotFoundException },

            };
        }


        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            HandleException(context);

            if (Transaction != null) await Transaction.RollbackAsync();

            await base.OnExceptionAsync(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;
            context.Result = new NotFoundObjectResult(exception?.Message)
            {
                StatusCode = StatusCodes.Status404NotFound
            };
            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var exception = context.Exception as UnprocessableRequestException;
            var details = new ValidationProblemDetails(exception?.Errors ?? throw new InvalidOperationException());

            context.Result = new UnprocessableEntityObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleBadRequestException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }

        private void HandleAuthorizationException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            context.ExceptionHandled = true;
        }

        private void HandleResourceNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as ResourceNotFoundException;
            context.Result = new NotFoundObjectResult(exception?.Message)
            {
                StatusCode = StatusCodes.Status404NotFound
            };
            context.ExceptionHandled = true;
        }


    }
}