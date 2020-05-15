title 发布Test

cd /d ..\Test\ServiceProvider
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\..\Publish\ServiceProvider
cd /d ..


cd /d ..\Test\ServiceConsumer
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\..\Publish\ServiceConsumer
cd /d ..



echo '发布成功！'


:: pause

exit