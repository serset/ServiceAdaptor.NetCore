@echo off 

dotnet nuget delete ServiceAdaptor.NetCore 1.0.5.83 -k ee28314c-f7fe-2550-bd77-e09eda3d0119  -s http://nuget.sers.cloud:8 --non-interactive

dotnet nuget delete ServiceAdaptor.NetCore.Be.Eureka 1.0.5.83 -k ee28314c-f7fe-2550-bd77-e09eda3d0119  -s http://nuget.sers.cloud:8 --non-interactive

dotnet nuget delete ServiceAdaptor.NetCore.Consul 1.0.5.83 -k ee28314c-f7fe-2550-bd77-e09eda3d0119  -s http://nuget.sers.cloud:8 --non-interactive

dotnet nuget delete ServiceAdaptor.NetCore.MinHttp 1.0.5.83 -k ee28314c-f7fe-2550-bd77-e09eda3d0119  -s http://nuget.sers.cloud:8 --non-interactive

dotnet nuget delete ServiceAdaptor.NetCore.Sers 1.0.5.83 -k ee28314c-f7fe-2550-bd77-e09eda3d0119  -s http://nuget.sers.cloud:8 --non-interactive

echo 'delete succeed£¡'
 

 