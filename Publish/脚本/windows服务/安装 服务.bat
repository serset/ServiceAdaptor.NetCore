@echo off

set groupName=ServiceAdaptor

:: installService uninstallService startService stopService
set action=installService



::(x.1)��ȡnssm��·��
cd /D %~dp0
set nssm=%~dp0nssm.exe

::(x.2)��ȡpublish�ļ��е�·��
set basePath=%~dp0
cd /d ../..
set publishPath=%cd%
cd /d %basePath%
 
::(x.3)��ȡ����ԱȨ��
if "%1%" neq "noadmin" (
echo ��ȡ����ԱȨ��
call:GetAdmin
) 


::(x.4)����
call:%action% "01.ServiceCenter" "App.ServiceCenter.dll" "\01.ServiceCenter\Sers\ServiceCenter" 
call:%action% "02.Gateway" "ServiceAdaptor.NetCore.Gateway.dll" "\02.Gateway\Gateway"

call:%action% "11.ServiceProvider" "ServiceProvider.dll" "\04.����վ��\11.ServiceProvider\ServiceProvider"
call:%action% "12.ServiceConsumer" "ServiceConsumer.dll" "\04.����վ��\12.ServiceConsumer\ServiceConsumer"
 

::(x.5)��ʾ�ɹ�������
echo �����ɹ��� 
::pause
:: ��ʱ10s
ping /n 10 127.0.0.1>nul
exit
 





::(x.6)����Ϊ����

::--------------------------------------------------------
::-- Function installService
::-- demo   call:installService "01.ServiceCenter" "App.ServiceCenter.dll" "\01.ServiceCenter\Sers\ServiceCenter"
::-- start

:installService

set appName=%~1
set dllName=%~2
set relativePath=%~3

set serviceName=%groupName%-%appName%
set servicePath=%publishPath%%relativePath%

echo ------
echo ��װ����%serviceName%

"%nssm%" install "%serviceName%" dotnet.exe
"%nssm%" set "%serviceName%" AppParameters "%dllName%"
"%nssm%" set "%serviceName%" AppDirectory "%servicePath%"
"%nssm%" set "%serviceName%" AppStdout "%servicePath%\Logs\nssm.log"
"%nssm%" set "%serviceName%" AppStderr "%servicePath%\Logs\nssm-err.log"

goto:eof
::-- end
::--------------------------------------------------------

 



::--------------------------------------------------------
::-- Function uninstallService
::-- demo   call:uninstallService "01.ServiceCenter"
::-- start

:uninstallService

set appName=%~1
set serviceName=%groupName%-%appName%

echo ------
echo ж�ط���%serviceName%

"%nssm%" stop "%serviceName%"
"%nssm%" remove "%serviceName%" confirm

goto:eof
::-- end
::--------------------------------------------------------






::--------------------------------------------------------
::-- Function stopService
::-- demo   call:stopService "01.ServiceCenter"
::-- start

:stopService

set appName=%~1
set serviceName=%groupName%-%appName%

echo ------
echo ֹͣ����%serviceName%

"%nssm%" stop "%serviceName%"

goto:eof
::-- end
::--------------------------------------------------------




::--------------------------------------------------------
::-- Function startService
::-- demo   call:startService "01.ServiceCenter"
::-- start

:startService

set appName=%~1
set serviceName=%groupName%-%appName%

echo ------
echo ��ʼ����%serviceName%

"%nssm%" start "%serviceName%"

goto:eof
::-- end
::--------------------------------------------------------






::-------------------------------------------------------------------------------
::-- Function GetAdmin  ��ȡ����ԱȨ��
::-- demo   call:GetAdmin

:GetAdmin

>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system" 
 
if '%errorlevel%' NEQ '0' (  
    goto GetAdmin_UACPrompt  
) else ( goto GetAdmin_gotAdmin )  
   
:GetAdmin_UACPrompt  
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs" 
    echo UAC.ShellExecute "%~s0", "", "", "runas", 1 >> "%temp%\getadmin.vbs" 
    "%temp%\getadmin.vbs"  
    exit
   
:GetAdmin_gotAdmin
    if exist "%temp%\getadmin.vbs" ( del "%temp%\getadmin.vbs" )  
    pushd "%CD%" 
    CD /D "%~dp0"  

goto:eof
::-- end
::--------------------------------------------------------


