echo '�����ļ�'

::��������
xcopy  "..\..\02.Gateway\Gateway"   "..\..\06.Docker\��������\adaptor-gateway\gateway" /e /i /r /y



::�����ļ�
xcopy  "..\..\02.Gateway\Gateway\wwwroot" "..\..\06.Docker\�����ļ�\wwwroot" /e /i /r /y
xcopy  "..\..\02.Gateway\Gateway\appsettings.json" "..\..\06.Docker\�����ļ�\" 


