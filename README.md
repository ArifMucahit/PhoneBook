Projeyi ayağa kaldırmak için öncelikle her 3 proje için appsettings.json dosyasındaki connection stringleri kendi veritabanımıza bakacak şekilde düzenlememiz gerekiyor.

Ardından Contact ve Report service web apiları için "Package Manager Console" üzerinden "update-database" komutunun çalıştırılması gerekir. Bu proje içerisinde görebileceğiniz "migration" dosyalarını çalıştırarak veritabanlarını oluşturur. 

Projelerin üçünü ayağa kaldırdıktan sonra Contact service swagger üzerinden ekleme/çıkarma işlemleri yapılabilir, report service üzerinden rapor talep edilip, talep edilen raporlar takip edilebilir. 

Contact ve Report servis kendi üzerinde alınan exceptionları "ErrorLog.txt" dosyasına kaydeder.
