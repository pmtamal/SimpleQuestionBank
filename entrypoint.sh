#!/bin/bash
set -e

echo "Running database migrations..."
dotnet ef database update --project src/QuestionBank.DataManagement --startup-project src/QuestionBank.Api


echo "Starting the application..."
exec dotnet QuestionBank.Api.dll
