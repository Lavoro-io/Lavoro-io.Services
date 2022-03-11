set _date=%date:/=%
set _time=%time::=%
set _time=%_time:.=%
set _time=%_time:,=%
set dateTime=%_date%%_time%
set dateTime=%dateTime: =%

@echo New DbMgr %dateTime%

dotnet build

dotnet ef migrations add %dateTime%

dotnet ef migrations script %dateTime% -o "..\MigrationsScript\db_%dateTime%.txt"

@echo OK
