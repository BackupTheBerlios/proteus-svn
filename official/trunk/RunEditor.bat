@echo off
SET OLDDIR=%CD%
cd %1
cd Bin
cd Debug
Proteus.Host.Editor
chdir /d %OLDDIR%
