using Infrastructure.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Application.BasicDto
{
    public class ErrorDetails<T>
    {
        public string Message { get; set; }
        public T? Data { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }


        public static ApiResponse<object> CreateErrorResponse(Exception exception)
        {
            var errorDetails = new ErrorDetails<object>
            {
                Message = exception.Message,
                Data = null
            };


            if (exception is EntityNotFoundException)
            {
                return ApiResponse<object>.CreateApiResponse(null, StatusCodes.Status404NotFound, false, errorDetails);
            }
            if (exception is FluentValidation.ValidationException validationException)
            {
                errorDetails.Message = "Validation failed";
                errorDetails.Data = validationException.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => ToCamelCase(group.Key),
                        group => group.Select(error => error.ErrorMessage));

                return ApiResponse<object>.CreateApiResponse(null, StatusCodes.Status400BadRequest, false, errorDetails);
            }

            errorDetails.Message = "Oops, An unexpected error happened.";
            return ApiResponse<object>.CreateApiResponse(null, StatusCodes.Status500InternalServerError, false, errorDetails);
        }

        private static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

    }

}
