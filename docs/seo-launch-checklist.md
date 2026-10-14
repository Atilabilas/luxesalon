# Hizmet odaklı SEO — canlıya geçiş ve ölçüm

5 Ekim 2026. Hedef: bilgi arayan blog ziyaretçisi yanında, hizmet arayan kişinin salonu bulması ve randevu talebi oluşturması.

## Kanıt, çıkarım, açık iş

- **Kanıt:** yeni sürüm 5 Ekim 2026’da canlıya yayımlandı. Yeni şablon, hizmet/randevu sayfaları, robots ve sitemap doğrulandı; canlı 54 sayfalık tarama 0 hatayla geçti. Sunucudaki appsettings ve web.config korunarak 93 dosya yüklendi. [Yayın kaydı](verification.md)
- **Kanıt:** yerel sürüm sunucuda Razor Pages ile HTML üretiyor. 20 test ve sitemapten 54 sayfalık tarama geçti.
- **Kullanıcı gözlemi:** Google’dan gelenler ağırlıklı olarak blog okuyor; hizmet için gelen yok.
- **Çıkarım:** hizmet görünürlüğü ve randevuya geçişi birlikte iyileştirmek gerekir. Sorunun yalnız metin uzunluğu olduğunu kanıtlayacak veri henüz yok.
- **Açık iş:** Search Console bağlantısı zaman aşımına uğradı. Sorgu/sayfa verisi alınamadı ve sitemap gönderimi doğrulanamadı.

## Bu sürümde uygulananlar

| İş | Durum / sınır |
| --- | --- |
| Hizmetleri ana sayfa ve menüde öne çıkarma | Uygulandı |
| Hizmet dizini | İşlem kartları, doğrudan randevu; diğer işlemler açılabilir gruplarda |
| Hizmet detayları | Kısa kapsam, adres, ücret görüşmesi, randevu ve WhatsApp ilk bölümde |
| Blog → hizmet | Gerçek etiket eşleşmesiyle ilgili hizmete bağlantı |
| Hizmet → randevu | Seçilen hizmet formda hazır |
| Canonical / hreflang / meta / H1 | Sunucu HTML’sinde; tarama ile kontrol |
| Eski URL / dil / www / HTTPS | Uygulama seviyesinde tek 301; proxy/CDN ayrıca canlıda kontrol edilmeli |
| 404 | Kaldırılmış/bilinmeyen sayfa 404; kullanıcıya geri dönüş yolu |
| Sitemap / robots | Canonical yayımlanmış URL’ler; halka açık sayfalar Googlebot’a açık |
| Yazar / yayıncı | Celil için gerçek kayıtlarla profil; eski yazıların yazarı topluca değiştirilmedi |
| WebP / responsive görseller | 27 yerel görsel; ana WebP sürümleri toplamda %60,5 daha küçük |
| Görsel güveni | Yanlış stok eşleşmeleri düzeltildi; gerçek salon galerisi için yüklenmiş görsel gerekir |
| CMS korunması | Mevcut veritabanında başlangıç verileri tekrar yazılmıyor |
| SSS | Görünür SSS ile uyumlu schema; Google FAQ rich result vaadi yok |

Boş mağaza ve hata sayfasındaki noindex korunur. Admin kimlik doğrulamayla korunur. Bu istisnalar hizmet/makale sayfalarının indekslenmesini engellemez.

## Canlıya geçiş sırası

1. Mevcut sürüm ve veritabanı yedeğini alın. Güncel kodu yayınlayın; veritabanını örnek veriyle değiştirmeyin.
2. HTTPS, kök alan adı, TR/EN hizmet URL’leri, randevu ve eski URL’leri dışarıdan kontrol edin. IIS/CDN’nin ikinci bir yönlendirme ekleyip eklemediğine bakın.
3. Search Console’da **hesabın döndürdüğü gerçek mülk adını** kullanın. Mülk adını tahmin etmeyin.
4. `https://celilatilla.com/sitemap.xml` gönderin. Başarılı API yanıtını veya Sitemaps raporundaki kabulü kaydedin. robots.txt’de bildirilmesi, API gönderiminin başarılı olduğu anlamına gelmez.
5. Aşağıdaki hizmet sayfalarını URL Denetimi ile kontrol edin; canonical, son tarama, indeks durumu ve render edilmiş içeriği kaydedin.
6. Mobil PageSpeed/CrUX verisini alın. Yerel laboratuvar sonucu, canlı saha verisi yerine geçmez.

## Hizmet aramalarını teşhis etme

