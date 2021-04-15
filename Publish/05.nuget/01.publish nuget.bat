title "publish nuget"

cd /d ../../Library

:: echo 'pack ServiceAdaptor'
:: cd /d ServiceAdaptor
:: dotnet build --configuration Release
:: dotnet pack --configuration Release --output ..\..\Publish\05.nuget\nuget 
:: cd /d ../

echo 'pack ServiceAdaptor.NetCore'
cd /d ServiceAdaptor.NetCore
dotnet build --configuration Release
dotnet pack --configuration Release --output ..\..\Publish\05.nuget\nuget  
cd /d ../

echo 'pack ServiceAdaptor.NetCore.MinHttp'
cd /d ServiceAdaptor.NetCore.MinHttp
dotnet build --configuration Release
dotnet pack --configuration Release --output ..\..\Publish\05.nuget\nuget  
cd /d ../

echo 'pack ServiceAdaptor.NetCore.Sers'
cd /d ServiceAdaptor.NetCore.Sers
dotnet build --configuration Release
dotnet pack --configuration Release --output ..\..\Publish\05.nuget\nuget  
cd /d ../

echo 'pack ServiceAdaptor.NetCore.Consul'
cd /d ServiceAdaptor.NetCore.Consul
dotnet build --configuration Release
dotnet pack --configuration Release --output ..\..\Publish\05.nuget\nuget  
cd /d ../

:: echo 'pack ServiceAdaptor.NetCore.Be.Eureka'
:: cd /d ServiceAdaptor.NetCore.Be.Eureka
:: dotnet build --configuration Release
:: dotnet pack --configuration Release --output ..\..\Publish\05.nuget\nuget  
:: cd /d ../

cd /d ..\Publish\05.nuget

echo 'publish nuget succeed��' 