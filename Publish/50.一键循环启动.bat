cd /d 脚本\循环启动脚本

for /R %%s in (*) do (   
 start /min "" "%%~nxs"
)    

cd /d ../..



