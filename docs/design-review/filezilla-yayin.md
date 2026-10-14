# Onaylı ana sayfanın FileZilla yayını

5 Ekim 2026. Kullanıcı yüklemeyi FileZilla ile kendisi yapacak. Bu paket henüz canlıya yüklenmedi.

- Hazır klasör: `Publish-Onayli-Tasarim-20261005/`
- ZIP: `CelilAtilla-onayli-tasarim-publish-20261005.zip`
- Mimari: win-x86, self-contained, .NET 8.0.22; önceki yayınla aynı.
- Paket: 558 dosya, 57.092.735 bayt ZIP. Arşiv bütünlük kontrolü geçti.
- Pakette appsettings, web.config, veritabanı, kullanıcı yüklemeleri ve anahtarlar yok.

## Yükleme

1. FileZilla'da mevcut sitenin uygulama köküne gidin: `LuxeSalon.Web.dll` ve `web.config` bulunan klasör. Mevcut uygulama dosyalarının yedeğini alın.
2. DLL'ler kullanımda olduğundan, yükleme öncesinde uygulama köküne geçici `app_offline.htm` dosyası yükleyin. Örnek dosya `.preview/yayin-bakim/app_offline.htm` altında hazırdır.
3. Hazır klasörün **içindekileri** aynı sunucu klasörüne aktarın; klasörün kendisini iç içe yüklemeyin. Aynı adlı uygulama dosyalarının üzerine yazın. Sunucu klasörünü boşaltmayın; mevcut `appsettings*`, `web.config`, `App_Data`, `uploads`, log ve anahtar klasörlerini koruyun.
4. Aktarım kuyruğu tamamlanıp başarısız aktarım kalmadığında, yalnız geçici `app_offline.htm` dosyasını kaldırın.
5. Siteyi açıp Ctrl+F5 ile yenileyin. Ana sayfa dört bölümden oluşmalı; rehber ayrı sayfada kalmalı. Ana sayfa → rehber → ana sayfa ve randevu bağlantılarını kontrol edin.

## Yerel doğrulama

- Release publish başarılı. Mevcut AppointmentsController nullable uyarıları devam ediyor; derleme hatası yok.
- Mevcut .NET testleri: 20 geçti, 0 başarısız.
- Ana sayfa 390, 910 ve 1440 px genişliklerde kontrol edildi: yatay taşma yok, tek H1, dört ana bölüm.
- Masaüstü açılış fotoğrafı 220 px yüksekliğinde.
- Mobil rehberden menü ile ana sayfaya dönüş ve randevu sayfasına geçiş doğrulandı. Randevu gönderilmedi.
- Ana sayfa canonical adresi `https://celilatilla.com/`.
- SEO 98/100 skoru yeniden ölçülmedi. Canlı yayın ve canlı doğrulama kullanıcı yüklemesinden sonra yapılabilir.
