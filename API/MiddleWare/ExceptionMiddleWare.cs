using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.MiddleWare
{
    public class ExceptionMiddleWare(IHostEnvironment env, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception err)
            {
                
                await HandleExceptionAsync(httpContext, err, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception err, IHostEnvironment env)
        {
          httpContext.Response.ContentType = "application/json";
          httpContext.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
          var response = env.IsDevelopment()? new ApiErrorsResponse(httpContext.Response.StatusCode, err.Message, err.StackTrace!):
          new ApiErrorsResponse(httpContext.Response.StatusCode, err.Message, "internal server error");

          //create json serializer options obj
          /*
          here the api is smart enough to handle or return the response in the camelcase to the client
          */
          var jsonSerializedOption = new JsonSerializerOptions
          {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          };
           var serializeJson = JsonSerializer.Serialize(response, jsonSerializedOption);
           return httpContext.Response.WriteAsync(serializeJson);
        }
    }
}