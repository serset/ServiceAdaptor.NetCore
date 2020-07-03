
cd /d ..\..\02.Gateway\ServiceAdaptor.NetCore.Gateway
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\..\Publish\02.Gateway\Gateway
cd /d ..\..\Publish\02.Gateway


:: pause
:: exit