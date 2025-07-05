using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

// list of greetings
string[] greetings = new[]
{
    "Hello World!",
    "Welcome!",
    "Greetings!",
    "Hi there!",
    "Bonjour, le monde!",
    "¡Hola, Mundo!",
};

// static variable to track last index
int? lastIndex = null;

app.MapGet("/", () =>
{
    var random = new Random();
    int newIndex;

    // ensure different greeting than last
    do
    {
        newIndex = random.Next(greetings.Length);
    } while (lastIndex.HasValue && newIndex == lastIndex.Value);

    lastIndex = newIndex; // update last used

    var greeting = greetings[newIndex];

    var html = $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Hello App</title>
            <link rel='stylesheet' href='/styles.css'>
        </head>
        <body>
            <div class='container'>
                <h1>{greeting}</h1>
            </div>
        </body>
        </html>
    ";

    return Results.Content(html, "text/html", Encoding.UTF8);
});

app.Run();
