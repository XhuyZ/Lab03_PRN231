#!/bin/bash

# Exit on error
set -e

# Read migration name from first argument
MIGRATION_NAME="init"

if [ -z "$MIGRATION_NAME" ]; then
  echo "❌ Please provide a migration name."
  echo "Usage: ./migration.sh InitialCreate"
  exit 1
fi

# Step 1: Ensure tool manifest exists
if [ ! -f ".config/dotnet-tools.json" ]; then
  echo "🔧 Creating tool manifest..."
  dotnet new tool-manifest
fi

# Step 2: Install EF Core tools locally
echo "🔧 Installing dotnet-ef..."
dotnet tool install --local dotnet-ef

# Step 3: Locate .csproj under any */DAL/ directory
DAL_CSPROJ=$(find . -maxdepth 1 -type f -name "*.csproj" | head -n 1)

if [ -z "$DAL_CSPROJ" ]; then
  echo "❌ Could not find any .csproj file under a DAL folder."
  exit 1
fi

# Step 4: Locate the DbContext class file
DB_CONTEXT_FILE=$(find "$(dirname "$DAL_CSPROJ")" -type f -name '*Context.cs' | head -n 1)

if [ -z "$DB_CONTEXT_FILE" ]; then
  echo "❌ Could not find a *Context.cs file under the DAL project."
  exit 1
fi

# Extract context class name (assumes "public class XxxContext")
CONTEXT_CLASS=$(grep -Eo 'public class [A-Za-z0-9_]+' "$DB_CONTEXT_FILE" | awk '{print $3}' | head -n 1)

if [ -z "$CONTEXT_CLASS" ]; then
  echo "❌ Could not determine DbContext class name from $DB_CONTEXT_FILE"
  exit 1
fi

# Step 5: Run migration command
echo "🚀 Running EF Core migration for: $CONTEXT_CLASS"

dotnet tool run dotnet-ef migrations add "$MIGRATION_NAME" \
  --project "$DAL_CSPROJ" \
  --context "$CONTEXT_CLASS"
# Step 5: Drop current database
echo "🚀 Dropping current database for: $CONTEXT_CLASS"

dotnet tool run dotnet-ef database drop --project "$DAL_CSPROJ" --context "$CONTEXT_CLASS" --force --no-build
# Step 6: Update the database
echo "🚀 Updating database for: $CONTEXT_CLASS"

dotnet tool run dotnet-ef database update \
  --project "$DAL_CSPROJ" \
  --context "$CONTEXT_CLASS"
