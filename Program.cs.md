## App Builder
`var builder = WebApplication.CreateBuilder(args);`
It initializes the app, load env vars and other configurations from appsettings.json, prepares **dependency injection container**

The MERN equivalent code is: `const app = express()`. Think of it like express app + loading .env

## Register Services
`builder.Services.AddOpenApi();`, it adds OpenAPI (Swagger like docs) and register it in DI container

## Build the app
`var app = builder.Build();`, it converts the builder to actual running app.

## Enviorment check
```
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
```
It runs OpenAPI only in development enviorment.
It's same as: 
```
if (process.env.NODE_ENV === "development") {
  app.use("/docs", swaggerUi.serve);
}
``` 
in MERN prespective.

## Middleware
`app.UseHttpsRedirection();`, it redirects http calls to https calls, it's same as:
```
app.use((req, res, next) => {
  if (!req.secure) {
    return res.redirect("https://" + req.headers.host + req.url);
  }
  next();
});
```
in MERN. This is a middleware pipeline.

## API route
`app.MapGet("/weatherforecast", () =>`, it creates a `GET` API, just like `app.get('/')` in express.

