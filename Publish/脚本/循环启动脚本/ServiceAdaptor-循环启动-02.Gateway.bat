title ServiceAdaptor-Ñ­»·Æô¶¯-02.Gateway

cd /d ..\..\02.Gateway\Gateway

:begin

 dotnet ServiceAdaptor.NetCore.Gateway.dll
 TIMEOUT /T 4

@echo restart
goto begin