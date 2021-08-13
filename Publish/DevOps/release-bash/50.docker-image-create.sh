set -e


#---------------------------------------------------------------------
#(x.1)参数
args_="

export basePath=/root/temp/svn


# "

 


#---------------------------------------------------------------------
echo "(x.2)docker-image-create"


publishPath=$basePath/Publish/release/release/publish
dockerPath=$basePath/Publish/release/release/docker-image



echo "copy dir"
mkdir -p $dockerPath
\cp -rf $basePath/Publish/ReleaseFile/docker-image/. $dockerPath


#查找所有需要发布的项目并发布
cd $basePath
for file in $(grep -a '<docker>' . -rl --include *.csproj)
do
	cd $basePath
	
	#get publishName
	publishName=`grep '<publish>' $file -r | grep -oP '>(.*)<' | tr -d '<>'`
	
	#get dockerName
	dockerName=`grep '<docker>' $file -r | grep -oP '>(.*)<' | tr -d '<>'`

	echo create $dockerName

	#copy file
	\cp -rf "$publishPath/$publishName" "$dockerPath/$dockerName/app"
done


