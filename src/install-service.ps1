# install
sc.exe create "Basalt.RemoteCommandService" binPath="C:\Program Files\dotnet\dotnet.exe W:\GitHub\wen-yan\basalt-remotecommandservice\src\Basalt.RemoteCommandService\bin\Debug\net9.0-windows\Basalt.RemoteCommandService.exe"

# start
sc.exe start "Basalt.RemoteCommandService"

# stop
sc.exe stop "Basalt.RemoteCommandService"

# uninstall
sc.exe delete "Basalt.RemoteCommandService"

# query
sc.exe query "Basalt.RemoteCommandService"