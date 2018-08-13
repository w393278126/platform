set module=webapiapp,webapicomment,webapiconfig,webapievent,webapifeed,webapigift,webapihttp,webapispam,webapitask,webapimusic,webapiprivate,webapiroom,webapistream,webapimb,webapiuser,webapi,webadmin,weblive,webpayment,websetting,webuser,webuserzone,webvideo,webapitag

@echo off
cls
title 项目管理工具
:menu
cls
color 0A
echo.
echo              ==============================
echo              请选择要进行的操作，然后按回车
echo              ==============================
echo.
echo              1.删除网站和应用程序池[以管理员身份运行]
echo.
echo              2.发布网站和应用程序池[以管理员身份运行]
echo.
echo              3.MsBuild[Path设置C:\Program Files (x86)\MSBuild\14.0\Bin\或C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\]
echo.
echo              Q.退出
echo.
echo.
:cho

set choice=
set /p choice=       请选择:
IF NOT "%choice%"=="" SET choice=%choice:~0,1%
if /i "%choice%"=="1" goto delete
if /i "%choice%"=="2" goto publish
if /i "%choice%"=="3" goto msbuild
if /i "%choice%"=="Q" goto end
echo 选择无效，请重新输入
echo.
goto cho

:delete
SET /p preName=请输入站点前缀(可选):
for %%g in (%module%) do (
  C:\Windows\System32\inetsrv\appcmd delete apppool %preName%%%g
  C:\Windows\System32\inetsrv\appcmd delete site %preName%%%g
)
goto cho

:publish
SET /p pathchoice=选择路径(1:发布环境,2:开发环境):
if /i "%pathchoice%"=="1" goto pathpublish
if /i "%pathchoice%"=="2" goto pathlocal

:pathpublish
set basepath=C:\platform\publish\
set webapiapppath=%basepath%debug.webapiapp
set webapicommentpath=%basepath%debug.webapicomment
set webapiconfigpath=%basepath%debug.webapiconfig
set webapieventpath=%basepath%debug.webapievent
set webapifeedpath=%basepath%debug.webapifeed
set webapigiftpath=%basepath%debug.webapigift
set webapihttppath=%basepath%debug.webapihttp
set webapispampath=%basepath%debug.webapispam
set webapitaskpath=%basepath%debug.webapitask
set webapimusicpath=%basepath%debug.webapimusic
set webapiprivatepath=%basepath%debug.webapiprivate
set webapiroompath=%basepath%debug.webapiroom
set webapistreampath=%basepath%debug.webapistream
set webapimbpath=%basepath%debug.webapimb
set webapiuserpath=%basepath%debug.webapiuser
set webapisportpath=%basepath%debug.webapisport
set webapipath=%basepath%debug.webapi
set webadminpath=%basepath%debug.webadmin
set weblivepath=%basepath%debug.weblive
set webpaymentpath=%basepath%debug.webpayment
set websettingpath=%basepath%debug.websetting
set webuserpath=%basepath%debug.webuser
set webuserzonepath=%basepath%debug.webuserzone
set webvideopath=%basepath%debug.webvideo
set webapitagpath=%basepath%debug.webapitag
goto pathin

:pathlocal
set basepath=C:\platform\
set webapiapppath=%basepath%app\Plu.Platform.Presentation.webapiapp
set webapicommentpath=%basepath%comment\Plu.Platform.Presentation.webapicomment
set webapiconfigpath=%basepath%config\Plu.Platform.Presentation.webapiconfig
set webapieventpath=%basepath%interaction\Plu.Platform.Presentation.webapievent
set webapifeedpath=%basepath%interaction\Plu.Platform.Presentation.webapifeed
set webapigiftpath=%basepath%interaction\Plu.Platform.Presentation.webapigift
set webapihttppath=%basepath%interaction\Plu.Platform.Presentation.webapihttp
set webapispampath=%basepath%interaction\Plu.Platform.Presentation.webapispam
set webapitaskpath=%basepath%interaction\Plu.Platform.Presentation.webapitask
set webapimusicpath=%basepath%live\Plu.Platform.Presentation.webapimusic
set webapiprivatepath=%basepath%live\Plu.Platform.Presentation.webapiprivate
set webapiroompath=%basepath%live\Plu.Platform.Presentation.webapiroom
set webapistreampath=%basepath%live\Plu.Platform.Presentation.webapistream
set webapimbpath=%basepath%messagebus\Plu.Platform.Presentation.webapimb
set webapiuserpath=%basepath%user\Plu.Platform.Presentation.webapiuser
set webapisportpath=%basepath%sport\Plu.Platform.Presentation.webapisport
set webapipath=%basepath%live\Plu.Platform.Presentation.webapi
set webadminpath=%basepath%houtai\Plu.Platform.Presentation.admin
set weblivepath=%basepath%live\Plu.Platform.Presentation.weblive
set webpaymentpath=%basepath%payment\Plu.Platform.Presentation.webpayment
set websettingpath=%basepath%setting\Plu.Platform.Presentation.websetting
set webuserpath=%basepath%user\Plu.Platform.Presentation.webuser
set webuserzonepath=%basepath%user\Plu.Platform.Presentation.webuserzone
set webvideopath=%basepath%video\Plu.Platform.Presentation.webvideo
set webapitagpath=%basepath%tag\Plu.Platform.Presentation.webapitags
goto pathin

