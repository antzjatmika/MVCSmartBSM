Informasi Patch0320
===================
A. Aplikasi Web SMART BSM terdiri dari 2 project
a. Aplikasi Web Service : MVCSmartAPI01
b. Aplikasi Client : MVCSmartClient01

Aplikasi MVCSmartClient01 memiliki web reference ke MVCSmartAPI01 (konfigurasi nya ada di bagian Konfigurasi MVCSmartClient01 point 2)
Masing-masing aplikasi memiliki konfigurasi yang perlu disesuaikan 
dengan lingkungan tempat aplikasi dipasang, diantaranya :
a. Konfigurasi MVCSmartAPI01 (Web.config)
1. connectionStrings, terdapat 5 connectionString yang harus diganti, menyesuaikan dengan setting database di server
  <connectionStrings>
    <add name="DefaultConnection" connectionString="KONEKSI_DB" providerName="System.Data.SqlClient" />
    <add name="DB_SMART_OWIN" connectionString="KONEKSI_DB" providerName="System.Data.SqlClient" />
    <add name="DB_SMART" connectionString="KONEKSI_DB" providerName="System.Data.SqlClient" />
    <add name="DB_SMARTEntities" connectionString="KONEKSI_DB" providerName="System.Data.EntityClient" />
    <add name="DB_SMARTEntities1" connectionString="KONEKSI_DB" providerName="System.Data.EntityClient" />
  </connectionStrings>
  
  contoh isian bisa dilihat di file Web.Debug.config

b. Konfigurasi MVCSmartClient01 (Web.config)
1. connectionStrings, terdapat 1 connectionString yang harus diganti, menyesuaikan dengan setting database di server
  <connectionStrings>
    <!--DEVELOPMENT-->
    <add name="DB_Connect" connectionString="kk" providerName="System.Data.SqlClient" />
  </connectionStrings>

  contoh isian bisa dilihat di file Web.Debug.config
  
2. appSettings, terdapat 1 key yang harus diganti, menyesuaikan dengan alamat webservice MVCSmartAPI01, yang ada terpasang di server
  <appSettings>
	...
	<add key="SmartAPIUrl" value="ALAMAT_WEBSERVICE" />
	...
  </appSettings>

  contoh isian bisa dilihat di file Web.Debug.config

B. Database Web SMART BSM
Aplikasi ini mengggunakan SQL Server sebagai databasenya.
Backup databasenya ada di folder Database.

Terimakasih.
Jika ada pertanyaan, terkait dengan petunjuk pemasangan aplikasi ini bisa email ke antzjatmika@gmail.com
