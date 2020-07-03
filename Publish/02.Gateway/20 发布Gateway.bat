title 发布Gateway

cd /d ..\..\02.Gateway\ServiceAdaptor.NetCore.Gateway
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\..\Publish\02.Gateway\Gateway
cd /d ..\..\Publish\02.Gateway


echo '发布成功！'


:: pause

exit