set module=app,batch,build,comment,config,data,domain,houtai,infrastructure,interaction,live,messagebus,payment,search,setting,user,video
set name=

@echo off
cls
title Git������
:menu
cls
color 0A
echo.
echo              ==============================
echo              ��ѡ��Ҫ���еĲ�����Ȼ�󰴻س�
echo              ==============================
echo.
echo              0.git clone
echo.
echo              1.git pull
echo.
echo              2.git commit
echo.
echo              3.git push
echo.
echo              4.git checkout
echo.
echo              5.git merge
echo.
echo              Q.�˳�
echo.
echo.
:cho

set choice=
set /p choice=       ��ѡ��:
IF NOT "%choice%"=="" SET choice=%choice:~0,1%
if /i "%choice%"=="0" goto clone
if /i "%choice%"=="1" goto pull
if /i "%choice%"=="2" goto commit
if /i "%choice%"=="3" goto push
if /i "%choice%"=="4" goto checkout
if /i "%choice%"=="5" goto merge
if /i "%choice%"=="6" goto msbuild
if /i "%choice%"=="Q" goto end
echo ѡ����Ч������������
echo.
goto cho

:clone
set name=clone 
goto exclone
goto cho

:pull
SET /p branchName=�������֧��(��ѡ):
set name=pull -v --progress "origin" %branchName%
goto ex
goto cho

:commit
SET /p message=������Message:
set name=commit -m %message%
goto ex
goto cho

:push
set name=push
goto excommit
goto cho

:checkout
SET /p branchName=�������֧��:
set name=checkout %branchName%
goto ex
goto cho

:merge
SET /p branchName=�������֧��:
set name=merge %branchName%
goto ex
goto cho

:error
echo ����
goto cho

:exclone

for %%g in (%module%) do (
    cd %cd%
    echo .....................
    echo ����%%gģ����...
    echo .....................
    git %name% git@git.corp.plu.cn:pluplatform/%%g.git
)
goto cho

:ex

for %%g in (%module%) do (
    cd %cd%\%%g
    echo .....................
    echo ����%%gģ����...
    echo .....................
    git %name%
    cd ..
)
goto cho


:excommit

for %%g in (%module%) do (
    cd %cd%\%%g
    echo .....................
    echo ����%%gģ����...
    echo .....................
    git add -A
    git %name%
    cd ..
)
goto cho

:end
exit
