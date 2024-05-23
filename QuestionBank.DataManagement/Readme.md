PGSQL Setup
========================
1. Install PGSQL First
2. Create a login user questionbank with password postgres

Migration 
==========================
Add-Migration <MigrationName> -StartupProject QuestionBank.Api -Project  QuestionBank.DataManagement

Update-Database -StartupProject QuestionBank.Api -Project QuestionBank.DataManagement

Remove-Migration -StartupProject QuestionBank.Api -Project QuestionBank.DataManagement