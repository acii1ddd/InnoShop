#!/bin/bash

if [ -z "$1" ]; then
  echo "❌ Укажи имя миграции как аргумент!"
  echo "Пример: ./add-migration.sh InitialCreate"
  exit 1
fi

MIGRATION_NAME=$1

dotnet ef migrations add "$MIGRATION_NAME" \
  -s "$HOME/pre-trainee/InnoShop/src/UserService/UserService.API/" \
  -p "$HOME/pre-trainee/InnoShop/src/UserService/UserService.Infrastructure/"
  --output-dir Data/Migrations

