title ServiceAdaptor-循环启动-11.ServiceProvider

cd /d ..\..\04.服务站点\11.ServiceProvider\ServiceProvider

:begin

 dotnet ServiceProvider.dll
 TIMEOUT /T 4

@echo restart
goto begin