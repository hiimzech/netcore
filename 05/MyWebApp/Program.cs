using Microsoft.AspNetCore.Rewrite;
using MyWebApp.interfaces;
using MyWebApp.Services;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddSingleton<WelcomeServices>(); //class only
builder.Services.AddSingleton<IWelcomeServices, WelcomeServices>(); //interface with class, will not refresh

builder.Services.AddScoped<IWelcomeServices, WelcomeServices>(); //use scope or else guid won't refresh

builder.Services.AddTransient<IWelcomeServices, WelcomeServices>(); //use transient or else time won't refresh



var app = builder.Build();


app.Use(async (context, next) =>
{
    await next();
    Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
});


app.UseRewriter(new RewriteOptions().AddRedirect("history", "about"));

//app.MapGet("/", () => "welcome to contoso!");
//app.MapGet("/", (WelcomeServices welcomeserve) => welcomeserve.GetWelcomeMsg()); //using class
//app.MapGet("/", (IWelcomeServices welcomeserve) => welcomeserve.GetWelcomeMsg()); //using interface

app.MapGet("/", async (IWelcomeServices welcomeserveSingleton, IWelcomeServices welcomeserveScoped, IWelcomeServices welcomeserveTransient) =>
{
    string single = $"singleton response: {welcomeserveSingleton.GetWelcomeMsg()}";
    string scoped = $"scoped response: {welcomeserveScoped.GetWelcomeMsg()}";
    string transient = $"transient response: {welcomeserveTransient.GetWelcomeMsg()}";

    return $"{single}\n{scoped}\n{transient}";
});

app.MapGet("/about", () => "contoso was found in 2000");

app.Run();

//dotnet publish -c Release -o published

//dotnet publish -c Release -r win-x64 -o publish-scd-win64 --self-contained
//dotnet publish -c Release -r linux-x64 -o publish-scd-linux64 --self-contained