echo '�����ļ�'

::��������
mklink /j "..\..\06.Docker\��������\adaptor_gateway\root\app\Gateway" "..\..\02.Gateway\Gateway"



::�����ļ�
xcopy  "..\..\..\02.Gateway\ServiceAdaptor.NetCore.Gateway\wwwroot" "..\..\06.Docker\�����ļ�\wwwroot" /e /i /r /y
xcopy  "..\..\..\02.Gateway\ServiceAdaptor.NetCore.Gateway\appsettings.json" "..\..\06.Docker\�����ļ�\" 


