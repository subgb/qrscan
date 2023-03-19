## Read QR Code from Screen

merge zxing.dll into qrscan.exe
```powershell
cd bin\Release
ILMerge.exe /ndebug /log /targetplatform:v4 /out:qrscan-merged.exe qrscan.exe zxing.dll
````
