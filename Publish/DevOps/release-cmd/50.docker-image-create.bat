@echo off

::���ñ����ӳ�
setlocal EnableDelayedExpansion


echo %~n0.bat start...


::(x.1)��ȡcodePath
set curPath=%cd%
cd /d "%~dp0"
cd /d ../../..
set codePath=%cd%
set publishPath=%codePath%/Publish/release/release/publish
set dockerPath=%codePath%/Publish/release/release/docker-image


rd /s /q "%dockerPath%"

::(x.2)copy dir
xcopy "%codePath%/Publish/ReleaseFile/docker-image" "%dockerPath%" /e /i /r /y



::(x.3)����������Ҫ��������Ŀ������
for /f "delims=" %%f in ('findstr /M /s /i "<docker>" *.csproj') do (
	::get publishName
	for /f "tokens=3 delims=><" %%a in ('type "%codePath%\%%f"^|findstr "<publish>.*publish"') do set publishName=%%a
	::get dockerName
	for /f "tokens=3 delims=><" %%a in ('type "%codePath%\%%f"^|findstr "<docker>.*docker"') do set dockerName=%%a

	echo create !dockerName!

	::copy file
	xcopy "%publishPath%/!publishName!" "%dockerPath%/!dockerName!/app" /e /i /r /y
)





echo %~n0.bat ִ�гɹ���

cd /d "%curPath%"