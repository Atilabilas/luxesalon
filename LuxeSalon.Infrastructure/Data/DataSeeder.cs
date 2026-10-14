using LuxeSalon.Core.Entities;
using LuxeSalon.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Infrastructure.Data;

public static class DataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        UpsertSettings(context);
        UpsertSiteContents(context);
        RefreshServices(context);
        RefreshServiceLandings(context);
        RefreshTeam(context);
        RefreshBlog(context);
        DeactivatePublicPrices(context);
        DeactivateSocialProof(context);
        RefreshMarquee(context);
        RefreshStats(context);
        RemoveFakeProducts(context);
        SeedSiteImages(context);
        RefreshGallery(context);
        context.SaveChanges();
    }

    static void UpsertSettings(ApplicationDbContext context)
    {
        var seeds = new (string Key, string Value, string Description, string Group)[]
        {
            ("Site_Name", "Celil & Atilla", "Site Adı", "General"),
            ("Site_Title", "Celil & Atilla | Saç Tasarımı ve Bakım", "Tarayıcı Başlığı", "General"),
            ("Site_Url", "https://celilatilla.com", "Canonical kök", "SEO"),
            ("SEO_Description", "Celil & Atilla, Bornova’nın Kazımdirik Mahallesi’nde kadın saç kesimi, balyaj, renklendirme ve bakım hizmetleri sunar. Erkek ve çocuk randevusu da alınır.", "Meta Açıklama", "SEO"),
            ("SEO_Default_Og_Image", "/images/services/kadin-kesim.jpg", "Varsayılan OG görsel", "SEO"),
            ("Analytics_GA4_Id", "", "GA4 Measurement ID (boşsa kapalı)", "SEO"),
            ("Blog_Title_Suffix", " | Celil & Atilla", "Blog Detay Başlık Eki", "SEO"),
            ("Shop_Title_Suffix", " | Celil & Atilla", "Mağaza Detay Başlık Eki", "SEO"),
            ("Contact_Phone", "0534 794 43 93", "Telefon", "Contact"),
            ("Contact_Email", "", "E-posta (sitede gösterilmez)", "Contact"),
            ("Contact_Address", "Kazımdirik Mah. Gediz Cd. 21 A", "Adres", "Contact"),
            ("Contact_City", "35100 Bornova / İzmir", "Şehir", "Contact"),
            ("Contact_Hours", "Pzt - Cmt: 08:30 - 20:00\nPazar: 10:00 - 19:00", "Çalışma Saatleri", "Contact"),
            ("Whatsapp_Number", "905347944393", "WhatsApp Numarası", "Contact"),
            ("Whatsapp_Message", "Merhaba, Celil & Atilla Bornova’dan randevu almak istiyorum.", "WhatsApp Mesajı", "Contact"),
            ("Social_Instagram", "https://www.instagram.com/celil_atilla/", "Instagram", "Social"),
            ("Social_Facebook", "", "Facebook", "Social"),
            ("Social_Pinterest", "", "Pinterest", "Social"),
            ("Instagram_Handle", "@celil_atilla", "Instagram Kullanıcı Adı", "Social"),
            ("Map_Lat", "38.46234930909974", "Harita Enlem", "Map"),
            ("Map_Lng", "27.208800699999998", "Harita Boylam", "Map"),
            ("Map_Label", "Celil & Atilla", "Harita Etiketi", "Map"),
            ("Map_Address", "Gediz Cd. 21 A, Kazımdirik / Bornova", "Harita Adres", "Map"),
            ("Map_Google_Url", "https://www.google.com/maps/search/?api=1&query=38.46234930909974,27.208800699999998", "Google Maps", "Map"),
        };

        foreach (var (key, value, description, group) in seeds)
        {
            var existing = context.SiteSettings.FirstOrDefault(s => s.Key == key);
            if (existing == null)
            {
                context.SiteSettings.Add(new SiteSetting
                {
                    Key = key,
                    Value = value,
                    Description = description,
                    Group = group
                });
            }
            else
            {
                existing.Value = value;
                existing.Description = description;
                existing.Group = group;
                existing.UpdatedAt = DateTime.UtcNow;
            }
        }
        context.SaveChanges();
    }

    static void UpsertSiteContents(ApplicationDbContext context)
    {
        foreach (var seed in GetSiteContentSeeds())
        {
            var culture = string.IsNullOrWhiteSpace(seed.Culture) ? "tr" : seed.Culture;
            seed.Culture = culture;
            var existing = context.SiteContents.FirstOrDefault(c =>
                c.Key == seed.Key && (c.Culture == culture || (culture == "tr" && (c.Culture == "" || c.Culture == null))));
            if (existing == null)
            {
                context.SiteContents.Add(seed);
            }
            else
            {
                existing.Value = seed.Value;
                existing.Section = seed.Section;
                existing.Label = seed.Label;
                existing.DisplayOrder = seed.DisplayOrder;
                existing.ContentType = seed.ContentType;
                existing.Culture = culture;
            }
        }
        context.SaveChanges();
    }

    static List<SiteContent> GetSiteContentSeeds()
    {
        var items = new List<SiteContent>
        {
        Sc("loading_text", "Loading", "Yükleme Metni", "celil & atilla", 1),
        Sc("hero_subtitle", "Hero", "Hero Alt Başlık", "SAÇINIZI TANIYAN BİR SALON", 1),
        Sc("hero_title_line1", "Hero", "Hero Başlık Satır 1", "Celil", 2),
        Sc("hero_title_line2", "Hero", "Hero Başlık Satır 2", "& Atilla", 3),
        Sc("hero_description", "Hero", "Hero Açıklama", "Kesimden renge, bakımdan şekillendirmeye; saçınıza, yüzünüze ve günlük ritminize uyan sonucu birlikte buluyoruz.", 4),
        Sc("hero_cta_appointment", "Hero", "Randevu CTA", "RANDEVU OLUŞTUR", 5),
        Sc("hero_cta_services", "Hero", "Hizmetler CTA", "NELER YAPIYORUZ?", 6),
        Sc("hero_scroll", "Hero", "Kaydır Metni", "SALONU KEŞFEDİN", 7),

        Sc("about_subtitle", "About", "Alt Başlık", "CELİL & ATİLLA", 1),
        Sc("about_title", "About", "Başlık", "Yıllar geçer, iyi bir salon sizi hatırlar", 2),
        Sc("about_description1", "About", "Açıklama 1", "Hikâyemiz 2000 yılında Bornova’da başladı. Bugün Kazımdirik Mahallesi, Gediz Caddesi 21 A’daki tek salonumuzda aynı özenle çalışmaya devam ediyoruz.", 3),
        Sc("about_description2", "About", "Açıklama 2", "Kadın kesimi, renk ve bakım işin merkezinde. Erkek ve çocuk randevuları da aynı ekibin temposunda ilerliyor. Önce saçınızı dinliyor, sonra gerçekten gereken işlemi konuşuyoruz.", 4),
        Sc("about_cta", "About", "CTA", "HİKÂYEMİZİ OKUYUN", 5),
        Sc("about_experience_number", "About", "Deneyim Sayısı", "25+", 6),
        Sc("about_experience_text", "About", "Deneyim Metni", "YILLIK SALON DENEYİMİ", 7),

        Sc("services_subtitle", "Services", "Alt Başlık", "İHTİYACINIZA GÖRE", 1),
        Sc("services_title", "Services", "Başlık", "Saçınız için doğru dokunuş", 2),
        Sc("service1_title", "Services", "Hizmet 1 Başlık", "Kadın kesim", 3),
        Sc("service1_desc", "Services", "Hizmet 1 Açıklama", "Yüze uygun kesim, kahkül ve fön", 4),
        Sc("service2_title", "Services", "Hizmet 2 Başlık", "Renklendirme", 5),
        Sc("service2_desc", "Services", "Hizmet 2 Açıklama", "Balyaj, ombre, röfle ve kök rötuşu", 6),
        Sc("service3_title", "Services", "Hizmet 3 Başlık", "Bakım", 7),
        Sc("service3_desc", "Services", "Hizmet 3 Açıklama", "Keratin, saç botoksu ve onarım", 8),
        Sc("service4_title", "Services", "Hizmet 4 Başlık", "Erkek & çocuk", 9),
        Sc("service4_desc", "Services", "Hizmet 4 Açıklama", "Fade, sakal düzeni ve çocuk kesimi", 10),

        Sc("team_subtitle", "Team", "Alt Başlık", "EKİP", 1),
        Sc("team_title", "Team", "Başlık", "Uzman kadromuz", 2),
        Sc("pricing_subtitle", "Pricing", "Alt Başlık", "Fiyat", 1),
        Sc("pricing_title", "Pricing", "Başlık", "Fiyat listesi", 2),
        Sc("testimonials_subtitle", "Testimonials", "Alt Başlık", "YORUMLAR", 1),
        Sc("portfolio_subtitle", "Portfolio", "Alt Başlık", "ÇALIŞMALAR", 1),
        Sc("portfolio_title", "Portfolio", "Başlık", "Salondan çıkan işler", 2),
        Sc("portfolio_cta", "Portfolio", "Tümünü Gör", "SALONU TANIYIN →", 3),
        Sc("instagram_subtitle", "Instagram", "Alt Başlık", "INSTAGRAM", 1),
        Sc("instagram_handle", "Instagram", "Kullanıcı Adı", "@celil_atilla", 2),
        Sc("cta_subtitle", "CTA", "Alt Başlık", "RANDEVU", 1),
        Sc("cta_title", "CTA", "Başlık", "Saçınız için doğru zamanı ayıralım", 2),
        Sc("cta_description", "CTA", "Açıklama", "Aklınızdaki işlemi ve tercih ettiğiniz zamanı paylaşın. Salon ekibi uygun randevu için size dönüş yapsın.", 3),
        Sc("contact_subtitle", "Contact", "Alt Başlık", "SALONA ULAŞIN", 1),
        Sc("contact_title", "Contact", "Başlık", "Randevunuz burada başlıyor", 2),
        Sc("contact_address_label", "Contact", "Adres Etiketi", "Adres", 3),
        Sc("contact_hours_label", "Contact", "Saat Etiketi", "Çalışma saatleri", 4),
        Sc("contact_button", "Contact", "Gönder Butonu", "MESAJI GÖNDER", 5),
        Sc("contact_form_name", "Contact", "Form Ad", "Adınız Soyadınız", 6),
        Sc("contact_form_email", "Contact", "Form E-posta", "E-posta", 7),
        Sc("contact_form_phone", "Contact", "Form Telefon", "Telefon", 8),
        Sc("contact_form_message", "Contact", "Form Mesaj", "Mesajınız", 9),
        Sc("footer_desc", "Footer", "Açıklama", "2000’de Bornova’da başlayan Celil & Atilla hikâyesi, bugün Kazımdirik’te kadın, erkek ve çocuk saçına aynı özenle devam ediyor.", 1),

        Sc("booking_time_select", "Index", "Saat Seçin", "Saat Seçin", 1),
        Sc("booking_email", "Index", "E-posta Placeholder", "E-posta", 2),
        Sc("booking_continue", "Index", "Devam Butonu", "DEVAM", 3),
        Sc("booking_modal_title", "Index", "Modal Başlık", "Randevuyu tamamla", 4),
        Sc("booking_modal_desc", "Index", "Modal Açıklama", "Adınızı ve telefonunuzu yazın. Onaylayınca WhatsApp açılır.", 5),
        Sc("booking_name_label", "Index", "Ad Soyad", "Ad Soyad", 6),
        Sc("booking_phone_label", "Index", "Telefon", "Telefon", 7),
        Sc("booking_confirm", "Index", "Onayla", "WHATSAPP İLE GÖNDER", 8),
        Sc("booking_processing", "Index", "İşleniyor", "İŞLENİYOR...", 9),
        Sc("service_details", "Index", "Detaylar", "DETAYLAR", 10),

        Sc("hakkimizda_page_title", "Hakkimizda", "Sayfa Başlığı", "Hakkımızda | Celil & Atilla", 1),
        Sc("hakkimizda_hero_subtitle", "Hakkimizda", "Hero Alt Başlık", "BİR SALONUN HAFIZASI", 2),
        Sc("hakkimizda_hero_title1", "Hakkimizda", "Hero Başlık 1", "Celil", 3),
        Sc("hakkimizda_hero_title2", "Hakkimizda", "Hero Başlık 2", "& Atilla", 4),
        Sc("hakkimizda_hero_quote", "Hakkimizda", "Hero Alıntı", "Saçı tanımak zaman ister. İyi sonuç, o zamanı ayırmakla başlar.", 5),
        Sc("hakkimizda_story_subtitle", "Hakkimizda", "Hikaye Alt Başlık", "Hikâyemiz", 6),
        Sc("hakkimizda_story_title", "Hakkimizda", "Hikaye Başlık", "Bir koltuk, bir ayna, yıllara yayılan tanışıklıklar", 7),
        Sc("hakkimizda_story_p1", "Hakkimizda", "Hikaye Paragraf 1", "Celil & Atilla’nın hikâyesi 2000 yılında Bornova’da başladı. Yıllar içinde saçlar, renkler ve alışkanlıklar değişti; değişmeyen şey, her randevuda kişiyi ve saçını yeniden dinlemek oldu.", 8),
        Sc("hakkimizda_story_p2", "Hakkimizda", "Hikaye Paragraf 2", "Bugün Kazımdirik Mahallesi, Gediz Caddesi 21 A’daki tek adresimizde Celil ve salon ekibi çalışıyor. Kadın kesimi, renklendirme ve bakım ana uzmanlığımız; erkek, çocuk, tırnak ve özel gün randevuları da aynı çatı altında.", 9),
        Sc("hakkimizda_story_p3", "Hakkimizda", "Hikaye Paragraf 3", "Menümüzde yalnızca yaptığımız işler var: balyajdan kök rötuşuna, keratinden saç botoksuna kadar saçın ihtiyacına göre seçilen uygulamalar. Tıbbi estetik, protez tırnak ve kalıcı oje sunmuyoruz. Çünkü güven, doğru beklentiyle başlar.", 10),
        Sc("hakkimizda_exp_number", "Hakkimizda", "Deneyim Kartı", "2000", 11),
        Sc("hakkimizda_exp_text", "Hakkimizda", "Deneyim Metni", "Kuruluş", 12),
        Sc("hakkimizda_philosophy_subtitle", "Hakkimizda", "Felsefe Alt Başlık", "Çalışma biçimimiz", 13),
        Sc("hakkimizda_philosophy_quote", "Hakkimizda", "Felsefe Alıntı", "\"Önce saçı görürüz. Sonra işlem konuşulur.\"", 14),
        Sc("hakkimizda_philosophy_author", "Hakkimizda", "Felsefe Yazar", "— Celil", 15),
        Sc("hakkimizda_team_subtitle", "Hakkimizda", "Ekip Alt Başlık", "Ekip", 16),
        Sc("hakkimizda_team_title", "Hakkimizda", "Ekip Başlık", "Uzman kadromuz", 17),
        Sc("hakkimizda_awards_subtitle", "Hakkimizda", "Ödül Alt Başlık", "", 18),
        Sc("hakkimizda_awards_title", "Hakkimizda", "Ödül Başlık", "", 19),
        Sc("hakkimizda_cta_subtitle", "Hakkimizda", "CTA Alt Başlık", "Randevu", 20),
        Sc("hakkimizda_cta_title", "Hakkimizda", "CTA Başlık", "Saçınız için doğru zamanı ayıralım", 21),
        Sc("hakkimizda_cta_button", "Hakkimizda", "CTA Buton", "RANDEVU AL", 22),

        Sc("faq_subtitle", "FAQ", "Alt Başlık", "SIK SORULANLAR", 1),
        Sc("faq_title", "FAQ", "Başlık", "Merak edilenler, net cevaplar", 2),
        Sc("faq_1_q", "FAQ", "Soru 1", "Celil & Atilla Kuaför nerede?", 3),
        Sc("faq_1_a", "FAQ", "Cevap 1", "Salonumuz Bornova’nın Kazımdirik Mahallesi’nde, Gediz Caddesi 21 A adresindedir. Celil & Atilla’nın başka şubesi yoktur; güncel konumumuz budur.", 4),
        Sc("faq_2_q", "FAQ", "Soru 2", "Çalışma saatleri nedir?", 5),
        Sc("faq_2_a", "FAQ", "Cevap 2", "Pazartesi–Cumartesi 08:30–20:00, Pazar 10:00–19:00. Randevu: 0534 794 43 93 veya WhatsApp.", 6),
        Sc("faq_3_q", "FAQ", "Soru 3", "Balyaj, ombre ve sombre farkı nedir?", 7),
        Sc("faq_3_a", "FAQ", "Cevap 3", "Balyaj ışığı saça serbest biçimde dağıtır, ombre koyu dipten açık uçlara daha belirgin geçer, sombre ise bu geçişin yumuşak yorumudur. Seçim saçın geçmişine ve istediğiniz görünüme göre yapılır.", 8),
        Sc("faq_4_q", "FAQ", "Soru 4", "Saç botoksu tıbbi botoks mudur?", 9),
        Sc("faq_4_a", "FAQ", "Cevap 4", "Hayır. Saç botoksu ve saç dolgusu iğnesiz bakımdır. Yüz botoksu veya tıbbi dolgu yapılmaz.", 10),
        Sc("faq_5_q", "FAQ", "Soru 5", "Erkek ve çocuk kabul ediyor musunuz?", 11),
        Sc("faq_5_a", "FAQ", "Cevap 5", "Evet. Kadın saç tasarımı ana uzmanlığımız; bunun yanında erkek kesimi, fade, sakal, tıraş, erkek boya ve çocuk kesimi için de randevu alınabilir.", 12),
        Sc("faq_6_q", "FAQ", "Soru 6", "Randevu nasıl alınır?", 13),
        Sc("faq_6_a", "FAQ", "Cevap 6", "0534 794 43 93’ü arayın, WhatsApp’tan yazın veya sitedeki randevu formunu doldurun. İstediğiniz işlem ile tercih ettiğiniz gün ve saat yeterlidir.", 14),

        Sc("hizmetler_page_title", "Hizmetler", "Sayfa Başlığı", "Saç, Bakım ve Güzellik Hizmetleri | Celil & Atilla", 1),
        Sc("hizmetler_hero_subtitle", "Hizmetler", "Hero Alt Başlık", "MENÜ", 2),
        Sc("hizmetler_hero_title", "Hizmetler", "Hero Başlık", "Hizmetlerimiz", 3),
        Sc("hizmetler_hero_desc", "Hizmetler", "Hero Açıklama", "Kadın kesimi, renklendirme ve bakım başta olmak üzere her işlemi saçın yapısına ve beklenen sonuca göre planlıyoruz. Erkek ve çocuk randevuları da alınır.", 4),
        Sc("hizmetler_empty_title", "Hizmetler", "Boş Başlık", "Liste hazırlanıyor", 5),
        Sc("hizmetler_empty_desc", "Hizmetler", "Boş Açıklama", "Kısa süre sonra.", 6),
        Sc("hizmetler_cat_kadin_kesim", "Hizmetler", "Kadın Kesim", "Kesim ve şekillendirme", 7),
        Sc("hizmetler_cat_kadin_kesim_title", "Hizmetler", "Kadın Kesim Başlık", "Kadın Kesim", 70),
        Sc("hizmetler_cat_renk", "Hizmetler", "Renk", "Geçişler ve tek renk", 8),
        Sc("hizmetler_cat_renk_title", "Hizmetler", "Renk Başlık", "Renklendirme", 80),
        Sc("hizmetler_cat_bakim", "Hizmetler", "Bakım", "Form ve onarım", 9),
        Sc("hizmetler_cat_bakim_title", "Hizmetler", "Bakım Başlık", "Bakım", 90),
        Sc("hizmetler_cat_kaynak", "Hizmetler", "Kaynak", "Uzunluk ve doluluk", 10),
        Sc("hizmetler_cat_kaynak_title", "Hizmetler", "Kaynak Başlık", "Saç Kaynağı", 100),
        Sc("hizmetler_cat_tirnak", "Hizmetler", "Tırnak", "Klasik bakım", 11),
        Sc("hizmetler_cat_tirnak_title", "Hizmetler", "Tırnak Başlık", "Tırnak", 110),
        Sc("hizmetler_cat_kas", "Hizmetler", "Kaş", "Kaş, kirpik ve ağda", 12),
        Sc("hizmetler_cat_kas_title", "Hizmetler", "Kaş Başlık", "Kaş, Kirpik & Ağda", 120),
        Sc("hizmetler_cat_ozel", "Hizmetler", "Özel Gün", "Gelin ve davet", 13),
        Sc("hizmetler_cat_ozel_title", "Hizmetler", "Özel Gün Başlık", "Özel Gün", 130),
        Sc("hizmetler_cat_erkek", "Hizmetler", "Erkek", "Kesim ve sakal", 14),
        Sc("hizmetler_cat_erkek_title", "Hizmetler", "Erkek Başlık", "Erkek", 140),
        Sc("hizmetler_cat_cocuk", "Hizmetler", "Çocuk", "Kısa, sakin randevu", 15),
        Sc("hizmetler_cat_cocuk_title", "Hizmetler", "Çocuk Başlık", "Çocuk", 150),
        Sc("hizmetler_cta_subtitle", "Hizmetler", "CTA Alt Başlık", "Randevu", 16),
        Sc("hizmetler_cta_title", "Hizmetler", "CTA Başlık", "İstediğiniz işlemi yazın, uygun saati bulalım", 17),
        Sc("hizmetler_cta_button", "Hizmetler", "CTA Buton", "RANDEVU AL", 18),
        Sc("hizmetler_price_note", "Hizmetler", "Fiyat Notu", "", 19),

        Sc("randevu_page_title", "Randevu", "Sayfa Başlığı", "Randevu | Celil & Atilla", 1),
        Sc("randevu_hero_subtitle", "Randevu", "Hero Alt Başlık", "UYGUN ZAMANI BULALIM", 2),
        Sc("randevu_hero_title", "Randevu", "Hero Başlık", "Randevu", 3),
        Sc("randevu_intro_title", "Randevu", "Giriş Başlık", "İletişim", 4),
        Sc("randevu_intro_heading", "Randevu", "Giriş Başlık HTML", "Saçınız için<br/><span class=\"italic\">zaman ayırın</span>", 5, "html"),
        Sc("randevu_intro_desc", "Randevu", "Giriş Açıklama", "İstediğiniz işlemi, günü ve saati yazın. Gönderince WhatsApp açılır; mesajı 0534 794 43 93 numarasına iletmeniz yeterlidir.", 6),
        Sc("randevu_success", "Randevu", "Başarı Mesajı", "Talebiniz alındı. En kısa sürede dönüş yapılır.", 7),
        Sc("randevu_submit", "Randevu", "Gönder Butonu", "WHATSAPP İLE GÖNDER", 8),
        Sc("randevu_sms_note", "Randevu", "SMS Notu", "Gönderince WhatsApp açılır. Mesajı salon numarasına iletmeniz gerekir; iletmeden randevu kesinleşmez.", 9),
        Sc("randevu_form_title", "Randevu", "Form Başlık", "Randevu bilgileri", 10),
        Sc("randevu_label_firstname", "Randevu", "Ad Etiketi", "Ad *", 11),
        Sc("randevu_label_lastname", "Randevu", "Soyad Etiketi", "Soyad *", 12),
        Sc("randevu_label_phone", "Randevu", "Telefon Etiketi", "Telefon *", 13),
        Sc("randevu_label_email", "Randevu", "E-posta Etiketi", "E-posta", 14),
        Sc("randevu_label_date", "Randevu", "Tarih Etiketi", "Tarih *", 15),
        Sc("randevu_label_time", "Randevu", "Saat Etiketi", "Saat *", 16),
        Sc("randevu_label_service", "Randevu", "Hizmet Etiketi", "Hizmet *", 17),
        Sc("randevu_label_notes", "Randevu", "Not Etiketi", "Not", 18),
        Sc("randevu_label_address", "Randevu", "Adres Etiketi", "Adres", 19),
        Sc("randevu_label_hours", "Randevu", "Saatler Etiketi", "Çalışma saatleri", 20),
        Sc("randevu_placeholder_firstname", "Randevu", "Ad Placeholder", "Adınız", 21),
        Sc("randevu_placeholder_lastname", "Randevu", "Soyad Placeholder", "Soyadınız", 22),
        Sc("randevu_placeholder_phone", "Randevu", "Telefon Placeholder", "05XX XXX XX XX", 23),
        Sc("randevu_placeholder_email", "Randevu", "E-posta Placeholder", "E-posta (isteğe bağlı)", 24),
        Sc("randevu_placeholder_notes", "Randevu", "Not Placeholder", "Saç boyası, çocuk, gelin…", 25),
        Sc("randevu_time_select", "Randevu", "Saat Seçin", "Saat seçin", 26),
        Sc("randevu_service_select", "Randevu", "Hizmet Seçin", "Hizmet seçin", 27),
        Sc("randevu_success_title", "Randevu", "Başarı Başlık", "Alındı", 28),

        Sc("blog_page_title", "Blog", "Sayfa Başlığı", "Saç Rehberi | Celil & Atilla", 1),
        Sc("blog_hero_subtitle", "Blog", "Hero Alt Başlık", "Celil & Atilla’dan", 2),
        Sc("blog_hero_title", "Blog", "Hero Başlık", "Saç, renk ve bakım rehberi", 3),
        Sc("blog_empty_title", "Blog", "Boş Başlık", "Yazı yok", 4),
        Sc("blog_empty_desc", "Blog", "Boş Açıklama", "Yeni yazı eklenince burada durur.", 5),
        Sc("blog_read_more", "Blog", "Devamını Oku", "YAZIYI OKU", 6),
        Sc("blog_other_posts", "Blog", "Diğer Yazılar", "Diğer bakım ve stil yazıları", 7),
        Sc("blog_newsletter_subtitle", "Blog", "Bülten Alt Başlık", "Randevu", 8),
        Sc("blog_newsletter_title", "Blog", "Bülten Başlık", "Saçınız için doğru işlemi birlikte seçelim", 9),
        Sc("blog_newsletter_desc", "Blog", "Bülten Açıklama", "Aklınızdaki işlemi ve uygun olduğunuz zamanı WhatsApp’tan yazın; salon ekibi size dönüş yapsın.", 10),
        Sc("blog_newsletter_placeholder", "Blog", "Bülten Placeholder", "Kullanılmıyor", 11),
        Sc("blog_newsletter_button", "Blog", "Abone Ol", "WHATSAPP", 12),
        Sc("blog_back", "Blog", "Geri Dön", "← Blog", 20),
        Sc("blog_share", "Blog", "Paylaş", "Paylaş", 21),

        Sc("shop_page_title", "Shop", "Sayfa Başlığı", "Mağaza | Celil & Atilla", 1),
        Sc("shop_hero_subtitle", "Shop", "Hero Alt Başlık", "EVDE BAKIM", 2),
        Sc("shop_hero_title", "Shop", "Hero Başlık", "Salon seçkisi yakında", 3),
        Sc("shop_empty_title", "Shop", "Boş Başlık", "Doğru ürünü seçmeden satışa başlamıyoruz", 4),
        Sc("shop_empty_desc", "Shop", "Boş Açıklama", "Evde bakım seçkisi hazırlanıyor. Bu sırada saçınızın ihtiyacı için salon ekibine danışabilirsiniz.", 5),
        Sc("shop_why_subtitle", "Shop", "Neden Biz Alt Başlık", "YAKLAŞIMIMIZ", 6),
        Sc("shop_why_title", "Shop", "Neden Biz Başlık", "Önce saç, sonra ürün", 7),
        Sc("shop_why_1_title", "Shop", "Neden 1", "İhtiyaca göre", 8),
        Sc("shop_why_1_desc", "Shop", "Neden 1 Açıklama", "Ürün, saçın yapısı ve yapılan işlem düşünülerek seçilir.", 9),
        Sc("shop_why_2_title", "Shop", "Neden 2", "Salonda tanışın", 10),
        Sc("shop_why_2_desc", "Shop", "Neden 2 Açıklama", "Seçkiyi Kazımdirik’teki salonumuzda görebileceksiniz.", 11),
        Sc("shop_why_3_title", "Shop", "Neden 3", "Bize danışın", 12),
        Sc("shop_why_3_desc", "Shop", "Neden 3 Açıklama", "Sorularınız için WhatsApp: 0534 794 43 93.", 13),
        Sc("shop_details_shipping", "Shop", "Kargo Metni", "Kargo yok", 14),
        Sc("shop_details_placeholder", "Shop", "Detay Placeholder", "Ürün yok.", 15),
        Sc("shop_add_cart", "Shop", "Sepete Ekle", "YOK", 16),
        Sc("shop_details_no_image", "Shop", "Görsel Yok", "Görsel yok", 17),
        Sc("shop_stock_label", "Shop", "Stok Etiketi", "Stok:", 18),
        Sc("shop_details_accordion_title", "Shop", "Detay Accordion", "Detay", 19),
        Sc("shop_details_shipping_title", "Shop", "Kargo Accordion", "Teslimat", 20),
        Sc("shop_details_shipping_desc", "Shop", "Kargo Açıklama", "Online satış yok.", 21),

        Sc("privacy_title", "Privacy", "Başlık", "Gizlilik", 1, "html"),
        Sc("privacy_content", "Privacy", "İçerik", "<p>Celil & Atilla Kuaför (celilatilla.com) randevu ve iletişim formunda ad, telefon ve isteğe bağlı e-posta alır. Bu bilgiler yalnızca randevu ve dönüş için kullanılır.</p><p>Adres: Kazımdirik Mah. Gediz Cd. 21 A, 35100 Bornova / İzmir. Telefon: 0534 794 43 93.</p>", 2, "html"),
        };

        items.AddRange(SiteContentEnSeed.Items());
        return items;
    }

    static SiteContent Sc(string key, string section, string label, string value, int order, string type = "text") =>
        new() { Key = key, Section = section, Label = label, Value = value, DisplayOrder = order, ContentType = type, Culture = "tr" };

    static void RefreshServices(ApplicationDbContext context)
    {
        var catalog = ServiceCatalog();
        var names = catalog.Select(s => s.Name).ToHashSet();
        var existing = context.Services.ToList();

        foreach (var item in catalog)
        {
            var match = existing.FirstOrDefault(s => s.Name == item.Name);
            if (match == null)
            {
                context.Services.Add(item);
            }
            else
            {
                match.Description = item.Description;
                match.DescriptionEn = item.DescriptionEn;
                match.NameEn = item.NameEn;
                match.Category = item.Category;
                match.DurationMinutes = item.DurationMinutes;
                match.ImageUrl = item.ImageUrl;
                match.Price = 0;
                if (string.IsNullOrWhiteSpace(match.Slug))
                    match.Slug = SlugHelper.FromTitle(match.Name);
                match.IsPublished = true;
            }
        }

        var referenced = context.Appointments.Select(a => a.ServiceId).Distinct().ToHashSet();
        var stale = existing.Where(s => !names.Contains(s.Name) && !referenced.Contains(s.Id)).ToList();
        if (stale.Count > 0)
            context.Services.RemoveRange(stale);

        context.SaveChanges();
    }

    static void RefreshServiceLandings(ApplicationDbContext context)
    {
        var landings = ServiceLandingSeed.Pages();
        var existing = context.Services.ToList();

        foreach (var seed in landings)
        {
            var match = existing.FirstOrDefault(s =>
                           string.Equals(s.Slug, seed.Slug, StringComparison.OrdinalIgnoreCase))
                       ?? existing.FirstOrDefault(s =>
                           string.Equals(s.Name, seed.Name, StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                context.Services.Add(seed);
                existing.Add(seed);
                continue;
            }

            match.Name = seed.Name;
            match.NameEn = seed.NameEn;
            match.Slug = seed.Slug;
            match.SlugEn = seed.SlugEn;
            match.Description = seed.Description;
            match.DescriptionEn = seed.DescriptionEn;
            match.LongDescription = seed.LongDescription;
            match.LongDescriptionEn = seed.LongDescriptionEn;
            match.SeoTitle = seed.SeoTitle;
            match.SeoTitleEn = seed.SeoTitleEn;
            match.MetaDescription = seed.MetaDescription;
            match.MetaDescriptionEn = seed.MetaDescriptionEn;
            match.Tags = seed.Tags;
            match.Category = seed.Category;
            match.ImageUrl = seed.ImageUrl;
            match.DurationMinutes = seed.DurationMinutes;
            match.IsLandingPage = true;
            match.IsPublished = true;
            match.Price = 0;
            match.UpdatedAt = DateTime.UtcNow;
        }

        context.SaveChanges();
    }

    static List<Service> ServiceCatalog()
    {
        const string basePath = "/images/services/";

        return new()
        {
            S("Kadın saç kesimi", "Women's haircut", "Kadın Kesim",
                "Yüz hattı, saç yoğunluğu ve günlük bakım alışkanlığınıza göre planlanan kesim. Trend isim değil; sizin tarifiniz esas alınır.",
                "A cut planned around face shape, density and your daily styling habits — not a trend name.",
                60, basePath + "kadin-kesim.jpg"),
            S("Kahkül", "Bangs / fringe", "Kadın Kesim",
                "Alın ve saç tipine göre kahkül veya fringe. Düzenli düzeltme ister; ilk görüşmede yoğunluk konuşulur.",
                "Bangs or fringe matched to your forehead and hair type. Needs regular trims; density is decided in consultation.",
                30, basePath + "kahkul.jpg"),
            S("Fön ve şekillendirme", "Blow-dry & styling", "Kadın Kesim",
                "Kesim sonrası veya ayrı randevuda fön. Düz, içe kıvrım veya hafif hacim — saçın kapasitesine göre.",
                "Blow-dry after a cut or as a separate visit. Straight, soft curl or light volume — according to what the hair can hold.",
                45, basePath + "fon.jpg"),
            S("Beach waves", "Beach waves", "Kadın Kesim",
                "Doğal, dağınık dalga formu. Her saç tipinde aynı durmaz; kıvırcık ve düz saçta sonuç ayrı değerlendirilir.",
                "A natural, loose wave. Results differ on curly and straight hair; we assess first.",
                45, basePath + "beach-waves.jpg"),

            S("Tek renk ve kök rötuşu", "Solid colour & root touch-up", "Renklendirme",
                "Tüm kafa boyası veya yalnızca dip rötuşu. Süre, uzunluk ve önceki boya yüküne bağlıdır.",
                "Full-head colour or roots only. Timing depends on length and previous colour load.",
                90, basePath + "tek-renk.jpg"),
            S("Röfle", "Highlights", "Renklendirme",
                "Folyo ile seçilmiş tutamlar. İnce veya kalın paket; yüz çevresi ve tepe dengesi birlikte planlanır.",
                "Selected foiled pieces. Fine or bold placement; face frame and crown balance are planned together.",
                120, basePath + "rofle.jpg"),
            S("Balyaj, ombre ve sombre", "Balayage, ombre and sombre", "Renklendirme",
                "Bornova’da en çok sorulan üç geçiş. Balyaj elde açma; ombre daha net sınır; sombre yumuşak ombre. Tek randevuda saça uygun olanı seçilir.",
                "Three transitions clients ask about most. Balayage is freehand; ombre is clearer; sombre is softer. One technique is chosen per visit.",
                180, basePath + "balyaj.jpg"),
            S("Pastel ve renk düzeltme", "Pastel & colour correction", "Renklendirme",
                "Fashion tonlar veya bozulmuş rengi toparlama. Bazen ikinci seans gerekir; vaat abartılmaz.",
                "Fashion tones or correcting compromised colour. A second session may be needed; we don’t overpromise.",
                150, basePath + "pastel.jpg"),

            S("Keratin bakımı", "Keratin treatment", "Bakım",
                "Kabaran saçı daha düzgün tutmak için. Ev bakımı şarttır. Her saça ütü efekti gibi satılmaz.",
                "Helps manage frizz and keep hair smoother. Home care is required. Not sold as a flat-iron result for every hair type.",
                180, basePath + "keratin.jpg"),
            S("Saç botoksu ve saç dolgusu", "Hair botox & hair filler", "Bakım",
                "İğnesiz bakım. Botoks doluluk ve parlaklık; dolgu ince, boşalmış tutam içindir. Tıbbi botoks değildir.",
                "Needle-free care. Hair botox for body and shine; filler for thin, hollow lengths. Not medical botox.",
                90, basePath + "sac-botoks.jpg"),
            S("Protein, nem ve saç derisi", "Protein, moisture & scalp care", "Bakım",
                "Kuru saçta nem, yorulmuş saçta protein, kepek veya yağlı dipte deri bakımı. Hangisi gerektiği görülünce seçilir.",
                "Moisture for dry hair, protein for fatigued lengths, scalp care for flakes or oily roots — chosen after assessment.",
                60, basePath + "protein.jpg"),
            S("Form işlemleri", "Smoothing & perm services", "Bakım",
                "Nanoplasti, perma, biaform ve Brezilya fönü. Form değiştirir; keratinin yerine geçmez. Uygun olanı ayrı konuşulur.",
                "Nanoplasty, perm, biaform and Brazilian blowout. They change shape; they are not a keratin substitute.",
                180, basePath + "form.jpg"),

            S("Saç kaynağı", "Hair extensions", "Saç Kaynağı",
                "Uzunluk veya doluluk için kaynak. Mevcut saçın durumu sınırı belirler; her saça uygulanmaz.",
                "Extensions for length or density. Existing hair condition sets the limit; not suitable for every client.",
                180, basePath + "kaynak.jpg"),

            S("Klasik manikür", "Classic manicure", "Tırnak",
                "Şekil, kutikül ve oje. Kalıcı oje ve protez tırnak bu menüde yoktur.",
                "Shape, cuticle and polish. Gel polish and acrylics are not on this menu.",
                40, basePath + "manikur.jpg"),
            S("Klasik ve spa pedikür", "Classic & spa pedicure", "Tırnak",
                "Klasik pedikür düzen; spa pedikür süre ve yumuşatma ekler. Medikal ayak tedavisi değildir.",
                "Classic pedicure for tidy feet; spa adds softening time. Not a medical foot treatment.",
                50, basePath + "pedikur.jpg"),

            S("Kaş şekillendirme ve laminasyon", "Brow shaping & lamination", "Kaş, Kirpik & Ağda",
                "Cımbız veya ip ile şekil; laminasyon kıl yönünü yatırır. Yüz hatlarına göre sınır çizilir.",
                "Tweezers or threading for shape; lamination sets hair direction. Borders follow your face.",
                40, basePath + "kas.jpg"),
            S("Kirpik lifting", "Lash lift", "Kaş, Kirpik & Ağda",
                "Kirpikleri kıvırır ve açık gösterir. Takma kirpik bu kartın parçası değildir.",
                "Lifts and opens the lashes. False lashes are not part of this service.",
                45, basePath + "kirpik.jpg"),
            S("Yüz ve vücut ağdası", "Face & body waxing", "Kaş, Kirpik & Ağda",
                "Sir / ağda. Bölge randevuda belirtilir; cilt hassasiyeti önceden sorulur.",
                "Waxing by area, confirmed at booking. Skin sensitivity is checked first.",
                30, basePath + "agda.jpg"),

            S("Gelin saçı", "Bridal hair", "Özel Gün",
                "Toplu, yarım açık veya duvak altı modeller. Prova önerilir; düğün günü sürpriz bırakılmaz.",
                "Updos, half-up or veil-ready styles. A trial is recommended; wedding day should not be a surprise.",
                90, basePath + "gelin-sac.jpg"),
            S("Gelin ve özel gün makyajı", "Bridal & event makeup", "Özel Gün",
                "Ten ve kıyafete göre planlanan makyaj. Ağır filtre vaadi yoktur; prova ile netleşir.",
                "Makeup planned for your skin and outfit. No heavy-filter promises; a trial clarifies the look.",
                75, basePath + "makyaj.jpg"),

            S("Erkek kesim ve fade", "Men's cut & fade", "Erkek",
                "Makine-makas, fade veya skin fade. Yanların açıklığı ve tepe uzunluğu birlikte konuşulur.",
                "Clipper-scissor work, fade or skin fade. Side length and crown height are decided together.",
                40, basePath + "erkek-kesim.jpg"),
            S("Sakal ve tıraş", "Beard & shave", "Erkek",
                "Hat, yanak, boyun düzeni veya tam tıraş. Yüz şekline göre sınır çizilir.",
                "Lines, cheeks, neck tidy-up or a full shave. Borders follow face shape.",
                25, basePath + "sakal.jpg"),
            S("Erkek boya", "Men's colour", "Erkek",
                "Beyaz kapatma veya hafif ton. Kadın global boyadan ayrı karışımdır; doğal görünüm hedeflenir.",
                "Grey coverage or a soft tone. Mixed separately from women’s global colour for a natural finish.",
                40, basePath + "erkek-boya.jpg"),

            S("Çocuk saç kesimi", "Children's haircut", "Çocuk",
                "Kız ve erkek çocuk. Kısa, sakin bir randevu; fotoğraf göstermek işe yarar.",
                "For girls and boys. A short, calm visit; bringing a photo helps.",
                25, basePath + "cocuk.jpg"),
        };
    }

    static Service S(string name, string nameEn, string category, string description, string descriptionEn, int minutes, string image) =>
        new()
        {
            Name = name,
            NameEn = nameEn,
            Category = category,
            Description = description,
            DescriptionEn = descriptionEn,
            DurationMinutes = minutes,
            ImageUrl = image,
            Price = 0,
            Slug = LuxeSalon.Core.Utilities.SlugHelper.FromTitle(name),
            IsPublished = true
        };

    static void RefreshTeam(ApplicationDbContext context)
    {
        var wanted = new (string Name, string Role, string Bio, int Order, string Image)[]
        {
            ("Celil", "Kurucu, kesim ve renk", "Salonu yürüten isim.", 1, "https://images.unsplash.com/photo-1560066984-138dadb4c035?q=80&w=600&auto=format&fit=crop"),
            ("Bülent", "Stilist", "Kesim ve şekil.", 2, "https://images.unsplash.com/photo-1521590832167-7bcbfaa6381f?q=80&w=600&auto=format&fit=crop"),
            ("Tuğba", "Stilist ve manikür", "Saç ve tırnak.", 3, "https://images.unsplash.com/photo-1604654894610-df63bc536371?q=80&w=600&auto=format&fit=crop"),
            ("Sevgül", "Manikür", "Klasik manikür ve pedikür.", 4, "https://images.unsplash.com/photo-1632345031435-8727f6897d53?q=80&w=600&auto=format&fit=crop"),
            ("Rame", "Asistan", "Salon akışı.", 5, "https://images.unsplash.com/photo-1522337660859-02fbefca4702?q=80&w=600&auto=format&fit=crop"),
            ("Irmak", "Asistan", "Salon akışı.", 6, "https://images.unsplash.com/photo-1562322140-8baeececf3df?q=80&w=600&auto=format&fit=crop"),
        };

        var existing = context.TeamMembers.ToList();
        foreach (var e in existing)
            e.IsActive = false;

        foreach (var w in wanted)
        {
            var match = existing.FirstOrDefault(x =>
                x.Name.Equals(w.Name, StringComparison.OrdinalIgnoreCase) ||
                x.Name.StartsWith(w.Name + " ", StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                context.TeamMembers.Add(new TeamMember
                {
                    Name = w.Name,
                    Role = w.Role,
                    Bio = w.Bio,
                    DisplayOrder = w.Order,
                    ImageUrl = w.Image,
                    IsActive = true
                });
            }
            else
            {
                match.Name = w.Name;
                match.Role = w.Role;
                match.Bio = w.Bio;
                match.DisplayOrder = w.Order;
                match.ImageUrl = w.Image;
                match.IsActive = true;
            }
        }
        context.SaveChanges();
    }

    static void RefreshBlog(ApplicationDbContext context)
    {
        var posts = BlogContentSeed.Posts();
        var existing = context.BlogPosts.ToList();
        var activeImages = posts
            .Where(p => !string.IsNullOrWhiteSpace(p.HeaderImageUrl))
            .Select(p => p.HeaderImageUrl!)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var post in posts)
        {
            // HeaderImageUrl is the stable seed identity. Keeping the existing row
            // preserves indexed /Blog/Details/{id} URLs when editorial titles change.
            var match = existing.FirstOrDefault(p =>
                string.Equals(p.HeaderImageUrl, post.HeaderImageUrl, StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                context.BlogPosts.Add(post);
                continue;
            }

            match.Title = post.Title;
            match.Summary = post.Summary;
            match.Content = post.Content;
            match.Author = post.Author;
            match.IsPublished = post.IsPublished;
            match.PublishedAt = post.PublishedAt;

            if (string.IsNullOrWhiteSpace(match.Slug) || match.Slug.StartsWith("yazi-", StringComparison.OrdinalIgnoreCase))
                match.Slug = post.Slug;

            match.SlugEn = post.SlugEn;
            match.TitleEn = post.TitleEn;
            match.SummaryEn = post.SummaryEn;
            match.ContentEn = post.ContentEn;
            match.Tags = post.Tags;
            match.UpdatedAt = post.UpdatedAt;
        }

        foreach (var stale in existing.Where(p =>
                     string.IsNullOrWhiteSpace(p.HeaderImageUrl) ||
                     !activeImages.Contains(p.HeaderImageUrl)))
        {
            stale.IsPublished = false;
        }

        context.SaveChanges();
    }

    static void DeactivatePublicPrices(ApplicationDbContext context)
    {
        foreach (var item in context.PriceItems)
            item.IsActive = false;
        context.SaveChanges();
    }

    static void DeactivateSocialProof(ApplicationDbContext context)
    {
        foreach (var t in context.Testimonials)
            t.IsActive = false;
        foreach (var a in context.AwardItems)
            a.IsActive = false;
        context.SaveChanges();
    }

    static void RefreshMarquee(ApplicationDbContext context)
    {
        var texts = new[] { "Kadın kesim", "Renklendirme", "Bakım", "Erkek", "Çocuk", "Gelin", "Tırnak" };
        var existing = context.MarqueeItems.OrderBy(m => m.DisplayOrder).ToList();
        for (var i = 0; i < texts.Length; i++)
        {
            if (i < existing.Count)
            {
                existing[i].Text = texts[i];
                existing[i].DisplayOrder = i + 1;
                existing[i].IsActive = true;
            }
            else
            {
                context.MarqueeItems.Add(new MarqueeItem { Text = texts[i], DisplayOrder = i + 1, IsActive = true });
            }
        }
        for (var i = texts.Length; i < existing.Count; i++)
            existing[i].IsActive = false;
        context.SaveChanges();
    }

    static void RefreshStats(ApplicationDbContext context)
    {
        var wanted = new (string Value, string Label, int Order)[]
        {
            ("2000", "Kuruluş", 1),
            ("Bornova", "Tek salon", 2),
            ("6", "Kişi", 3),
        };
        var existing = context.StatItems.Where(s => s.Section == "Hakkimizda").OrderBy(s => s.DisplayOrder).ToList();
        for (var i = 0; i < wanted.Length; i++)
        {
            if (i < existing.Count)
            {
                existing[i].Value = wanted[i].Value;
                existing[i].Label = wanted[i].Label;
                existing[i].DisplayOrder = wanted[i].Order;
            }
            else
            {
                context.StatItems.Add(new StatItem
                {
                    Section = "Hakkimizda",
                    Value = wanted[i].Value,
                    Label = wanted[i].Label,
                    DisplayOrder = wanted[i].Order
                });
            }
        }
        context.SaveChanges();
    }

    static void RemoveFakeProducts(ApplicationDbContext context)
    {
        var fake = new[]
        {
            "Sculpting Clay No. 04", "Silk Infusion Serum", "Editorial Volume Mist",
            "Keratin Repair Mask", "Color Protect Şampuan", "Argan Oil Elixir"
        };
        var stale = context.Products.Where(p => fake.Contains(p.Name)).ToList();
        if (stale.Count > 0)
            context.Products.RemoveRange(stale);
        context.SaveChanges();
    }

    static void SeedSiteImages(ApplicationDbContext context)
    {
        var images = new List<(string Section, string Url, string? Title, int Order)>
        {
            ("Hero", "https://images.unsplash.com/photo-1560066984-138dadb4c035?q=80&w=1920&auto=format&fit=crop", "Celil & Atilla Bornova", 1),
            ("About", "https://images.unsplash.com/photo-1522337360788-8b13dee7a37e?q=80&w=800&auto=format&fit=crop", "Salon", 1),
            ("Marquee", "https://images.unsplash.com/photo-1521590832167-7bcbfaa6381f?q=80&w=1920&auto=format&fit=crop", "Marquee", 1),
            ("Pricing", "https://images.unsplash.com/photo-1521590832167-7bcbfaa6381f?q=80&w=800&auto=format&fit=crop", "Fiyat", 1),
            ("Booking", "https://images.unsplash.com/photo-1600948836101-f9ffda59d250?q=80&w=1920&auto=format&fit=crop", "Randevu", 1),
            ("Loader1", "https://images.unsplash.com/photo-1560066984-138dadb4c035?q=80&w=1920&auto=format&fit=crop", null, 1),
            ("Loader2", "https://images.unsplash.com/photo-1522337360788-8b13dee7a37e?q=80&w=1920&auto=format&fit=crop", null, 2),
            ("Loader3", "https://images.unsplash.com/photo-1595476108010-b4d1f102b1b1?q=80&w=1920&auto=format&fit=crop", null, 3),
            ("Loader4", "https://images.unsplash.com/photo-1580618672591-eb180b1a973f?q=80&w=1920&auto=format&fit=crop", null, 4),
            ("Service1", "/images/services/kadin-kesim.jpg", "Kadın kesim", 1),
            ("Service2", "/images/services/balyaj.jpg", "Renklendirme", 2),
            ("Service3", "/images/services/keratin.jpg", "Bakım", 3),
            ("Service4", "/images/services/erkek-kesim.jpg", "Erkek ve çocuk", 4),
            ("Hero_Hakkimizda", "https://images.unsplash.com/photo-1595476108010-b4d1f102b1b1?q=80&w=1920&auto=format&fit=crop", "Hakkımızda", 1),
            ("About_Hakkimizda", "https://images.unsplash.com/photo-1522337360788-8b13dee7a37e?q=80&w=800&auto=format&fit=crop", "Hikâye", 1),
            ("Cta_Hakkimizda", "https://images.unsplash.com/photo-1562322140-8baeececf3df?q=80&w=1920&auto=format&fit=crop", "CTA", 1),
            ("Hero_Hizmetler", "https://images.unsplash.com/photo-1560066984-138dadb4c035?q=80&w=1920&auto=format&fit=crop", "Hizmetler", 1),
            ("Hero_Randevu", "https://images.unsplash.com/photo-1560066984-138dadb4c035?q=80&w=1920&auto=format&fit=crop", "Randevu", 1),
            ("Hero_Blog", "https://images.unsplash.com/photo-1522337360788-8b13dee7a37e?q=80&w=1920&auto=format&fit=crop", "Blog", 1),
            ("Hero_Shop", "https://images.unsplash.com/photo-1594465919760-441fe5908ab0?q=80&w=1920&auto=format&fit=crop", "Mağaza", 1),
        };

        foreach (var (section, url, title, order) in images)
        {
            var existing = context.SiteImages.FirstOrDefault(i => i.Section == section);
            if (existing == null)
            {
                context.SiteImages.Add(new SiteImage
                {
                    Section = section,
                    ImageUrl = url,
                    Title = title,
                    AltText = title,
                    DisplayOrder = order,
                    IsActive = true
                });
            }
            else
            {
                existing.Title = title;
                existing.AltText = title;
                if (url.StartsWith("/images/"))
                    existing.ImageUrl = url;
            }
        }
        context.SaveChanges();
    }

    static void RefreshGallery(ApplicationDbContext context)
    {
        var portfolio = new (string Title, string Overlay, string Url)[]
        {
            ("Balyaj", "Balyaj", "/images/services/balyaj.jpg"),
            ("Kesim", "Kadın kesim", "/images/services/kadin-kesim.jpg"),
            ("Röfle", "Röfle ve tek renk", "/images/services/rofle.jpg"),
            ("Gelin", "Gelin saçı", "/images/services/gelin-sac.jpg"),
            ("Bakım", "Keratin", "/images/services/keratin.jpg"),
            ("Fade", "Erkek fade", "/images/services/erkek-kesim.jpg"),
        };

        var existingPortfolio = context.GalleryItems.Where(g => g.Section == "Portfolio").OrderBy(g => g.DisplayOrder).ToList();
        for (var i = 0; i < portfolio.Length; i++)
        {
            if (i < existingPortfolio.Count)
            {
                existingPortfolio[i].Title = portfolio[i].Title;
                existingPortfolio[i].OverlayText = portfolio[i].Overlay;
                existingPortfolio[i].ImageUrl = portfolio[i].Url;
                existingPortfolio[i].DisplayOrder = i + 1;
                existingPortfolio[i].IsActive = true;
            }
            else
            {
                context.GalleryItems.Add(new GalleryItem
                {
                    Section = "Portfolio",
                    Title = portfolio[i].Title,
                    OverlayText = portfolio[i].Overlay,
                    ImageUrl = portfolio[i].Url,
                    DisplayOrder = i + 1,
                    IsActive = true
                });
            }
        }
        for (var i = portfolio.Length; i < existingPortfolio.Count; i++)
            existingPortfolio[i].IsActive = false;

        var instagram = new[]
        {
            "/images/services/balyaj.jpg",
            "/images/services/kadin-kesim.jpg",
            "/images/services/keratin.jpg",
            "/images/services/gelin-sac.jpg",
            "/images/services/manikur.jpg",
            "/images/services/erkek-kesim.jpg",
        };
        var existingIg = context.GalleryItems.Where(g => g.Section == "Instagram").OrderBy(g => g.DisplayOrder).ToList();
        for (var i = 0; i < instagram.Length; i++)
        {
            if (i < existingIg.Count)
            {
                existingIg[i].ImageUrl = instagram[i];
                existingIg[i].DisplayOrder = i + 1;
                existingIg[i].IsActive = true;
            }
            else
            {
                context.GalleryItems.Add(new GalleryItem
                {
                    Section = "Instagram",
                    ImageUrl = instagram[i],
                    DisplayOrder = i + 1,
                    IsActive = true
                });
            }
        }
        for (var i = instagram.Length; i < existingIg.Count; i++)
            existingIg[i].IsActive = false;

        context.SaveChanges();
    }
}
