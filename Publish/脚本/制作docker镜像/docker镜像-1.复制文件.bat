echo '复制文件'

::制作镜像
xcopy  "..\..\02.Gateway\Gateway"   "..\..\06.Docker\制作镜像\adaptor-gateway\gateway" /e /i /r /y



::部署文件
xcopy  "..\..\02.Gateway\Gateway\wwwroot" "..\..\06.Docker\部署文件\wwwroot" /e /i /r /y
xcopy  "..\..\02.Gateway\Gateway\appsettings.json" "..\..\06.Docker\部署文件\" 


