set _date=%date:/=%
set _time=%time::=%
set _time=%_time:.=%
set dateTime=%_date%%_time%

@echo New DbMgr %dateTime%

dotnet ef migrations add '%dateTime%'

REM dotnet ef migrations script -o 'db_%dateTime%';

@echo OK
