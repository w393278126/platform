set seviceName=ReportMinuteUpdateOnlineCountBatch
set sevicePath=D:\PluService\ReportMinuteUpdateOnlineCountBatch\Plu.Platform.Service.exe
set seviceDesc=ReportMinuteUpdateOnlineCountBatch

@echo off
cls
title Service������
:menu
cls
color 0A
echo.
echo              ==============================
echo              ��ѡ��Ҫ���еĲ�����Ȼ�󰴻س�
echo              ==============================
echo.
echo              1.��װ(%seviceName%)Service
echo.
echo              2.����(%seviceName%)Service
echo.
echo              3.ֹͣ(%seviceName%)Service
echo.
echo              4.ж��(%seviceName%)Service
echo.
echo              Q.�˳�
echo.
echo.
:cho

set choice=
set /p choice=       ��ѡ��:
IF NOT "%choice%"=="" SET choice=%choice:~0,1%
if /i "%choice%"=="1" goto installScheduler
if /i "%choice%"=="2" goto startScheduler
if /i "%choice%"=="3" goto stopScheduler
if /i "%choice%"=="4" goto uninstallScheduler
if /i "%choice%"=="Q" goto end
echo ѡ����Ч������������
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
echo δ���ҵ������ļ�!
goto cho

:end
exit