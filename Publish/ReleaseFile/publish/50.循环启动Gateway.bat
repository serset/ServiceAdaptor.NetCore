title Gateway

cd /d Gateway 

:begin

dotnet ServiceAdaptor.NetCore.Gateway.dll
 TIMEOUT /T 4

@echo restart
goto begin
