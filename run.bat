@echo off
REM Double-click to start the site (requires .NET 8 SDK)
dotnet restore
dotnet run --project "%~dp0BoxCricketBuddy.csproj"
pause
