title ����Test

cd /d ..\Test\ServiceProvider
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\..\Publish\ServiceProvider
cd /d ..


cd /d ..\Test\ServiceConsumer
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\..\Publish\ServiceConsumer
cd /d ..



echo '�����ɹ���'


:: pause

exit