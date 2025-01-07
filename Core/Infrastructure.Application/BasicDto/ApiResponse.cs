using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Application.BasicDto
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Result { get; set; }
        public int StatusCode { get; set; }
        public ErrorDetails<object>? Error { get; set; }


        public static ApiResponse<object> CreateApiResponse(object data, int statusCode, bool isSuccess = true, ErrorDetails<object> errorDetails = null)
        {
            return new ApiResponse<object>
            {
                IsSuccess = isSuccess,
                Result = data,
                StatusCode = statusCode,
                Error = errorDetails
            };
        }
        public override string ToString()
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, serializeOptions);
        }

    }
}
