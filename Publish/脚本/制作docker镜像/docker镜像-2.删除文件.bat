echo '删除文件'
 
::制作镜像
rd /s/q "..\..\06.Docker\制作镜像\adaptor_gateway\root\app\Gateway" 




::部署文件
rd /s/q "..\..\06.Docker\部署文件\wwwroot"
del "..\..\06.Docker\部署文件\appsettings.json" 