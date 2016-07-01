## The Windows Service

The [Windows Service](https://msdn.microsoft.com/en-us/library/zt39148a.aspx) hosts the WebAPI using OWIN

/api/ping - should return pong

There is a userAppSettings.config file that controls how the service runs

```XML
<appSettings>
  <add key="port" value="9021" />
  <add key="useHTTPS" value="false" />
  <add key="useLocalhost" value="true" />
  <add key="asConsole" value="true" />
</appSettings>
```

* port = the port you want to listen on
* useHTTPS = true to listen using *https* rather than *http*
* useLocalhost = true to listen on *localhost* rather than the machines IP address
* asConsole = run as a console application rather than a Windows Service, easier for debugging

### Installation
This is performed using the .NET [installutil](https://msdn.microsoft.com/en-us/library/50614e95.aspx "Microsoft.NET InstallUtil" target="_blank")

#### Install
```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil /username=domain\username /password=password /unattended ExampleService.exe 
```

#### Uninstall
```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil /u ExampleService.exe 
```
