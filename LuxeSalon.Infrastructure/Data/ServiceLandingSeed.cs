using LuxeSalon.Core.Entities;

namespace LuxeSalon.Infrastructure.Data;

internal static class ServiceLandingSeed
{
    internal static List<Service> Pages() => new()
    {
        Landing(
            slug: "balyaj-ombre-sombre",
            slugEn: "balayage-ombre-sombre",
            name: "Balyaj, ombre ve sombre",
            nameEn: "Balayage, ombre and sombre",
            category: "Renklendirme",
            image: "/images/services/balyaj.jpg",
            minutes: 180,
            tags: "balyaj,ombre,renklendirme",
            description: "Doğal ışıltıdan belirgin uçlara: saçınızın geçmişi ve hedeflediğiniz geçişe göre balyaj, ombre veya sombre planlanır.",
            descriptionEn: "From soft dimension to bolder ends: balayage, ombre or sombre is planned around your hair history and the transition you want.",
            seoTitle: "Balyaj, Ombre ve Sombre | Celil & Atilla Bornova",
            seoTitleEn: "Balayage, Ombre & Sombre | Celil & Atilla Bornova",
            metaDescription: "Bornova Kazımdirik’te balyaj, ombre ve sombre renklendirme. Saç analizi sonrası doğal geçiş ve bakım planı.",
            metaDescriptionEn: "Balayage, ombre and sombre colour in Kazımdirik, Bornova. Hair analysis first, then a natural transition and aftercare plan.",
            longTr: BuildLong(
                intent: "Balyaj, ombre ve sombre aynı işlem değildir. Balyaj ışığı saça serbest biçimde yerleştirir; ombre koyu dipten açık uçlara daha belirgin bir çizgi kurar; sombre ise bu kontrastı yumuşatır. Kazımdirik’teki salonumuzda seçim, yalnızca referans fotoğrafa değil saçın mevcut rengine, daha önce açılıp açılmadığına ve evde ayırabileceğiniz bakım süresine göre yapılır.",
                process: "Randevu öncesi saç geçmişinizi ve hedef tonu konuşuruz. İşlem günü saç analizi yapılır, tutam yoğunluğu ve açılış seviyesi netleştirilir. Balyajda folyo veya serbest el tekniği; ombre ve sombrede geçiş hattının yüksekliği birlikte belirlenir. Açıcı ve tonlama aşamaları saçın tepkisine göre ilerler; gerekirse ikinci seans planlanır.",
                suitability: "Doğal uzama çizgisi isteyen, dip doğal kalsın diyenler için balyaj güçlü bir seçenektir. Daha belirgin uç kontrastı ombre; yumuşak ama fark edilir değişim sombre ile gelir. Koyu boya, ev boyası veya sık açıcı geçmişi tek seansta sınır koyabilir — bu nedenle dürüst bir ön görüşme şarttır.",
                aftercare: "Renk koruyucu şampuan, düzenli nem desteği ve kontrollü ısı kullanımı tonun canlı kalmasına yardımcı olur. Mor şampuan her yıkamada değil, ihtiyaç halinde kullanılır. Dip uzadıkça rötuş veya ton yenileme ayrı planlanır.",
                faq: new[]
                {
                    ("Balyaj ile ombre aynı gün yapılır mı?", "Genelde tek teknik seçilir. Karışık beklenti varsa prova görüşmesinde netleştirilir."),
                    ("Ne kadar sürer?", "Saç uzunluğu ve boya geçmişine göre çoğunlukla 2–3 saat; düzeltme gereken saçlarda daha uzun olabilir."),
                    ("Bornova’da neredesiniz?", "Kazımdirik Mahallesi, Gediz Caddesi 21 A — Celil & Atilla’nın tek salon adresi.")
                },
                ctaTr: "Balyaj, ombre veya sombre için <a href=\"/randevu\">randevu oluşturun</a> veya 0534 794 43 93 üzerinden güncel saç fotoğrafınızı paylaşın."),
            longEn: BuildLongEn(
                intent: "Balayage, ombre and sombre are not interchangeable. Balayage places light freely through the hair; ombre builds a clearer shift from darker roots to lighter ends; sombre softens that contrast. At our Kazımdirik salon, the choice depends on your current colour, previous lightening and the upkeep you can realistically keep up at home — not just a reference photo.",
                process: "We discuss your colour history and target tone before the appointment. On the day we assess the hair, confirm foil or freehand placement and how high the transition should sit. Lightener and toning steps follow the hair’s response; a second session is booked when needed rather than forced in one visit.",
                suitability: "Balayage suits clients who want a softer grow-out line. Ombre works when you prefer more visible end contrast; sombre sits between the two. Heavy box dye, dark permanent colour or repeated bleach can limit what is safe in a single session — an honest consultation matters.",
                aftercare: "Colour-safe shampoo, regular moisture and controlled heat help the tone stay fresh. Purple shampoo is used when needed, not every wash. As roots grow, a root refresh or gloss can be scheduled separately.",
                faq: new[]
                {
                    ("Can I combine balayage and ombre in one visit?", "Usually one technique is chosen. If you want a hybrid look, we clarify that in a consultation first."),
                    ("How long does it take?", "Most appointments run 2–3 hours depending on length and history; colour correction can take longer."),
                    ("Where are you located?", "Gediz Caddesi 21 A, Kazımdirik — the only Celil & Atilla address in Bornova.")
                },
                ctaEn: "Book <a href=\"/en/appointment\">balayage, ombre or sombre</a> or send a recent hair photo via 0534 794 43 93.")),

        Landing(
            slug: "kadin-sac-kesimi",
            slugEn: "womens-haircut",
            name: "Kadın saç kesimi",
            nameEn: "Women's haircut",
            category: "Kadın Kesim",
            image: "/images/services/kadin-kesim.jpg",
            minutes: 60,
            tags: "kadin-kesim,kahkul,fon",
            description: "Yüz hattı, saç yoğunluğu ve günlük şekillendirme alışkanlığınıza göre planlanan kadın saç kesimi.",
            descriptionEn: "Women's haircuts planned around face shape, density and how much time you spend styling each morning.",
            seoTitle: "Kadın Saç Kesimi | Celil & Atilla Bornova",
            seoTitleEn: "Women's Haircut | Celil & Atilla Bornova",
            metaDescription: "Bornova’da yüze ve saç yapısına uygun kadın kesimi, kat ve kahkül. Kazımdirik Gediz Cd. 21 A.",
            metaDescriptionEn: "Face-shape-led women's haircuts in Bornova — layers, bangs and everyday wearability. Gediz Cd. 21 A, Kazımdirik.",
            longTr: BuildLong(
                intent: "İyi bir kadın saç kesimi, trend isminden önce sizin yüzünüzde ve günlük hayatınızda nasıl duracağıyla ilgilidir. Kat, kahkül ve uzunluk; saçın doğal yönü, yoğunluğu ve nemde aldığı form birlikte değerlendirilir. Bornova Kazımdirik’te kesime başlamadan önce neyi sevdiğinizi kadar neyi istemediğinizi de konuşuruz.",
                process: "Konsültasyonda referans fotoğraf, günlük bakım süreniz ve son kesim deneyiminiz dinlenir. Saç ıslak ve kuru kontrol edilir; katların başlangıç noktası belirlenir. Kesim sonrası fön veya şekillendirme isteğe bağlıdır. Kahkül ayrı planlanıyorsa mevcut kesimle birlikte düşünülür.",
                suitability: "Kalın saçta kat ağırlığı dağıtabilir; ince saçta yanlış kat uçları zayıf gösterebilir — yoğunluk kişiye göre ayarlanır. Kısa kesim her zaman daha az bakım demek değildir; uzun saçta doğru uç temizliği hafiflik sağlayabilir.",
                aftercare: "Kesimden sonra evde kullandığınız ısı ve fırçalama alışkanlığı formu korur. Kahkül düzenli düzeltme ister. Uçlar büyüdükçe hacim dağılabilir; 6–8 haftada bir rötuş önerilir.",
                faq: new[]
                {
                    ("Kahkül aynı randevuda yapılır mı?", "Evet, kesimle birlikte planlanabilir; yoğunluk ve bakım beklentisi önceden konuşulmalıdır."),
                    ("Fön dahil mi?", "Kesim süresine dahildir; ayrıca detaylı şekillendirme istenirse süre uzatılabilir."),
                    ("Salon nerede?", "Gediz Caddesi 21 A, Kazımdirik Mahallesi — Bornova’daki tek adresimiz.")
                },
                ctaTr: "<a href=\"/randevu\">Kadın saç kesimi randevusu alın</a> · 0534 794 43 93"),
            longEn: BuildLongEn(
                intent: "A good women's haircut is about how the shape lives on your face and in your routine — not the name of a trend. Layers, bangs and length are weighed against natural growth pattern, density and how the hair behaves in humidity. In Kazımdirik we talk about what you want to avoid as clearly as what you like.",
                process: "Consultation covers reference photos, styling time and your last cut. We check wet and dry shape, then place layers where they will actually move. Blow-dry or basic styling can finish the visit; bangs are planned as part of the overall silhouette when needed.",
                suitability: "Layers can remove bulk on thick hair but can look wispy on fine hair if placed too high. Short does not always mean low maintenance; long hair can feel lighter with the right internal layers and clean ends.",
                aftercare: "Heat tools and brushing habits at home keep the shape. Bangs need regular trims. Ends lose structure as they grow — a refresh every 6–8 weeks is typical.",
                faq: new[]
                {
                    ("Can bangs be cut in the same visit?", "Yes, when planned upfront so density and upkeep are realistic."),
                    ("Is blow-dry included?", "Basic finish is part of the cut; detailed styling can extend the appointment."),
                    ("Where is the salon?", "Gediz Caddesi 21 A, Kazımdirik — our only Bornova location.")
                },
                ctaEn: "<a href=\"/en/appointment\">Book a women's haircut</a> · 0534 794 43 93")),

        Landing(
            slug: "keratin-sac-botoksu",
            slugEn: "keratin-hair-botox",
            name: "Keratin bakımı ve saç botoksu",
            nameEn: "Keratin treatment and hair botox",
            category: "Bakım",
            image: "/images/services/keratin.jpg",
            minutes: 180,
            tags: "keratin,sac-botoksu,bakim",
            description: "Kabarma kontrolü, parlaklık ve yıpranmış uç desteği: keratin ve saç botoksu saç analizi sonrası seçilir.",
            descriptionEn: "Frizz control, shine and mid-length repair — keratin and hair botox are chosen after a proper hair assessment.",
            seoTitle: "Keratin ve Saç Botoksu | Celil & Atilla Bornova",
            seoTitleEn: "Keratin & Hair Botox | Celil & Atilla Bornova",
            metaDescription: "Bornova’da keratin bakımı ve iğnesiz saç botoksu. Tıbbi botoks değildir; saç yapısına göre planlanır.",
            metaDescriptionEn: "Keratin smoothing and needle-free hair botox in Bornova. Not medical botox — planned for your hair structure.",
            longTr: BuildLong(
                intent: "Keratin bakımı kabarma ve form üzerinde çalışır; saç botoksu ise yıpranmış boy ve uçlara dolgunluk ile parlaklık kazandırmayı hedefler. İkisi de tıbbi botoks değildir — iğne kullanılmayan kozmetik saç bakımıdır. Kazımdirik’teki Celil & Atilla’da seçim, yalnızca işlem adına göre değil saçın esnekliği ve boya geçmişine göre yapılır.",
                process: "Saç analizi ve hedef görüşmesi yapılır. Keratin uygulamasında ürün saça işlenir, bekletme ve fön/ütü aşamaları protokole göre tamamlanır. Saç botoksunda besleyici krem bazlı ürün boy ve uçlara odaklanır. Süre saç uzunluğu ve yoğunluğuna bağlıdır.",
                suitability: "Kabaran, nemle dağılan saç keratin bakımından fayda görebilir. Mat, kuru uçlar ve inceleşmiş görünüm saç botoksuna yönelebilir. Çok kırılgan veya aşırı açılmış saçta önce onarım planı gerekebilir.",
                aftercare: "İlk yıkama zamanı ve kullanılacak şampuan işlem sonrası net söylenir. Sülfatsız veya salon önerili ürünler formu korur. Sık ısı ve sert fırçalama sonucu kısaltır.",
                faq: new[]
                {
                    ("Saç botoksu tıbbi botoks mudur?", "Hayır. İğnesiz, kozmetik saç bakımıdır."),
                    ("Keratin her saça aynı düzlüğü verir mi?", "Hayır. Sonuç saç kalınlığı, kıvrımı ve geçmiş işlemlere bağlıdır."),
                    ("Adresiniz?", "Kazımdirik Mah. Gediz Cd. 21 A, Bornova.")
                },
                ctaTr: "<a href=\"/hizmetler\">Bakım hizmetlerini inceleyin</a> veya <a href=\"/randevu\">randevu alın</a>."),
            longEn: BuildLongEn(
                intent: "Keratin treatments focus on frizz and shape; hair botox targets fullness and shine on tired mid-lengths and ends. Neither is medical botox — both are topical salon care. At Celil & Atilla in Kazımdirik we pick the service from hair elasticity and colour history, not from menu names alone.",
                process: "We assess the hair and agree on the goal. Keratin follows product protocol with processing time and heat sealing. Hair botox uses a cream-based treatment concentrated on mid-lengths and ends. Timing depends on length and density.",
                suitability: "Frizzy, humidity-reactive hair often suits keratin. Dull, dry ends may benefit from hair botox. Very fragile or over-lightened hair may need repair first.",
                aftercare: "First-wash timing and shampoo choice are explained at checkout. Salon-recommended or sulphate-free care extends results. Excessive heat and rough brushing shorten them.",
                faq: new[]
                {
                    ("Is hair botox the same as injectable botox?", "No. It is a needle-free cosmetic hair treatment."),
                    ("Will keratin make every hair pin-straight?", "No. Results vary with thickness, curl and previous chemical work."),
                    ("Where are you?", "Gediz Cd. 21 A, Kazımdirik, Bornova.")
                },
                ctaEn: "See <a href=\"/en/services\">care services</a> or <a href=\"/en/appointment\">book an appointment</a>.")),

        Landing(
            slug: "rofle-kok-rotusu",
            slugEn: "highlights-root-touch-up",
            name: "Röfle ve kök rötuşu",
            nameEn: "Highlights and root touch-up",
            category: "Renklendirme",
            image: "/images/services/rofle.jpg",
            minutes: 120,
            tags: "rofle,kok-rotusu,renklendirme",
            description: "Düzenli ışıltı veya dip dengesi: röfle yoğunluğu ve kök rötuşu aralığı saçınıza göre planlanır.",
            descriptionEn: "Regular dimension or root balance — highlight placement and touch-up timing are tailored to your hair.",
            seoTitle: "Röfle ve Kök Rötuşu | Celil & Atilla Bornova",
            seoTitleEn: "Highlights & Root Touch-Up | Celil & Atilla Bornova",
            metaDescription: "Bornova Kazımdirik’te röfle, ince paket açılımı ve kök rötuşu. Beyaz kapatma ve ton yenileme ayrı değerlendirilir.",
            metaDescriptionEn: "Foily highlights and root touch-ups in Kazımdirik, Bornova. Grey coverage and gloss refresh assessed separately.",
            longTr: BuildLong(
                intent: "Röfle seçilmiş tutamların kontrollü açılmasıdır; kök rötuşu ise uzayan doğal saçı mevcut boyalı uzunluklarla yeniden dengelemek içindir. İkisi aynı randevuda olabilir fakat ihtiyaç farklıdır. Gediz Caddesi 21 A’daki salonumuzda beyaz oranı, kontrast ve önceki boya yükü birlikte okunur.",
                process: "Renk geçmişi sorulur; gerekirse güncel fotoğraf istenir. Röflede paket sıklığı ve yüz çevresi açıklığı belirlenir. Kök rötuşunda yalnızca diplere mi yoksa ton yenileme de mi gerektiği netleştirilir. Açıcı ve boya bekleme süreleri saçın tepkisine göre izlenir.",
                suitability: "Düzenli ışıltı isteyenler ince veya kalın paket röfleye yönelebilir. Yüksek kontrastlı dip hattı erken rötuş gerektirir. Koyu boya geçmişi hedef tonu tek seansta sınırlayabilir.",
                aftercare: "Renk koruyucu ürün ve düzenli nem desteği açık tutamları canlı tutar. Mor şampuan gereğinden fazla kullanılmamalıdır. Dip uzamasına göre 4–6 haftada kök rötuşu planlanabilir.",
                faq: new[]
                {
                    ("Kök rötuşu kaç haftada bir?", "Beyaz oranı ve kontrasta göre genelde 4–6 hafta; kişisel tolerans değişir."),
                    ("Röfle ile balyaj aynı mı?", "Hayır. Röfle daha düzenli tutam dağılımı; balyaj daha serbest ışık yerleşimi sunar."),
                    ("Randevu nasıl alınır?", "0534 794 43 93, WhatsApp veya <a href=\"/randevu\">randevu formu</a>.")
                },
                ctaTr: "<a href=\"/randevu\">Renklendirme randevusu oluşturun</a>."),
            longEn: BuildLongEn(
                intent: "Highlights lift selected sections in a controlled pattern; a root touch-up rebalances growing natural hair with previously coloured lengths. They can happen in one visit but solve different needs. At Gediz Caddesi 21 A we read grey percentage, contrast and previous colour load together.",
                process: "We ask colour history and may request a recent photo. Foil density and face-framing lightness are set for highlights. For roots we clarify whether only the regrowth needs colour or if mids and ends need a gloss too. Processing is monitored on the hair.",
                suitability: "Clients wanting regular dimension choose fine or bold foil patterns. High root contrast needs earlier refresh. Dark permanent history can cap what is achievable in one session.",
                aftercare: "Colour-safe care and moisture keep highlights bright. Purple shampoo only when needed. Root touch-ups often sit on a 4–6 week rhythm depending on contrast.",
                faq: new[]
                {
                    ("How often should roots be done?", "Often every 4–6 weeks depending on grey and contrast — personal tolerance varies."),
                    ("Are highlights the same as balayage?", "No. Highlights use a more regular foil pattern; balayage is freer placement."),
                    ("How do I book?", "Call or WhatsApp 0534 794 43 93 or use the <a href=\"/en/appointment\">booking form</a>.")
                },
                ctaEn: "<a href=\"/en/appointment\">Book a colour appointment</a>.")),

        Landing(
            slug: "gelin-saci",
            slugEn: "bridal-hair",
            name: "Gelin saçı",
            nameEn: "Bridal hair",
            category: "Özel Gün",
            image: "/images/services/gelin-sac.jpg",
            minutes: 90,
            tags: "gelin,ozel-gun,makyaj",
            description: "Toplu, yarım açık veya duvak altı gelin saçı; prova ile düğün günü sürprize bırakılmaz.",
            descriptionEn: "Updos, half-up styles and veil-friendly bridal hair — trial sessions keep wedding day calm.",
            seoTitle: "Gelin Saçı | Celil & Atilla Bornova",
            seoTitleEn: "Bridal Hair | Celil & Atilla Bornova",
            metaDescription: "Bornova’da gelin saçı provası ve düğün günü uygulaması. Duvak yerleşimi ve saç uzunluğuna göre plan.",
            metaDescriptionEn: "Bridal hair trials and wedding-day styling in Bornova. Veil placement planned for your length and density.",
            longTr: BuildLong(
                intent: "Gelin saçı referans fotoğrafın birebir kopyası değildir; sizin uzunluğunuz, yoğunluğunuz ve duvağın oturduğu yer esastır. Prova, modeli kilitlemek ve düğün günü sürpriz bırakmamak içindir. Kazımdirik’te prova ve düğün günü randevuları ayrı planlanır.",
                process: "Prova randevusunda model, aksesuar ve duvak konuşulur; saç denemesi yapılır. Düğün günü saç hazırlığı süresi, gelin dışında hazırlanacak kişi sayısına göre genişletilir. Makyaj aynı salonda istenirse ayrı süre ayrılır.",
                suitability: "Toplu saç yoğun ve uzun saçta daha rahat oturur; ince saçta hacim desteği gerekebilir. Yarım açık modeller orta uzunlukta esneklik sunar. Prova özellikle ilk kez topuz deneyenler için önerilir.",
                aftercare: "Düğün günü saç spreyi ve pin kullanımı modele göre değişir; provada hangi ürünlerin taşınacağı konuşulur. Prova sonrası saç yıkama zamanı ve ev bakımı kısaca anlatılır.",
                faq: new[]
                {
                    ("Prova zorunlu mu?", "Zorunlu değil ama düğün günü riskini ciddi azaltır."),
                    ("Ne zaman yazmalıyım?", "Tarih netleşince; hafta sonu slotları erken dolar."),
                    ("Anne veya kardeş için de yapılır mı?", "Evet, ek kişiler için ayrı süre planlanır.")
                },
                ctaTr: "<a href=\"/randevu\">Gelin saçı provası için randevu isteyin</a> · Gediz Cd. 21 A"),
            longEn: BuildLongEn(
                intent: "Bridal hair is not a copy-paste of a Pinterest board — your length, density and where the veil sits define what works. A trial locks the shape so wedding morning stays predictable. In Kazımdirik we schedule trials and wedding-day appointments separately.",
                process: "Trial day covers style, accessories and veil; we test the shape on your hair. Wedding-day timing expands if bridesmaids or family members are styled too. Makeup can be booked in the same salon with its own time block.",
                suitability: "Full updos sit more easily on thick, longer hair; finer hair may need volume support. Half-up styles offer flexibility at medium length. Trials are especially helpful if updos are new to you.",
                aftercare: "Hairspray and pin count depend on the style — we note what to bring on the day during the trial. Basic wash-and-care notes follow the trial if needed.",
                faq: new[]
                {
                    ("Is a trial mandatory?", "No, but it sharply reduces wedding-day guesswork."),
                    ("When should I book?", "As soon as your date is fixed — weekends fill early."),
                    ("Can family members be styled too?", "Yes, with extra time added per person.")
                },
                ctaEn: "<a href=\"/en/appointment\">Request a bridal hair trial</a> · Gediz Cd. 21 A, Kazımdirik")),

        Landing(
            slug: "sac-kaynagi",
            slugEn: "hair-extensions",
            name: "Saç kaynağı",
            nameEn: "Hair extensions",
            category: "Saç Kaynağı",
            image: "/images/services/kaynak.jpg",
            minutes: 180,
            tags: "sac-kaynagi,uzunluk,yogunluk",
            description: "Uzunluk veya yoğunluk için saç kaynağı; renk eşleşmesi ve taşıma gücü önce değerlendirilir.",
            descriptionEn: "Length or fullness with hair extensions — colour match and hair strength are assessed first.",
            seoTitle: "Saç Kaynağı | Celil & Atilla Bornova",
            seoTitleEn: "Hair Extensions | Celil & Atilla Bornova",
            metaDescription: "Bornova Kazımdirik’te saç kaynağı görüşmesi. Uzunluk, yoğunluk ve renk uyumu saç analiziyle planlanır.",
            metaDescriptionEn: "Extension consultations in Kazımdirik, Bornova. Length, density and colour blending planned after hair analysis.",
            longTr: BuildLong(
                intent: "Saç kaynağı yalnızca uzunluk eklemez; doğru uygulandığında yoğunluk ve form da kazandırır. Doğal görünüm bağlantı dağılımına, renk eşleşmesine ve son kesime bağlıdır. Her saça kaynak uygulanmaz — kırılgan veya ağır açıcı geçmişi olan saç önce değerlendirilir.",
                process: "Hedef uzunluk veya yoğunluk netleştirilir. Mevcut saç rengi dip, boy ve uç farklarıyla okunur; gerekirse birden fazla ton karıştırılır. Bağlantılar yükü eşit dağıtacak şekilde yerleştirilir. Sonrasında bütünleyici kesim geçişi gizler.",
                suitability: "İnce saçta amaç her zaman bel hizası uzunluk olmayabilir; daha az tutamla yan doluluk yeterli gelebilir. Çok kırılgan saçta ertelemek daha doğru olabilir.",
                aftercare: "Bağlantı noktalarını çekiştirmeden taramak, ıslakken toplamamak ve bakım ürününü diplere yığmamak gerekir. Söküm ve bakım tarihi yönteme göre planlanır.",
                faq: new[]
                {
                    ("Her saça yapılır mı?", "Hayır. Taşıma gücü yoksa önce bakım veya bekleme önerilir."),
                    ("Renk nasıl eşleşir?", "Tek hazır ton nadiren yeter; karışım ve son kesim birlikte düşünülür."),
                    ("Salon adresi?", "Kazımdirik Mah. Gediz Cd. 21 A — Bornova.")
                },
                ctaTr: "<a href=\"/randevu\">Saç kaynağı görüşmesi için randevu isteyin</a>."),
            longEn: BuildLongEn(
                intent: "Extensions add length and, when placed well, density and shape. A natural result depends on even bond distribution, colour blending and a finishing cut. Not every head of hair is ready — fragile or recently over-lightened hair may need a pause.",
                process: "We clarify whether you want length, fullness or both. We read root, mid and end colour separately and may blend multiple tones. Bonds are spaced to share weight. A softening cut hides the transition line.",
                suitability: "Fine hair may need fewer pieces for side fullness rather than waist-length weight. Very brittle hair may be better served with repair first.",
                aftercare: "Brush gently at bonds, avoid tight updos on wet hair and do not pile conditioner at the roots. Maintenance and removal dates follow the method used.",
                faq: new[]
                {
                    ("Can anyone get extensions?", "Not always — if the hair cannot carry bonds, we suggest repair or waiting."),
                    ("How is colour matched?", "Rarely one off-the-shelf shade; blending and a cut finish the look."),
                    ("Where are you?", "Gediz Cd. 21 A, Kazımdirik, Bornova.")
                },
                ctaEn: "<a href=\"/en/appointment\">Book an extension consultation</a>.")),

        Landing(
            slug: "erkek-kesim-fade",
            slugEn: "mens-cut-fade",
            name: "Erkek kesim ve fade",
            nameEn: "Men's cut and fade",
            category: "Erkek",
            image: "/images/services/erkek-kesim.jpg",
            minutes: 40,
            tags: "erkek,fade,sakal",
            description: "Makine–makas, fade ve skin fade; yan geçiş ile tepe uzunluğu birlikte planlanır.",
            descriptionEn: "Clipper-scissor blends, fades and skin fades — sides and top planned as one shape.",
            seoTitle: "Erkek Kesim ve Fade | Celil & Atilla Bornova",
            seoTitleEn: "Men's Haircut & Fade | Celil & Atilla Bornova",
            metaDescription: "Bornova’da erkek saç kesimi, fade ve sakal hattı. Kazımdirik Gediz Cd. 21 A.",
            metaDescriptionEn: "Men's haircuts, fades and beard lines in Bornova. Gediz Cd. 21 A, Kazımdirik.",
            longTr: BuildLong(
                intent: "Erkek kesiminde fade yalnızca makine numarası değildir; deriye ne kadar yaklaşıldığı ve tepeyle yanın nasıl bağlandığıdır. Sakal hattı aynı çizgide düşünülmezse yüz dengesi bozulur. Celil & Atilla’da düzenli bakım isteyen erkek müşteriler için saç, sakal ve hafif boya aynı randevuda planlanabilir.",
                process: "Yanların açıklığı ve tepe uzunluğu konuşulur; referans fotoğraf yardımcı olur. Fade geçişi makine ve makasla birleştirilir. Sakal ve boyun hattı isteğe göre düzenlenir. Erkek boya ayrı karışım ve süre ister.",
                suitability: "İş ve okul için daha kısa yanlar; daha belirgin fade isteyenler skin fade değerlendirebilir. Seyrek sakalda hat çizgisi gerçekçi tutulur.",
                aftercare: "3–4 haftada bir yan tazeleme formu korur. Sakal ve saç aynı ritimde giderse yüz çerçevesi dengeli kalır.",
                faq: new[]
                {
                    ("Sakal aynı randevuda olur mu?", "Evet, süreye eklenir."),
                    ("Erkek boya yapılır mı?", "Evet — beyaz kapatma veya hafif ton; doğal görünüm hedeflenir."),
                    ("Konum?", "Gediz Caddesi 21 A, Kazımdirik / Bornova.")
                },
                ctaTr: "<a href=\"/randevu\">Erkek kesim veya fade randevusu oluşturun</a>."),
            longEn: BuildLongEn(
                intent: "A fade is not just a guard number — it is how close the sides sit to the skin and how they meet the top. Beard lines should follow the same geometry or the face looks unbalanced. At Celil & Atilla, regular clients often combine cut, beard tidy and subtle colour in one visit.",
                process: "We agree on side length and top weight; photos help. Fades blend clipper and scissor work. Beard and neck lines are optional add-ons. Men's colour uses a separate mix and timing.",
                suitability: "Conservative side lengths suit work and school; skin fades suit bolder contrast. Sparse beards get realistic edge work rather than sharp lines that grow out awkwardly.",
                aftercare: "Side refreshes every 3–4 weeks keep the shape. Matching beard and hair cadence keeps the frame even.",
                faq: new[]
                {
                    ("Can beard work be added?", "Yes — time is added to the appointment."),
                    ("Do you offer men's colour?", "Yes — grey blending or subtle tone with a natural finish."),
                    ("Where are you?", "Gediz Caddesi 21 A, Kazımdirik, Bornova.")
                },
                ctaEn: "<a href=\"/en/appointment\">Book a men's cut or fade</a>.")),

        Landing(
            slug: "cocuk-kesimi",
            slugEn: "kids-haircut",
            name: "Çocuk saç kesimi",
            nameEn: "Kids' haircut",
            category: "Çocuk",
            image: "/images/services/cocuk.jpg",
            minutes: 25,
            tags: "cocuk-kesim",
            description: "Kız ve erkek çocuk kesimi; kısa, sakin randevu için doğru saat ve net model fotoğrafı önerilir.",
            descriptionEn: "Haircuts for girls and boys — a calm visit starts with timing and a clear reference photo.",
            seoTitle: "Çocuk Saç Kesimi | Celil & Atilla Bornova",
            seoTitleEn: "Kids' Haircut | Celil & Atilla Bornova",
            metaDescription: "Bornova Kazımdirik’te çocuk saç kesimi. Okul çıkışı ve erken saat randevuları; ebeveyn kesimi aynı gün planlanabilir.",
            metaDescriptionEn: "Kids' haircuts in Kazımdirik, Bornova. After-school slots; parent cuts can be booked the same day.",
            longTr: BuildLong(
                intent: "Çocuk kesiminde süre kısadır; bekleme uzarsa iş zorlaşır. Okul çıkışı veya erken saat genelde daha sakindir. Ebeveyn kesimi aynı güne yazılabilir — iki ayrı süre ayırmak gerekir. Kazımdirik’teki salonumuzda net bir model fotoğrafı süreci hızlandırır.",
                process: "Randevu saati çocuğun daha rahat olduğu aralığa alınır. Model fotoğrafı veya kısa tarif alınır. Kesim hızlı ve sakin tempoda yapılır. Kahkül veya yan detay varsa baştan belirtilir.",
                suitability: "İlk kesim deneyimi olan çocuklar için kısa oturumlar uygundur. Çok hareketli günlerde ertelemek bazen daha iyidir.",
                aftercare: "Evde düzenli uç tazeleme; çocuk saçının hızlı uzamasına göre 4–6 haftada bir tekrar.",
                faq: new[]
                {
                    ("Ebeveyn aynı gün kesilebilir mi?", "Evet, iki ayrı randevu süresi planlanır."),
                    ("Ne getirmeliyim?", "Nasıl kestirmek istediğinizi gösteren bir fotoğraf yeterli."),
                    ("Çalışma saatleri?", "Pzt–Cmt 08:30–20:00, Pazar 10:00–19:00 — Gediz Cd. 21 A.")
                },
                ctaTr: "<a href=\"/randevu\">Çocuk saç kesimi için uygun saat sorun</a>."),
            longEn: BuildLongEn(
                intent: "Kids' cuts are short appointments — long waits make them harder. After-school or early slots are usually calmer. A parent can book their own cut the same day with a separate time block. A clear photo beats a film reference at our Kazımdirik salon.",
                process: "We book when the child is usually calmest. You bring a photo or simple description. The cut moves at a steady, unhurried pace. Bangs or side details are noted upfront.",
                suitability: "First-time cuts work best as brief visits. On restless days, rescheduling can be kinder than forcing it.",
                aftercare: "Light trims at home between visits; most children return every 4–6 weeks depending on growth.",
                faq: new[]
                {
                    ("Can a parent get cut the same day?", "Yes — two time blocks are scheduled."),
                    ("What should we bring?", "One photo showing the length and shape you want."),
                    ("Opening hours?", "Mon–Sat 08:30–20:00, Sun 10:00–19:00 — Gediz Cd. 21 A.")
                },
                ctaEn: "<a href=\"/en/appointment\">Ask for a calm kids' slot</a>."))
    };

