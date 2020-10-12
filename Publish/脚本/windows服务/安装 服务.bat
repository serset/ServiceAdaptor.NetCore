@echo off

set groupName=ServiceAdaptor

:: installService uninstallService startService stopService
set action=installService



::(x.1)获取nssm的路径
cd /D %~dp0
set nssm=%~dp0nssm.exe

::(x.2)获取publish文件夹的路径
set basePath=%~dp0
cd /d ../..
set publishPath=%cd%
cd /d %basePath%
 
::(x.3)获取管理员权限
if "%1%" neq "noadmin" (
echo 获取管理员权限
call:GetAdmin
) 


::(x.4)处理
call:%action% "01.ServiceCenter" "App.ServiceCenter.dll" "\01.ServiceCenter\Sers\ServiceCenter" 
call:%action% "02.Gateway" "ServiceAdaptor.NetCore.Gateway.dll" "\02.Gateway\Gateway"

call:%action% "11.ServiceProvider" "ServiceProvider.dll" "\04.服务站点\11.ServiceProvider\ServiceProvider"
call:%action% "12.ServiceConsumer" "ServiceConsumer.dll" "\04.服务站点\12.ServiceConsumer\ServiceConsumer"
 

::(x.5)提示成功并结束
echo 操作成功！ 
::pause
:: 延时10s
ping /n 10 127.0.0.1>nul
exit
 





::(x.6)以下为函数

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
echo 安装服务：%serviceName%

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
echo 卸载服务：%serviceName%

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
echo 停止服务：%serviceName%

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
echo 开始服务：%serviceName%

"%nssm%" start "%serviceName%"

goto:eof
::-- end
::--------------------------------------------------------






::-------------------------------------------------------------------------------
::-- Function GetAdmin  获取管理员权限
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


