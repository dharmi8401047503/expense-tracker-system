# ExpenseTracker — refreshed UI and authentication

## Requirements
- .NET 8 SDK
- SQL Server LocalDB (or change `ExpenseTracker/appsettings.Development.json` connection string)

## Run (PowerShell, from this folder)
```powershell
dotnet restore
dotnet build
dotnet ef database update --project .\ExpenseTracker\ExpenseTracker.csproj --startup-project .\ExpenseTracker\ExpenseTracker.csproj
dotnet run --project .\ExpenseTracker\ExpenseTracker.csproj
```
The app also applies migrations automatically when it starts. Existing migrations and data models are preserved.

## Authentication
- `/Account/Register` and `/Account/Login` use a dedicated responsive layout.
- Registration requires at least 8 characters with uppercase, lowercase and a number.
- SQL Server must be running and accessible for registration to save a user.
- Registration and login show helpful errors on validation and database failure.

## Windows Application Control
If `0x800711C7` appears, Windows has blocked a generated assembly. This is a device policy issue, not an application validation issue. Check Windows Security > Protection history or consult the device administrator. Do not disable protection.

Note: Builds/tests were not executed in the artifact creation environment because the .NET SDK was not installed.
