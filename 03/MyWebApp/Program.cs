using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World! 4");

app.Use(async (context,next) =>
{
    await next();
    Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
    // await next();
});


app.UseRewriter(new RewriteOptions().AddRedirect("history","about"));

app.MapGet("/", () => "welcome to contoso!");

app.MapGet("/about", () => "contoso was found in 2000");

// app.Use(async (context,next) =>
// {
//     Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
//     await next();
// });




app.Run();
