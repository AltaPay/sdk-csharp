SET GACUTIL="C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\gacutil.exe"

SET REGASM="C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\regasm.exe"

 
%REGASM% /u AltaPayMoto.dll 

%GACUTIL% /u AltaPayMoto.dll


%REGASM% AltaPayMoto.dll /tlb:AltaPayMoto.tlb

%GACUTIL% /i AltaPayMoto.dll

 

%REGASM% AltaPayMoto.dll /tlb:AltaPayMoto.tlb

%GACUTIL% /i AltaPayMoto.dll