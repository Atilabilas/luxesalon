# Doğrulama ve yayın kaydı

5 Ekim 2026. Yeni sürüm `https://celilatilla.com` adresine yayımlandı. Aşağıdaki yerel laboratuvar sonuçları ve canlı yayın kontrolleri farklı kapsamlardır.

## Canlı yayın — 5 Ekim 2026

- FileZilla’daki kullanıcı tarafından açıkça yetkilendirilen `celil` kaydı kullanıldı. Hedef alan adı ile sunucu eşleşmesi ve mevcut win-x86 / .NET 8.0.22 mimarisi doğrulandı.
- 557 paket dosyası karşılaştırıldı: 464 dosya aynı kaldı, 4 dosya güncellendi, 89 dosya eklendi.
- Önce değişen uygulama dosyaları yedeklendi; kısa bakım sırasında mevcut SQLite veritabanının soğuk yedeği alındı. Veritabanı veya kullanıcı yüklemeleri yayın paketinden sunucuya taşınmadı.
- Yüklenen 93 dosya sunucudan geri okunarak SHA-256 ile paketle karşılaştırıldı; tamamı eşleşti.
- `appsettings.json`, `appsettings.Development.json` ve `web.config` içerikleri okunmadı, yüklenmedi veya değiştirilmedi. Sunucudaki dosya boyutları ve değişiklik tarihleri yayın öncesi/sonrası aynı kaldı. Yereldeki 13 mevcut config dosyasının SHA-256 özeti de değişmedi.
- Ana sayfa, hizmet dizini/detayı, İngilizce hizmet dizini, hizmet seçilmiş randevu, sitemap ve robots HTTP 200; bilinmeyen sayfa gerçek HTTP 404 döndürüyor. Yeni şablonun içeriği doğrulandı.
- Canlı sitemapten 54 sayfa ve kullanılan 10 farklı yerel görsel URL’si tarandı: 0 hata. Meta açıklamaları benzersiz, canonical doğru; indeks engeli, kırık iç bağlantı veya yetim sayfa bulunmadı.
- Canlı masaüstü/mobil tarayıcı kontrolü de geçti: ana sayfa, hizmet dizini/detayı ve randevu sayfasında tek H1 ve yatay taşma kontrol edildi; hizmet ID 10 formda seçili, mobil menü açılıyor ve Escape ile kapanıyor. Tarayıcı hatası yok. Canlıda test randevusu gönderilmedi.

Yayın kanıtı ve geri dönüş dosyaları `.preview/deploy-backup-20261005/` altında. Paket `.preview/CelilAtilla-publish-20261005.zip`; arşiv bütünlüğü kontrol edildi. Bu klasörler kaynak kontrolüne eklenmez.

## Kabul edilen yerel kontroller

| Kontrol | Sonuç | Kapsam |
| --- | --- | --- |
| .NET testleri | 20 geçti, 0 başarısız | Canonical/eski URL normalizasyonu, sitemap, yapılandırılmış veri ve görsel URL işleme dahil |
| Sunucu HTML taraması | 54 sayfa, 0 hata | Sitemapteki TR/EN sayfalar; HTTP 200, tek H1, benzersiz meta açıklaması, doğru canonical |
| İndekslenebilirlik | Tarama geçti | Googlebot robots izni; yayımlanan sayfalarda meta veya HTTP başlığında noindex yok |
| Bağlantı/görsel kontrolü | 0 kırık iç bağlantı veya yetim sayfa | Sitemap sayfalarından oluşan bağlantı grafiği; kullanılan 10 farklı yerel görsel URL’si HTTP 200 |
| JSON-LD | JSON sözdizimi geçerli | Sunucunun ürettiği JSON ayrıştırıldı; bu sonuç Google rich result uygunluğu garantisi değildir |
| Masaüstü/mobil tarayıcı | Kontroller geçti | Ana sayfa, salon, hizmet dizini/detayı, blog, randevu; mobil menü ve SSS; İngilizce hizmet; yatay taşma yok |
| Hizmet → randevu | Seçim korundu | Kadın saç kesimi sayfasından hizmet ID 10 formda seçili geldi |
| Randevu gönderimi | HTTP 302, hedef wa.me | Ayrı önizleme veritabanına yazıldı; dış WhatsApp isteği testte yakalandı, kişiye mesaj gönderilmedi |
| Blog → hizmet | İlgili hizmet bağlantıları var | Röfle yazısı balyaj/röfle hizmetlerine ve hizmeti seçilmiş randevuya bağlanıyor |
| Bilinmeyen sayfa | HTTP 404 | Geri dönüş bağlantıları bulunan gerçek hata sayfası; ana sayfaya sahte başarılı yönlendirme yok |
| WebP dönüşümü | 27 yerel görsel | Ana WebP sürümleri 4.384.216 bayttan 1.733.734 bayta indi: %60,5 azalma; responsive varyantlar ayrıca üretildi |
| CMS korunması | İçerik değişmedi | Başlatma öncesi/sonrası ayarlar, içerikler, yazılar, hizmetler ve ekip kayıtlarının mantıksal özeti aynı |