Son 90 günü önceki 90 günle karşılaştırın. Blog ve hizmet URL’lerini ayrı gruplarda inceleyin; marka sorgularını ayrıca ayırın.

| İhtiyaç / sorgu örneği | Öncelikli URL |
| --- | --- |
| Bornova kuaför | / |
| Bornova kadın saç kesimi | /hizmetler/kadin-sac-kesimi |
| Bornova balyaj / ombre | /hizmetler/balyaj-ombre-sombre |
| Bornova keratin bakımı | /hizmetler/keratin-sac-botoksu |
| Bornova gelin saçı | /hizmetler/gelin-saci |
| Bornova erkek kesim / fade | /hizmetler/erkek-kesim-fade |

Diğer hizmetlerin gerçek URL’leri sitemapten alınır. Bunlar hedeflenen niyet örnekleridir; arama hacmi veya sıra iddiası değildir.

- **Gösterim yoksa:** önce indeks, canonical, erişim, yerel işletme bilgisi ve sayfanın hizmet kapsamı.
- **Gösterim var, tıklama azsa:** sorgu ile sayfa başlığı/özetinin uyumu.
- **Tıklama var, talep azsa:** randevu yolu, gerçek iş örnekleri, güven bilgisi ve mobil kullanım.
- **Blog talep getirmiyorsa:** ilgili hizmet bağlantısının ve randevu mesajının kullanımı; işe yaramayan yazı kopyalarını birleştirme kararını veriyle verin.

Ölçüm kaydında gösterim/tıklama yanında telefon tıklaması, WhatsApp tıklaması, form talebi ve gerçekten gerçekleşen randevu ayrı tutulur. Bir tıklama otomatik olarak kazanılmış müşteri sayılmaz. Analytics kimliği ve izin düzeni doğrulanmadan yeni izleme kurulduğu iddia edilmez.

## Google Business Profile

Gerçek işletme adı, tek salon adresi, telefon, çalışma/özel gün saatleri ve hizmetleri siteyle karşılaştırın. Profil sahipliği ve kategori, profil ekranında doğrulanmalı. Gerçek salon ve izinli işlem fotoğrafları ekleyin; gerçek müşteri değerlendirmelerine yanıt verin.

Google yerel sonuçları alaka, uzaklık ve tanınırlıkla ilişkilendirir. Daha iyi yerel sıra satın alınamaz veya talep edilemez. [Google’ın yerel sıralama açıklaması](https://support.google.com/business/answer/7091?hl=en)

## Kaliteli bağlantı kazanma — uygulanacak işler

Bu çalışma bağlantı kazanıldığını iddia etmez; üçüncü taraflara mesaj gönderilmedi.

1. Gerçek salon/işlem görselleri ve Celil’in doğruladığı kısa bir vaka hazırlayın. İzin, tarih, başlangıç durumu ve işlemin sınırları açık olsun.
2. Gerçek ilişki varsa düğün fotoğrafçısı, gelinlik işletmesi veya yerel etkinlik ortağına faydalı ortak kaynak önerin. Önce ilişkiyi ve doğru kişiyi doğrulayın.
3. İşletmenin gerçekten üye olduğu mesleki/yerel kuruluş kaydı varsa isim, adres ve site bağlantısını düzeltin.
4. Gerçek bir yerel haber veya mesleki deneyim paylaşımı için özgün kaynak sunun. Yayıncı kendi editoryal kararıyla bağlantı verir.
5. Link satın alma, otomatik dizin paketi veya zorunlu karşılıklı link sözleşmesi kullanmayın. Sponsorluk varsa niteliği açıklayın ve uygun link işaretlemesi kullanın.

Gönderim için seçilmiş muhataplar, gerçek ilişki ve kullanıcı talimatı gerekir. Uydurma ortaklık, değerlendirme veya geçmiş oluşturulmaz. [Google bağlantı spam politikası](https://developers.google.com/search/docs/essentials/spam-policies)

## Yerel önizleme

PowerShell’de `./scripts/start-preview.ps1` çalıştırın. Site `http://localhost:5100` adresindedir. Önizleme ayrı veritabanı kopyası kullanır; üretim veritabanı üzerinde test randevusu oluşturulmaz.

`scripts/verify-site.py` Python standart kütüphanesiyle çalışır. `scripts/optimize-images.py` Pillow gerektirir ve orijinal görselleri korur. Güncel test ve tarama çıktıları tasarımın Google’da ilk sıraya çıkacağını kanıtlamaz.

