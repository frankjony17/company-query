using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Company.JsonApiSpec.Models.Errors;
using Company.Query.Infra.CrossCutting.Interfaces.Exception;
using System;
using System.Net;

namespace Company.Query.Api.Filters
{
    public class JsonApiSpecExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var jsonApiSpecErrorCollection = CreateJsonApiSpecErrorCollection(context);

            context.Result = new ObjectResult(jsonApiSpecErrorCollection)
            {
                StatusCode = jsonApiSpecErrorCollection.GetErrorStatusCode()
            };
        }

        private JsonApiSpecErrorCollection CreateJsonApiSpecErrorCollection(ExceptionContext context)
        {
            if (context.Exception is JsonApiSpecException jsonApiSpecException)
            {
                return jsonApiSpecException.GetError();
            }

            var jsonApiSpecError = CreateJsonApiSpecError(context.Exception);

            var jsonApiSpecErrorCollection = new JsonApiSpecErrorCollection();
            jsonApiSpecErrorCollection.Add(jsonApiSpecError);

            return jsonApiSpecErrorCollection;
        }

        private JsonApiSpecError CreateJsonApiSpecError(Exception exception)
        {
            return exception is ICustomException customException
                ? CreateJsonApiSpecErrorFromCustomException(customException)
                : CreateJsonApiSpecErrorForInternalError(exception);
        }

        private JsonApiSpecError CreateJsonApiSpecErrorFromCustomException(ICustomException customException) =>
            new JsonApiSpecError(status: customException.StatusCode,
                                 title: customException.Title,
                                 detail: customException.Message);

        private JsonApiSpecError CreateJsonApiSpecErrorForInternalError(Exception exception) =>
            new JsonApiSpecError(status: (int)HttpStatusCode.InternalServerError,
                                 title: nameof(HttpStatusCode.InternalServerError),
                                 detail: exception.Message);
    }
}
