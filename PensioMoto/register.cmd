SET GACUTIL="C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\gacutil.exe"

SET REGASM="C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\regasm.exe"

 

%REGASM% PensioMoto.dll /tlb:PensioMoto.tlb

%GACUTIL% /i PensioMoto.dll

 

%REGASM% PensioMoto.dll /tlb:PensioMoto.tlb

%GACUTIL% /i PensioMoto.dll