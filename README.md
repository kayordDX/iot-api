# Kayord.IOT

Backend for kayord iot

```bash
dotnet ef migrations add Initial --project src/Kayord.IOT --startup-project src/Kayord.IOT --output-dir Data/Migrations

dotnet ef database update --project src/Kayord.IOT --startup-project src/Kayord.IOT

dotnet run --project src/Kayord.IOT
```

## Postgres

```bash
docker compose up -d
```

## Secrets

```bash
dotnet user-secrets init
dotnet user-secrets set "MQTT:User" "secret"
dotnet user-secrets set "MQTT:Password" "secret"
dotnet user-secrets set "MQTT:Server" "secret"
dotnet user-secrets list
```

## Schema

Entity
===
Name

State
===
StateId
EntityId
State
OldStateId
LastUpdated

Event -> TimeScaleTable
===
EntityId
Time
Value