@echo off

::(x.1)��ȡcodePath
set curPath=%cd%
cd /d "%~dp0"
cd /d ../../..
set codePath=%cd%
set nugetPath=%codePath%/Publish/release/release/nuget

::(x.2)����������Ҫ����nuget����Ŀ������
for /f "delims=" %%f in ('findstr /M /s /i "<pack/>" *.csproj') do (
	echo pack %codePath%\%%f\..
	cd /d "%codePath%\%%f\.."
	dotnet build --configuration Release
	dotnet pack --configuration Release --output "%nugetPath%"
	@if errorlevel 1 (echo . & echo .  & echo �������Ų飡& pause) 
)


echo %~n0.bat ִ�гɹ���


cd /d "%curPath%"
