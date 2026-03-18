**When I run: `dotnet new webapi -n MyFirstApi`**

I got this folder structure

```
MyFirstApi/
 ├── bin/
 ├── obj/
 ├── Properties/
 ├── appsettings.json
 ├── appsettings.Development.json
 ├── Program.cs
 ├── MyFirstApi.csproj
 ├── MyFirstApi.http
 └── MyFirstApi.sln
```

## bin Folder

It contains .dll files and build files.

It's being created after running `dotnet build` or `dotnet run`

In React/NextJS term, it's same as `dist` or `output` folder, for best practices, it's best to not edit it manually.

## Obj Folder

These are temporary build files, it stores:

* Intermediate compilation data
* Dependency resolution files

For the best practices, don't touch it, leave it to CLI.

## Properties Folder

It contains: `launchsettings.json`, used for:

* Running app locally
* Enviorment config (Dev, Production)

Example:

```
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5000"
    }
  }
}
```

It defines:

* Port
* Launch browser
* Environment

## appsettings.json

It is the main configuration file

It's being used for:

* DB connection
* API keys
* App configs

Example:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

## appsettings.Development.json

Environment-specific config

* Only used in Development mode

Example:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

Important to note that:
> This overrides appsettings.json in development

## MyFirstApi.csproj

Project file (VERY IMPORTANT)

It defines:

* Dependencies

* Target framework

* Build settings

Example:
```
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```

**Think like**
> Like package.json in Node.js

## Program.cs
It's a C# file, It's the entry point of the application, just like `app.js` or `index.js` in a `React/NextJS` app.

This is where:

* App starts

* Routes defined

* Middleware added

Example:
```
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

**This handles:**

* Routing

* Dependency Injection

* Middleware pipeline