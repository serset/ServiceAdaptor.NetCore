:: @echo off

cd /d �ű�\�����ű� 

:: ���з���
:: for /R %%s in (����-*) do (   
::  start "����" "%%s"
:: )  

:: ���з���
for /R %%s in (����-*) do (   
 call "%%s"
)  


echo �������
echo �������
echo �������

:: pause