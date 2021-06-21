set -e

# cd /root/temp/svn/Publish/DevOps/github-bash;bash startup.bash;



#----------------------------------------------
#(x.1)µ±Ç°Â·¾¶


export name=ServiceAdaptor

#export DOCKER_USERNAME=serset
#export DOCKER_PASSWORD=xxx

#export NUGET_SERVER=https://api.nuget.org/v3/index.json
#export NUGET_KEY=xxxxxxxxxx

#export export GIT_SSH_SECRET=xxxxxx




#----------------------------------------------
#(x.2)bash

for file in *.sh
do
    echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    echo bash $file
    bash $file
done







