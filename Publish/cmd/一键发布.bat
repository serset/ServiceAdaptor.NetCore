
call "nuget-pack.bat"

call "dotnet-publish.bat"

call "docker-image-create.bat"

xcopy  "../ReleaseFile/docker-deploy" "../release/release/docker-deploy"  /e /i /r /y



echo %~n0.bat Ö´ÐÐ³É¹¦£¡

pause