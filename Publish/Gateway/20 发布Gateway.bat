title ����Gateway

cd /d ..\..\ServiceAdaptor.NetCore.Gateway
dotnet build --configuration Release
dotnet publish --configuration Release --output ..\Publish\Gateway\Gateway
cd /d ../Publish


echo '�����ɹ���'


:: pause

exit