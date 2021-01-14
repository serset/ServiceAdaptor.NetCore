echo '复制文件'

::制作镜像
mklink /j "..\..\06.Docker\制作镜像\adaptor_gateway\root\app\Gateway" "..\..\02.Gateway\Gateway"



::部署文件
xcopy  "..\..\..\02.Gateway\ServiceAdaptor.NetCore.Gateway\wwwroot" "..\..\06.Docker\部署文件\wwwroot" /e /i /r /y
xcopy  "..\..\..\02.Gateway\ServiceAdaptor.NetCore.Gateway\appsettings.json" "..\..\06.Docker\部署文件\" 


