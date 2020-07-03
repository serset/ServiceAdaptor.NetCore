title ServiceAdaptor-循环启动-12.ServiceConsumer

cd /d ..\..\04.服务站点\12.ServiceConsumer\ServiceConsumer

:begin

 dotnet ServiceConsumer.dll
 TIMEOUT /T 4

@echo restart
goto begin