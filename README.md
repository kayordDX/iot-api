# Kayord.IOT

Backend for kayord iot

```bash
dotnet ef migrations add Initial --project src/Kayord.POS --startup-project src/Kayord.POS --output-dir Data/Migrations

dotnet ef database update --project src/Kayord.POS --startup-project src/Kayord.POS

dotnet run --project src/Kayord.POS
```

## Postgres

```bash
docker compose up -d
```
