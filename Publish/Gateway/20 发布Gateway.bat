title 发布Gateway

cd /d ..\..\ServiceAdaptor.NetCore.Gateway
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\Publish\Gateway\Gateway
cd /d ../Publish


echo '发布成功！'


:: pause

exit