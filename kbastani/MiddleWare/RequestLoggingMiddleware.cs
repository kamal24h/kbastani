//using Humanizer;
//using System.Diagnostics;
//using System.Text;

namespace WebAPP.MiddleWare;
//{
//    public class RequestLoggingMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<RequestLoggingMiddleware> _logger;
//        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
//        {
//            _next = next; _logger = logger;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            var watch = Stopwatch.StartNew();
//            _logger.LogInformation($"Request started: {context.Request.Path}");
//            await _next(context);
//            watch.Stop();
//            _logger.LogInformation($"Request ended: {context.Request.Path} took {watch.ElapsedMilliseconds} ms");
//        }
//    }

//}

//To create a custom logging middleware in ASP.NET Core and persist logs (e.g., to a file), follow these steps:

//1.Create the Middleware
//Define a custom middleware class to log request and response details.
//Csharpusing Microsoft.AspNetCore.Http;

using System.IO;
using System.Text;
using System.Threading.Tasks;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log Request
        var requestLog = await FormatRequest(context.Request);
        await LogToFileAsync("RequestLog.txt", requestLog);

        // Copy original response body stream
        var originalResponseBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            // Continue down the pipeline
            await _next(context);

            // Log Response
            var responseLog = await FormatResponse(context.Response);
            await LogToFileAsync("ResponseLog.txt", responseLog);

            // Copy the response back to the original stream
            await responseBody.CopyToAsync(originalResponseBodyStream);
        }
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        var body = request.Body;

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await body.ReadAsync(buffer, 0, buffer.Length);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        body.Seek(0, SeekOrigin.Begin);

        return $"Method: {request.Method}, Path: {request.Path}, Body: {bodyAsText}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return $"Status Code: {response.StatusCode}, Body: {text}";
    }

    private async Task LogToFileAsync(string fileName, string log)
    {
        var logPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        await File.AppendAllTextAsync(logPath, $"{log}\n\n");
    }
}


//2.Register the Middleware
//Add the middleware to the request pipeline in the Startup.cs or Program.cs file.
//Csharppublic class Startup
//{
//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        app.UseMiddleware<CustomLoggerMiddleware>();

//        // Other middlewares
//        app.UseRouting();
//        app.UseEndpoints(endpoints =>
//        {
//            endpoints.MapControllers();
//        });
//    }
//}


//3.Test the Middleware

//Run your application and make HTTP requests.
//Check the RequestLog.txt and ResponseLog.txt files in the application directory for logged data.


//4. Notes

//Performance: Logging large request/response bodies can impact performance. Use this approach selectively for debugging or specific endpoints.
//File Management: Rotate or archive log files periodically to avoid excessive file size.
//Third-Party Libraries: For production-grade logging, consider libraries like Serilog or NLog, which provide advanced features like structured logging and log file rotation.

//This custom middleware gives you full control over logging while persisting logs to files.

