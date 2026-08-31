using Microsoft.AspNetCore.Rewrite;
using MyWebApp.interfaces;
using MyWebApp.Services;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddSingleton<WelcomeServices>(); //class only
builder.Services.AddSingleton<IWelcomeServices, WelcomeServices>(); //interface with class



var app = builder.Build();


app.Use(async (context,next) =>
{
    await next();
    Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
});


app.UseRewriter(new RewriteOptions().AddRedirect("history","about"));

//app.MapGet("/", () => "welcome to contoso!");
//app.MapGet("/", (WelcomeServices welcomeserve) => welcomeserve.GetWelcomeMsg()); //using class
app.MapGet("/", (IWelcomeServices welcomeserve) => welcomeserve.GetWelcomeMsg()); //using interface

app.MapGet("/about", () => "contoso was found in 2000");




app.Run();
