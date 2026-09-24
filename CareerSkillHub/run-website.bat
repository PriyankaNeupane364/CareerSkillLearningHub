@echo off
title Career Skill Hub - Run Website
cd /d "%~dp0"

set "IEXPRESS="
if exist "%ProgramFiles%\IIS Express\iisexpress.exe" set "IEXPRESS=%ProgramFiles%\IIS Express\iisexpress.exe"
if exist "%ProgramFiles(x86)%\IIS Express\iisexpress.exe" set "IEXPRESS=%ProgramFiles(x86)%\IIS Express\iisexpress.exe"

if "%IEXPRESS%"=="" (
    echo IIS Express found chaina. Visual Studio ya IIS Express install garer pheri chalaunu.
    pause
    exit /b 1
)

echo Checking if server already running...
netstat -ano | findstr ":51739" | findstr "LISTENING" >nul
if %errorlevel%==0 (
    echo Server already running chha. Browser khuldai...
    start "" "http://localhost:51739/"
    echo.
    echo NOTE: DUI WOTA bat NA chalau. Tyasto server aru nabhako window close gari 
    echo browser ma http://localhost:51739/  kholeu.
    pause
    exit /b 0
)

echo Killing old IIS Express...
taskkill /f /im iisexpress.exe >nul 2>&1
ping 127.0.0.1 -n 4 >nul

echo Starting Career Skill Hub: http://localhost:51739/
echo.
start "" powershell -WindowStyle Hidden -Command "Start-Sleep -Seconds 8; Start-Process 'http://localhost:51739/'"

"%IEXPRESS%" /path:"%CD%" /port:51739 /clr:v4.0

echo.
echo Server rokkiyo. Pheri chalauna yo file double-click garnu.
pause