:pathin

SET /p testchoice=test域名前缀(例如test1.):
set webapiappbind=http/*:80:%testchoice%a1.plu.cn,http/*:80:%testchoice%a2.plu.cn,http/*:80:%testchoice%a1.longzhu.com,http/*:80:%testchoice%a2.longzhu.com
set webapicommentbind=http/*:80:%testchoice%comment.plu.cn,http/*:80:%testchoice%comment.longzhu.com
set webapiconfigbind=http/*:80:%testchoice%configapi.plu.cn,http/*:80:%testchoice%configapi.longzhu.com
set webapieventbind=http/*:80:%testchoice%eventapi.plu.cn,http/*:80:%testchoice%eventapi.longzhu.com
set webapifeedbind=http/*:80:%testchoice%feedapi.plu.cn,http/*:80:%testchoice%feedapi.longzhu.com
set webapigiftbind=http/*:80:%testchoice%giftapi.plu.cn,http/*:80:%testchoice%giftapi.longzhu.com
set webapihttpbind=http/*:80:%testchoice%qqvip.api.plu.cn,http/*:80:%testchoice%qqvipapi.longzhu.com
set webapispambind=http/*:80:%testchoice%spamapi.plu.cn,http/*:80:%testchoice%spamapi.longzhu.com
set webapitaskbind=http/*:80:%testchoice%task.u.plu.cn,http/*:80:%testchoice%tasku.longzhu.com
set webapimusicbind=http/*:80:%testchoice%musicapi_corp.plu.cn,http/*:80:%testchoice%musicapi_corp.longzhu.com
set webapiprivatebind=http/*:80:%testchoice%private.api.plu.cn,http/*:80:%testchoice%privateapi.longzhu.com
set webapiroombind=http/*:80:%testchoice%roomapicdn.plu.cn,http/*:80:%testchoice%rankapi.longzhu.com,http/*:80:%testchoice%roomapicdn.longzhu.com
set webapistreambind=http/*:80:%testchoice%livestream.plu.cn,http/*:80:%testchoice%livestreamcdn.plu.cn,http/*:80:%testchoice%livestream.longzhu.com,http/*:80:%testchoice%livestreamcdn.longzhu.com
set webapimbbind=http/*:80:%testchoice%mb.tga.plu.cn,http/*:80:%testchoice%mbtga.longzhu.com,http/*:80:%testchoice%livemb.longzhu.com
set webapiuserbind=http/*:80:%testchoice%userapi.plu.cn,http/*:80:%testchoice%userapi.longzhu.com
set webapisportbind=http/*:80:%testchoice%sportapi.plu.cn,http/*:80:%testchoice%sportapi.longzhu.com
set webapibind=http/*:80:%testchoice%star.api.plu.cn,http/*:80:%testchoice%star.apicdn.plu.cn,http/*:80:%testchoice%liveapi.plu.cn,http/*:80:%testchoice%starapi.longzhu.com,http/*:80:%testchoice%starapicdn.longzhu.com,http/*:80:%testchoice%liveapi.longzhu.com
set webadminbind=http/*:80:%testchoice%admin.plu.cn
set weblivebind=http/*:80:%testchoice%star.longzhu.com
set webpaymentbind=http/*:80:%testchoice%pay.plu.cn,http/*:80:%testchoice%pay.longzhu.com
set websettingbind=http/*:80:%testchoice%setting.longzhu.com
set webuserbind=http/*:80:%testchoice%i.longzhu.com
set webuserzonebind=http/*:80:%testchoice%zone.longzhu.com
set webvideobind=http/*:80:%testchoice%v.longzhu.com
set webapitagbind=http/*:80:%testchoice%tagapi.plu.cn,http/*:80:%testchoice%tagapi.longzhu.com
goto publishsite

:publishsite
SET /p preName=请输入站点前缀(可选):
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapiapp /physicalPath:%webapiapppath% /bindings:%webapiappbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapicomment /physicalPath:%webapicommentpath% /bindings:%webapicommentbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapiconfig /physicalPath:%webapiconfigpath% /bindings:%webapiconfigbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapievent /physicalPath:%webapieventpath% /bindings:%webapieventbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapifeed /physicalPath:%webapifeedpath% /bindings:%webapifeedbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapigift /physicalPath:%webapigiftpath% /bindings:%webapigiftbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapihttp /physicalPath:%webapihttppath% /bindings:%webapihttpbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapispam /physicalPath:%webapispampath% /bindings:%webapispambind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapitask /physicalPath:%webapitaskpath% /bindings:%webapitaskbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapimusic /physicalPath:%webapimusicpath% /bindings:%webapimusicbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapiprivate /physicalPath:%webapiprivatepath% /bindings:%webapiprivatebind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapiroom /physicalPath:%webapiroompath% /bindings:%webapiroombind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapistream /physicalPath:%webapistreampath% /bindings:%webapistreambind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapimb /physicalPath:%webapimbpath% /bindings:%webapimbbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapiuser /physicalPath:%webapiuserpath% /bindings:%webapiuserbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapisport /physicalPath:%webapisportpath% /bindings:%webapisportbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapi /physicalPath:%webapipath% /bindings:%webapibind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webadmin /physicalPath:%webadminpath% /bindings:%webadminbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%weblive /physicalPath:%weblivepath% /bindings:%weblivebind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webpayment /physicalPath:%webpaymentpath% /bindings:%webpaymentbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%websetting /physicalPath:%websettingpath% /bindings:%websettingbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webuser /physicalPath:%webuserpath% /bindings:%webuserbind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webuserzone /physicalPath:%webuserzonepath% /bindings:%webuserzonebind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webvideo /physicalPath:%webvideopath% /bindings:%webvideobind%
C:\Windows\System32\inetsrv\appcmd add site /name:%preName%webapitag /physicalPath:%webapitagpath% /bindings:%webapitagbind%

for %%g in (%module%) do (
  C:\Windows\System32\inetsrv\appcmd add apppool /name:%preName%%%g /managedRuntimeVersion:v4.0 /managedPipelineMode:Integrated
  C:\Windows\System32\inetsrv\appcmd set site /site.name:%preName%%%g /[path='/'].applicationPool:%preName%%%g
)

goto cho

:msbuild
SET /p configchoice=选择配置(1:Debug,2:Release):
if /i "%configchoice%"=="1" goto msbuilddebug
if /i "%configchoice%"=="2" goto msbuildrelease

:msbuilddebug
set config=Debug
goto msbuildcore

:msbuildrelease
set config=Release
goto msbuildcore

:msbuildcore
set basepath=%cd%\publish\
set webapiapppath=%basepath%debug.webapiapp
set webapicommentpath=%basepath%debug.webapicomment
set webapiconfigpath=%basepath%debug.webapiconfig
set webapieventpath=%basepath%debug.webapievent
set webapifeedpath=%basepath%debug.webapifeed
set webapigiftpath=%basepath%debug.webapigift
set webapihttppath=%basepath%debug.webapihttp
set webapispampath=%basepath%debug.webapispam
set webapitaskpath=%basepath%debug.webapitask
set webapimusicpath=%basepath%debug.webapimusic
set webapiprivatepath=%basepath%debug.webapiprivate
set webapiroompath=%basepath%debug.webapiroom
set webapistreampath=%basepath%debug.webapistream
set webapimbpath=%basepath%debug.webapimb
set webapiuserpath=%basepath%debug.webapiuser
set webapisportpath=%basepath%debug.webapisport
set webapipath=%basepath%debug.webapi
set webadminpath=%basepath%debug.webadmin
set weblivepath=%basepath%debug.weblive
set webpaymentpath=%basepath%debug.webpayment
set websettingpath=%basepath%debug.websetting
set webuserpath=%basepath%debug.webuser
set webuserzonepath=%basepath%debug.webuserzone
set webvideopath=%basepath%debug.webvideo
set webapitagpath=%basepath%debug.webapitag
MSBuild.exe ..\app\Plu.Platform.Presentation.webapiapp\Plu.Platform.Presentation.webapiapp.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapiapppath%
MSBuild.exe ..\comment\Plu.Platform.Presentation.webapicomment\Plu.Platform.Presentation.webapicomment.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapicommentpath%
MSBuild.exe ..\config\Plu.Platform.Presentation.webapiconfig\Plu.Platform.Presentation.webapiconfig.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapiconfigpath%
MSBuild.exe ..\interaction\Plu.Platform.Presentation.webapievent\Plu.Platform.Presentation.webapievent.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapieventpath%
MSBuild.exe ..\interaction\Plu.Platform.Presentation.webapifeed\Plu.Platform.Presentation.webapifeed.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapifeedpath%
MSBuild.exe ..\interaction\Plu.Platform.Presentation.webapigift\Plu.Platform.Presentation.webapigift.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapigiftpath%
MSBuild.exe ..\interaction\Plu.Platform.Presentation.webapihttp\Plu.Platform.Presentation.webapihttp.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapihttppath%
MSBuild.exe ..\interaction\Plu.Platform.Presentation.webapispam\Plu.Platform.Presentation.webapispam.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapispampath%
MSBuild.exe ..\interaction\Plu.Platform.Presentation.webapitask\Plu.Platform.Presentation.webapitask.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapitaskpath%
MSBuild.exe ..\live\Plu.Platform.Presentation.webapimusic\Plu.Platform.Presentation.webapimusic.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapimusicpath%
MSBuild.exe ..\live\Plu.Platform.Presentation.webapiprivate\Plu.Platform.Presentation.webapiprivate.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapiprivatepath%
MSBuild.exe ..\live\Plu.Platform.Presentation.webapiroom\Plu.Platform.Presentation.webapiroom.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapiroompath%
MSBuild.exe ..\live\Plu.Platform.Presentation.webapistream\Plu.Platform.Presentation.webapistream.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapistreampath%
MSBuild.exe ..\messagebus\Plu.Platform.Presentation.webapimb\Plu.Platform.Presentation.webapimb.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapimbpath%
MSBuild.exe ..\user\Plu.Platform.Presentation.webapiuser\Plu.Platform.Presentation.webapiuser.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapiuserpath%
MSBuild.exe ..\sport\Plu.Platform.Presentation.WebApiSport\Plu.Platform.Presentation.WebApiSport.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapisportpath%
MSBuild.exe ..\live\Plu.Platform.Presentation.webapi\Plu.Platform.Presentation.webapi.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapipath%
MSBuild.exe ..\houtai\Plu.Platform.Presentation.admin\Plu.Platform.Presentation.admin.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webadminpath%
MSBuild.exe ..\live\Plu.Platform.Presentation.weblive\Plu.Platform.Presentation.weblive.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%weblivepath%
MSBuild.exe ..\payment\Plu.Platform.Presentation.webpayment\Plu.Platform.Presentation.webpayment.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webpaymentpath%
MSBuild.exe ..\setting\Plu.Platform.Presentation.websetting\Plu.Platform.Presentation.websetting.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%websettingpath%
MSBuild.exe ..\user\Plu.Platform.Presentation.webuser\Plu.Platform.Presentation.webuser.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webuserpath%
MSBuild.exe ..\user\Plu.Platform.Presentation.webuserzone\Plu.Platform.Presentation.webuserzone.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webuserzonepath%
MSBuild.exe ..\video\Plu.Platform.Presentation.webvideo\Plu.Platform.Presentation.webvideo.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webvideopath%
MSBuild.exe ..\tag\Plu.Platform.Presentation.webapitags\Plu.Platform.Presentation.webapitags.csproj /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet /p:VisualStudioVersion=15.0 /p:SolutionDir=%cd% /t:WebPublish /p:Configuration=%config% /p:WebPublishMethod=FileSystem /p:publishUrl=%webapitagpath%
goto cho

:error
echo 错误
goto cho

:end
exit
