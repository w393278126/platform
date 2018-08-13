set module=app,batch,build,comment,config,data,domain,houtai,infrastructure,interaction,live,messagebus,payment,search,setting,user,video
set name=

@echo off
cls
title Git管理工具
:menu
cls
color 0A
echo.
echo              ==============================
echo              请选择要进行的操作，然后按回车
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
echo              Q.退出
echo.
echo.
:cho

set choice=
set /p choice=       请选择:
IF NOT "%choice%"=="" SET choice=%choice:~0,1%
if /i "%choice%"=="0" goto clone
if /i "%choice%"=="1" goto pull
if /i "%choice%"=="2" goto commit
if /i "%choice%"=="3" goto push
if /i "%choice%"=="4" goto checkout
if /i "%choice%"=="5" goto merge
if /i "%choice%"=="6" goto msbuild
if /i "%choice%"=="Q" goto end
echo 选择无效，请重新输入
echo.
goto cho

:clone
set name=clone 
goto exclone
goto cho

:pull
SET /p branchName=请输入分支名(可选):
set name=pull -v --progress "origin" %branchName%
goto ex
goto cho

:commit
SET /p message=请输入Message:
set name=commit -m %message%
goto ex
goto cho

:push
set name=push
goto excommit
goto cho

:checkout
SET /p branchName=请输入分支名:
set name=checkout %branchName%
goto ex
goto cho

:merge
SET /p branchName=请输入分支名:
set name=merge %branchName%
goto ex
goto cho

:error
echo 错误
goto cho

:exclone

for %%g in (%module%) do (
    cd %cd%
    echo .....................
    echo 处理%%g模块中...
    echo .....................
    git %name% git@git.corp.plu.cn:pluplatform/%%g.git
)
goto cho

:ex

for %%g in (%module%) do (
    cd %cd%\%%g
    echo .....................
    echo 处理%%g模块中...
    echo .....................
    git %name%
    cd ..
)
goto cho


:excommit

for %%g in (%module%) do (
    cd %cd%\%%g
    echo .....................
    echo 处理%%g模块中...
    echo .....................
    git add -A
    git %name%
    cd ..
)
goto cho

:end
exit
