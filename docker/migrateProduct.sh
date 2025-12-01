#!/bin/bash

if [ -z "$1" ]; then
  echo "Укажи имя миграции как аргумент!"
  exit 1
fi

MIGRATION_NAME=$1

dotnet ef migrations add "$MIGRATION_NAME" \
  -s "$HOME/pre-trainee/InnoShop/src/ProductService/ProductService.API/" \
  -p "$HOME/pre-trainee/InnoShop/src/ProductService/ProductService.Infrastructure/" \
  --output-dir Data/Migration
