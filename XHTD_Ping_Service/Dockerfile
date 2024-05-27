# escape=\

FROM microsoft/dotnet-framework:4.7.2-sdk-windowsservercore-1803 AS build

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY ["XHTD_Ping_Service/bin/Debug/", "/Service/"]

WORKDIR "C:/Service/"

RUN "C:/Service/InstallUtil.exe" /LogToConsole=true /ShowCallStack XHTD_Ping_Service.exe; \
    Set-Service -Name "\"XHTD_Ping_Service\"" -StartupType Automatic; \
    Set-ItemProperty "\"Registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XHTD_Ping_Service\"" -Name AllowRemoteConnection -Value 1

ENTRYPOINT ["powershell"]
CMD Start-Service \""XHTD_Ping_Service\""; \
    Get-EventLog -LogName System -After (Get-Date).AddHours(-1) | Format-List ;\
    $idx = (get-eventlog -LogName System -Newest 1).Index; \
    while ($true) \
    {; \
    start-sleep -Seconds 1; \
    $idx2  = (Get-EventLog -LogName System -newest 1).index; \
    get-eventlog -logname system -newest ($idx2 - $idx) |  sort index | Format-List; \
    $idx = $idx2; \
    }