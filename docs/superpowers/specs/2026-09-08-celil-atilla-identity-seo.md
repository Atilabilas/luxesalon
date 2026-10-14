# Celil & Atilla — kimlik, hizmet, SEO

**Tarih:** 2026-09-08  
**Yaklaşım:** A — mevcut ASP.NET CMS iskeleti kalır. NAP, metin, hizmet, schema, blog yenilenir. Görsel palet aynı.

## Amaç

`celilatilla.com` üzerindeki uydurma Alsancak / sahte telefon / fiyat / yanlış Instagram kaydını kesmek. Google’a tek doğru işletme sinyali vermek. Lüksü katalog ve abartıyla değil, sade doğru metinle kurmak.

## Kilidi tutulmuş gerçekler

| Alan | Değer |
|------|--------|
| Marka | Celil & Atilla (soyad yok) |
| Kuruluş | 2000 |
| Salon | Tek: Kazımdirik, Gediz Cd. 21 A, 35100 Bornova / İzmir |
| Telefon / WhatsApp | 0534 794 43 93 |
| E-posta | Sitede yok (form + WhatsApp) |
| Instagram | https://www.instagram.com/celil_atilla/ |
| Domain | https://celilatilla.com |
| Maps | https://share.google/NykmYWxlZspy7lvUt |
| Saat | Pzt–Cmt 08:30–20:00, Pazar 10:00–19:00 |
| Dil | Türkçe |
| Fiyat | Halka kapalı |
| Botoks / dolgu | Yalnızca saç işlemi; iğne / tıbbi estetik yok |
| Atilla | Markada; ekip kartında yok |
| Ekip (ad) | Celil, Bülent, Tuğba, Sevgül, Rame, Irmak |
| Sosyal kanıt | Sahte yorum/ödül kapalı; bloklar boş kalabilir |
| Mağaza | Sayfa durur, “yakında” |
| Palet | Fildişi / charcoal / gold |

## Bilinçli sınırlar

- Her tekniğe ayrı URL yok. 9 grup, grup içinde 1–4 kart. Ombre / sombre / balyaj aynı renk sayfasında.
- “En iyi”, “uluslararası”, “28+ yıl”, “15 bin müşteri”, sahte ödül yok.
- Hotmail sitede görünmez.
- Kalıcı oje / protez tırnak yok.

## Sayfa ağacı (URL’ler korunur)

- `/` kadın önde (kesim, renk, bakım, erkek+çocuk kartı)
- `/Hakkimizda`
- `/Hizmetler` — 9 grup
- `/Blog`, `/Blog/{id}`
- `/Randevu`
- `/Shop` (boş)
- `/Privacy`

## Hizmet grupları

1. Kadın Kesim — kesim, kahkül, fön, beach waves  
2. Renklendirme — tek renk+kök, röfle, balyaj/ombre/sombre, pastel/düzeltme  
3. Bakım — keratin; saç botoksu+dolgu; protein/nem/deri; nanoplasti/perma/biaform/Brezilya fönü  
4. Saç Kaynağı  
5. Tırnak — klasik manikür; klasik + spa pedikür  
6. Kaş, Kirpik & Ağda  
7. Özel Gün — gelin saçı; makyaj  
8. Erkek — kesim/fade; sakal/tıraş; boya  
9. Çocuk — kız + erkek kesim  

## SEO

- Title kalıbı: `{sayfa} | Celil & Atilla Kuaför Bornova`
- Tek NAP, HairSalon + LocalBusiness JSON-LD, `sameAs` Instagram + Maps
- E-posta ve sahte aggregateRating yok
- `/sitemap.xml`, `robots.txt`
- Blog: local niyet (Bornova + işlem), abartısız karşılaştırma yazıları
- Canlı DB’deki seed `if (Any()) return` yüzünden ezilmez; seeder **upsert / refresh** yapar

## Başarı

Yayında adres Bornova Gediz 21 A, telefon 0534, Instagram `celil_atilla`, fiyat yok, Alsancak yok. Hizmetler 9 grup. Blog local küme. Schema doğrulanabilir.