    static Service Landing(
        string slug,
        string slugEn,
        string name,
        string nameEn,
        string category,
        string image,
        int minutes,
        string tags,
        string description,
        string descriptionEn,
        string seoTitle,
        string seoTitleEn,
        string metaDescription,
        string metaDescriptionEn,
        string longTr,
        string longEn) =>
        new()
        {
            Name = name,
            NameEn = nameEn,
            Slug = slug,
            SlugEn = slugEn,
            Description = description,
            DescriptionEn = descriptionEn,
            LongDescription = longTr,
            LongDescriptionEn = longEn,
            SeoTitle = seoTitle,
            SeoTitleEn = seoTitleEn,
            MetaDescription = metaDescription,
            MetaDescriptionEn = metaDescriptionEn,
            Tags = tags,
            Category = category,
            ImageUrl = image,
            DurationMinutes = minutes,
            IsLandingPage = true,
            IsPublished = true,
            Price = 0,
            UpdatedAt = DateTime.UtcNow
        };

    static string BuildLong(
        string intent,
        string process,
        string suitability,
        string aftercare,
        (string Q, string A)[] faq,
        string ctaTr)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("<h2>Ne için?</h2><p>").Append(intent).Append("</p>");
        sb.Append("<h2>Nasıl ilerler?</h2><p>").Append(process).Append("</p>");
        sb.Append("<h2>Kimler için uygun?</h2><p>").Append(suitability).Append("</p>");
        sb.Append("<h2>Sonrasında bakım</h2><p>").Append(aftercare).Append("</p>");
        sb.Append("<h2>Sık sorulanlar</h2>");
        foreach (var (q, a) in faq)
            sb.Append("<p><strong>").Append(q).Append("</strong> ").Append(a).Append("</p>");
        sb.Append("<p>").Append(ctaTr).Append("</p>");
        return sb.ToString();
    }

    static string BuildLongEn(
        string intent,
        string process,
        string suitability,
        string aftercare,
        (string Q, string A)[] faq,
        string ctaEn)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("<h2>What it's for</h2><p>").Append(intent).Append("</p>");
        sb.Append("<h2>How it works</h2><p>").Append(process).Append("</p>");
        sb.Append("<h2>Who it's for</h2><p>").Append(suitability).Append("</p>");
        sb.Append("<h2>Aftercare</h2><p>").Append(aftercare).Append("</p>");
        sb.Append("<h2>FAQ</h2>");
        foreach (var (q, a) in faq)
            sb.Append("<p><strong>").Append(q).Append("</strong> ").Append(a).Append("</p>");
        sb.Append("<p>").Append(ctaEn).Append("</p>");
        return sb.ToString();
    }
}
