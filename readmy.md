# Plug & play DB checklist

A) Migracje: tak — folder `src/Eurodruk.App/Data/Migrations/` (z migracją `20240620120000_InitialCreate`).

B) Zmienione pliki:
- `src/Eurodruk.App/Data/DatabaseInitializer.cs`
- `src/Eurodruk.App/Program.cs`
- `src/Eurodruk.App/Data/Migrations/20240620120000_InitialCreate.cs`
- `src/Eurodruk.App/Data/Migrations/WorkshopDbContextModelSnapshot.cs`

C) Komendy do uruchomienia na Windows (PowerShell):
```powershell
# (opcjonalnie) utworzenie bazy, jeśli nie istnieje
sqlcmd -S .\SQLEXPRESS -Q "IF DB_ID('EurodrukDb') IS NULL CREATE DATABASE [EurodrukDb];"

# zaktualizowanie schematu (tworzy bazę, jeśli trzeba)
dotnet tool install --global dotnet-ef
cd <ścieżka-do-repo>/Eurodruk-projekt
dotnet ef database update -p src/Eurodruk.App -s src/Eurodruk.App
```

D) Kompilacja: w tym środowisku brakowało zainstalowanego .NET SDK, więc nie mogłem uruchomić `dotnet build`. Na Windows uruchom `dotnet build Eurodruk.sln` po wcześniejszym zainstalowaniu .NET 8 SDK.
