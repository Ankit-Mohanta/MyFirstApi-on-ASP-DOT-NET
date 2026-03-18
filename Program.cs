var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add the Mongo service dependecy injection (DI) to the container, so that it can be used in the controllers
builder.Services.AddSingleton<MongoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// creating a GET API for default endpoint, which will return just hello world as a string response
app.MapGet("/", () => "Hello World!");

app.MapGet("/weatherforecast", () =>
{
    // generate the response data using the WeatherForecast record and return it as an array
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast"); // naming the endpoint for better OpenAPI documentation

// Create the endpoint for getting all users from MongoDB database, which will use the MongoService to fetch the data and return it as a list of User objects
app.MapGet("/users", async(MongoService mongo) =>
{
    var users = await mongo.GetUsersAsync();
    return users;
});

// Create the endpoint for creating a new user in MongoDB database, which will accept a User object in the request body and use the MongoService to insert it into the database, then return the created user as a response
app.MapPost("/users", async (MongoService mongo, User user) =>
{
    await mongo.CreateUserAsync(user);
    return Results.Ok(user);
});

app.Run();

// Just like DTOs, records are a great way to represent data in a structured way. In this case, the WeatherForecast record has three properties: Date, TemperatureC, and Summary. The TemperatureF property is calculated based on the TemperatureC property, providing a convenient way to get the temperature in Fahrenheit without needing to store it separately.
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