Görsel incelemede fildişi, krem, antrasit ve altın tema; masaüstü bölünmüş giriş; mobil menü; hizmet kartları ve randevu aksiyonları kontrol edildi. Stok salon/işlem görselleri temsili olarak belirtilir. Stok çalışan portreleri gerçek kişiler gibi sunulmaz.

## Yavaş mobil laboratuvar ölçümü

Koşullar: yerel Development sunucusu, Chromium, 390×844 ekran, soğuk önbellek, 150 ms gecikme, 1,6 Mbps indirme, CPU 4 kat yavaşlatma. Her sayfada tek örnek alındı.

| Sayfa | LCP | CLS | Navigation tamamlanması | Aktarılan bayt |
| --- | --- | --- | --- | --- |
| / | 1.216 ms | 0 | 1.694 ms | 252.909 |
| /hizmetler/kadin-sac-kesimi | 1.056 ms | 0 | 1.527 ms | 223.799 |
| /blog | 1.040 ms | 0 | 1.838 ms | 274.687 |

Bu üç örnek yerel LCP < 2 saniye hedefini geçti. Canlı sunucu, CDN, gerçek cihazlar ve kullanıcı etkileşimleri ölçülmedi. INP sonucu yok. Laboratuvar CLS sonucu gerçek kullanıcıların tamamında sıfır kayma olduğunu kanıtlamaz.

## Ham kanıt ve yeniden çalışma

- `.preview/seo-verification.json`: son canlı 54 sayfalık HTTP/HTML taraması.
- `.preview/browser-verification.json`: tarayıcı akışları ve mobil laboratuvar ölçümü.
- `output/playwright/`: masaüstü/mobil ekran görüntüleri.
- `.preview/cms-fingerprint-before.json`: yeniden başlatma öncesi içerik özeti.
- `.preview/production-http-check.json`: yayın öncesindeki eski canlı sürümün sınırlı HTTP kontrolü.
- `.preview/deploy-backup-20261005/deployment.json`: başarılı yayın, dosya bütünlüğü, config metadatası ve canlı HTTP kontrolleri.
- `.preview/live-browser-verification.json`: canlı tarayıcı kontrolleri; `output/playwright/live-*.png`: canlı ekran görüntüleri.

Önizleme için PowerShell’de `./scripts/start-preview.ps1` çalıştırın. Script mevcut veritabanının ayrı kopyasını kullanır. Sunucu açıkken `python scripts/verify-site.py http://localhost:5100` ile taramayı yeniden çalıştırabilirsiniz; Python’un çalışır bir kurulumu gerekir. .NET doğrulaması: `dotnet test LuxeSalon.Tests --no-restore`.

## Açık kalan kabul

1. **Canlı saha performansı:** yeni sürümün yayını tamamlandı; CrUX/gerçek kullanıcı LCP, INP ve CLS verisi henüz doğrulanmadı. Yerel laboratuvar sonuçları bu verinin yerine geçmez.
2. **Search Console:** mülk listesi ve araç yetenek çağrıları 300 saniyede zaman aşımına uğradı. Gerçek mülk/sorgu verisi alınamadı; sitemap gönderilmedi. Çalışan bağlantı ile gönderim ve URL Denetimi gerekir.
3. **İçerik denetimi:** Celil’in biyografisi mevcut doğrulanmış kayıtlara dayanır. Her eski yazıyı Celil’in yazdığı veya kontrol ettiği iddia edilmez. İşlem iddialarının gerçek salon uygulamasıyla editoryal kontrolü ayrı kabul maddesidir.
4. **Yerel güven:** gerçek salon, ekip ve izinli işlem fotoğrafları; Business Profile sahipliği/kategori/hizmet kontrolü gerekir.
5. **Bağlantı kazanımı:** plan hazırlandı; dış kişilere mesaj gönderilmedi, yeni geri bağlantı kazanıldığı iddia edilmez.
6. **Canlı sonuç ölçümü:** hizmet sorgularının gösterim/tıklamaları, telefon/WhatsApp/form talepleri ve gerçekleşen randevular ayrıca izlenmeli. Birinci sıra veya gelir artışı bu testlerle kanıtlanamaz.

Yayın ve ölçüm adımları için [hizmet odaklı yayın kontrol listesi](seo-launch-checklist.md), editoryal kabul için [Google rehberi](../guidelines.md) kullanılır.
