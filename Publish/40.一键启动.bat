cd /d �ű�\�����ű�
 

for /R %%s in (*-����1-*) do (   
 start /min "" ""%%~nxs"
)   

TIMEOUT /T 1

for /R %%s in (*-����2-*) do (   
 start /min "" ""%%~nxs"
)     

cd /d ../..

 

