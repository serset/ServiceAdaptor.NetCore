title ServiceAdaptor-Ñ­»·Æô¶¯-01.ServiceCenter

cd /d ..\..\01.ServiceCenter\Sers\ServiceCenter

:begin

 dotnet App.ServiceCenter.dll
 TIMEOUT /T 4

@echo restart
goto begin