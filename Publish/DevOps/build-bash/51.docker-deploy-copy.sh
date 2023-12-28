set -e


#---------------------------------------------------------------------
#(x.1)参数
args_="

export basePath=/root/temp/svn

# "

 
#---------------------------------------------------------------------
#(x.2)
publishPath="$basePath/Publish/release/release/Station(net6.0)"
deployPath=$basePath/Publish/release/release/docker-deploy



#----------------------------------------------
echo "(x.3)copy dir"
\cp -rf "$basePath/Publish/ReleaseFile/docker-deploy/." "$deployPath"



 
