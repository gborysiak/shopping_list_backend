---
name: add-migration
description: Add an EF Core migration and regenerate deploy.sql
disable-model-invocation: true
argument-hint: <MigrationName>
---

1. `dotnet ef migrations add $ARGUMENTS` (name like the existing `versionNN` scheme if none given: next number after the latest in `Migrations/`).
2. `dotnet ef migrations script --idempotent -o deploy.sql`
3. Show `git status` so the user can review the new migration, snapshot and deploy.sql. Do not apply to a database.
