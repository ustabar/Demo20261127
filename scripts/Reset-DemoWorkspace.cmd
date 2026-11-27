@echo off
setlocal
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Reset-DemoWorkspace.ps1" %*
exit /b %ERRORLEVEL%
