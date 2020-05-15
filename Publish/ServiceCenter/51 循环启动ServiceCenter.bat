title Sers-ServiceCenter

cd /d Sers\ServiceCenter

:begin
dotnet App.ServiceCenter.dll

 TIMEOUT /T 1
@echo restart
goto begin
 