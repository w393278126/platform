set seviceName=ReportMinuteUpdateOnlineCountBatch
set sevicePath=D:\PluService\ReportMinuteUpdateOnlineCountBatch\Plu.Platform.Service.exe
set seviceDesc=ReportMinuteUpdateOnlineCountBatch

@echo off
cls
title Service管理工具
:menu
cls
color 0A
echo.
echo              ==============================
echo              请选择要进行的操作，然后按回车
echo              ==============================
echo.
echo              1.安装(%seviceName%)Service
echo.
echo              2.启动(%seviceName%)Service
echo.
echo              3.停止(%seviceName%)Service
echo.
echo              4.卸载(%seviceName%)Service
echo.
echo              Q.退出
echo.
echo.
:cho

set choice=
set /p choice=       请选择:
IF NOT "%choice%"=="" SET choice=%choice:~0,1%
if /i "%choice%"=="1" goto installScheduler
if /i "%choice%"=="2" goto startScheduler
if /i "%choice%"=="3" goto stopScheduler
if /i "%choice%"=="4" goto uninstallScheduler
if /i "%choice%"=="Q" goto end
echo 选择无效，请重新输入
echo.
goto cho

:installScheduler
sc create %seviceName% binPath= %sevicePath% start= auto displayname= %seviceName%
sc description %seviceName% %seviceDesc%
goto cho

:startScheduler
sc start %seviceName%
goto cho

:stopScheduler
sc stop %seviceName%
goto cho

:uninstallScheduler
sc delete %seviceName%
goto cho

:error
echo 未能找到服务文件!
goto cho

:end
exit