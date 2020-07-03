cd /d 脚本\启动脚本
 

for /R %%s in (*-启动1-*) do (   
 start /min "" ""%%~nxs"
)   

TIMEOUT /T 1

for /R %%s in (*-启动2-*) do (   
 start /min "" ""%%~nxs"
)     

cd /d ../..

 

