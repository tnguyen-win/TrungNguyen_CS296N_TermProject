@echo off

echo ----------------
echo 1 - call dotnet watch run
echo 2 - [ local - reset and update database / migrations ]
echo 3 - call dotnet build
echo 4 - call dotnet test
echo 8 - [ remote - update database ]
echo 10 - call git reset --soft HEAD~1
echo ----------------

set /P input="ENTER: "

if %input% == 1 (
	call dotnet watch run
)

if %input% == 2 (
    call mysql -u johnsmith -pSecret!123 -e "DROP SCHEMA IF EXISTS gametrading;"

    call dotnet ef migrations remove

    call dotnet ef migrations add Identity

    call dotnet ef database update
)

if %input% == 3 (
    call dotnet build
)

if %input% == 4 (
    call dotnet test
)

if %input% == 8 (
    call mysql -u aa4c8e_trading -pSecret!123 -h MYSQL5045.site4now.net -P 3306 -e "DROP SCHEMA IF EXISTS db_aa4c8e_trading;"

    call dotnet ef database update --connection "server=MYSQL5045.site4now.net;port=3306;user=aa4c8e_trading;password=Secret!123;database=db_aa4c8e_trading;"
)

if %input% == 10 (
	call git reset --soft HEAD~1
)

echo ----------------

echo FINISHED

pause
