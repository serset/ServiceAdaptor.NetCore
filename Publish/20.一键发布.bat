:: @echo off

cd /d 脚本\发布脚本 

:: 并行发布
:: for /R %%s in (发布-*) do (   
::  start "发布" "%%s"
:: )  

:: 串行发布
for /R %%s in (发布-*) do (   
 call "%%s"
)  


echo 发布完成
echo 发布完成
echo 发布完成

:: pause