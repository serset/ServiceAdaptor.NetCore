set -e



#----------------------------------------------
#(x.1)当前路径 
curWorkDir=$PWD

cd $curWorkDir/../release-bash
export releaseBashPath=$PWD

 


#----------------------------------------------
#(x.2)
cd $releaseBashPath
bash startup.bash





 
#----------------------------------------------
#(x.9)
#cd $curWorkDir
