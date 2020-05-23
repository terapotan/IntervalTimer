@echo off
echo プロジェクトスクリプトのインストールを開始します……
git clone git@github.com:terapotan/projectScripts.git projectScripts
set USER_INPUT=
set /P USER_INPUT="新規作成する環境名を入力してください。>"
cd ./projectScripts
git checkout %USER_INPUT%
cd ../
python ./projectScripts/initialSetting.py

pause

del /f "%~dp0%~nx0"