using LuxeSalon.Core.Entities;
using LuxeSalon.Core.Utilities;

namespace LuxeSalon.Infrastructure.Data;

internal static class BlogContentSeed
{
    internal static List<BlogPost> Posts() => new List<BlogPost>()
    {
        Post(
            "Balyaj, Ombre ve Sombre Arasındaki Farklar",
            "Doğal bir geçiş mi, daha belirgin açık uçlar mı? Balyaj, ombre ve sombre arasındaki farkları saçınızın geçmişi ve istediğiniz sonuç üzerinden anlatıyoruz.",
            @"<p>“Doğal dursun, sarıya kaçmasın ve dip çıktığında sert bir çizgi oluşmasın.” Renklendirme görüşmelerinde en sık duyduğumuz beklenti bu. <strong>Balyaj</strong>, <strong>ombre</strong> ve <strong>sombre</strong> benzer görünse de ışığın saça yerleşme biçimi ve geçişin belirginliği farklıdır.</p>
<h2>Balyaj: saça yerleşen doğal ışık</h2>
<p>Balyaj, seçilen tutamların serbest el tekniğiyle açılmasıdır. Yüz çevresi aydınlatılabilir, dip doğal bırakılabilir ve açık tonlar saçın hareketine göre dağıtılabilir. Daha yumuşak uzama çizgisi isteyenler için güçlü bir seçenektir.</p>
<h2>Ombre: koyu dipten açık uçlara belirgin geçiş</h2>
<p>Ombrede renk değişimi özellikle boy ve uçlarda daha görünürdür. Kontrast isteyen, açık uçları öne çıkarmayı seven saçlarda tercih edilir. Önceden boyalı saçta hedef tona tek seansta ulaşılıp ulaşılamayacağı saç analiziyle netleşir.</p>
<h2>Sombre: ombrenin daha yumuşak yorumu</h2>
<p>Sombrede geçiş çizgisi geri plandadır; tonlar birbirine daha yakın tutulur. “Değişiklik belli olsun ama keskin görünmesin” diyenler için dengeli bir alternatiftir.</p>
<h2>Hangi teknik saçınıza uygun?</h2>
<p>Kararı yalnızca örnek fotoğraf vermez. Saçın mevcut rengi, daha önce açılıp açılmadığı, yoğunluğu ve evde ayırabileceğiniz bakım süresi birlikte değerlendirilir. Kazımdirik Mahallesi’ndeki salonumuzda renk görüşmesi bu nedenle işlemden önce yapılır.</p>
<p><strong>İşlem süresi:</strong> Saçın uzunluğu ve boya geçmişine göre çoğunlukla 2–3 saat aralığındadır. Renk düzeltme gereken saçlarda birden fazla seans planlanabilir.</p>
<p><a href=""/randevu"">Balyaj, ombre veya sombre için randevu talebi oluşturun</a> ya da 0534 794 43 93 üzerinden saçınızın güncel fotoğrafını paylaşın. Celil &amp; Atilla, Gediz Caddesi 21 A, Kazımdirik / Bornova.</p>",
            "/images/services/balyaj.jpg",
            DateTime.UtcNow.AddDays(-2),
            "balayage-ombre-sombre-differences",
            "Balayage, Ombre and Sombre: What's the Difference?",
            "Soft dimension or bolder ends? We explain balayage, ombre and sombre through your hair history and the result you actually want.",
            @"<p>“Keep it natural, avoid brass and don’t let a harsh line show at the roots.” That is the most common brief we hear in colour consultations. <strong>Balayage</strong>, <strong>ombre</strong> and <strong>sombre</strong> look similar in photos, but light sits differently on the hair and the transition reads differently in real life.</p>
<h2>Balayage: light painted through the hair</h2>
<p>Balayage lifts selected sections freehand. Face-framing pieces can brighten the front, roots can stay natural and lighter tones follow how your hair moves. It suits anyone who wants a softer grow-out line.</p>
<h2>Ombre: a clearer shift from dark roots to light ends</h2>
<p>With ombre, the colour change is more visible through the lengths and ends. It works when you like contrast and want lighter tips to stand out. On previously coloured hair, whether the target tone is realistic in one session is decided after a proper analysis.</p>
<h2>Sombre: ombre with a gentler edge</h2>
<p>Sombre keeps the transition line quieter; tones stay closer together. It is a balanced option if you want change that shows, but not a sharp band.</p>
<h2>Which technique fits your hair?</h2>
<p>A reference photo alone does not decide it. Current colour, past lightening, density and the upkeep you can manage at home are weighed together. That is why we always consult before colour at our Kazımdirik salon.</p>
<p><strong>Timing:</strong> Most visits run 2–3 hours depending on length and history; correction can need more than one session.</p>
<p><a href=""/en/appointment"">Request a balayage, ombre or sombre appointment</a> or send a recent hair photo on 0534 794 43 93. Celil &amp; Atilla, Gediz Caddesi 21 A, Kazımdirik / Bornova.</p>",
            "balyaj,ombre,renklendirme"),

        Post(
            "Keratin Bakımı mı, Saç Botoksu mu?",
            "Kabarmayı kontrol etmek, parlaklığı geri kazanmak veya yıpranmış saçı desteklemek aynı ihtiyaç değildir. Keratin, saç botoksu ve saç dolgusunu doğru beklentiyle karşılaştırın.",
            @"<p>Elektriklenen, matlaşan veya işlemlerden sonra gücünü kaybeden saç için tek bir bakım reçetesi yoktur. Keratin, saç botoksu ve saç dolgusu farklı ihtiyaçlara cevap verir. Buradaki <strong>saç botoksu tıbbi bir uygulama değildir</strong>; iğne kullanılmayan kozmetik saç bakımıdır.</p>
<h2>Keratin bakımı ne zaman tercih edilir?</h2>
<p>Keratin bakımı kabarma ve nemle dağılan form üzerinde çalışır. Saçın daha kontrollü taranmasına ve daha düzenli görünmesine yardımcı olur. Sonucun korunması kullanılan şampuan ve evde uygulanan ısıyla doğrudan ilişkilidir.</p>
<h2>Saç botoksu ne sağlar?</h2>
<p>Saç botoksu, yıpranmış görünen boy ve uçlara dolgunluk ile parlaklık kazandırmayı hedefleyen krem bazlı bir bakımdır. Saçın formunu keratin kadar değiştirmez; bakım görünümünü öne çıkarır.</p>
<h2>Saç dolgusu hangi ihtiyaca yönelir?</h2>
<p>İncelmiş ve boşalmış hissedilen tutamlarda bakım desteği için değerlendirilir. Kırılma devam ediyorsa önce ısı, açıcı ve ev bakım alışkanlıkları gözden geçirilir.</p>
<h2>Doğru bakımı saç analizi belirler</h2>
<p>Kazımdirik’teki Celil &amp; Atilla’da bakım seçimi, yalnızca işlem adına göre yapılmaz. Saçın esnekliği, gözenekliliği, boya geçmişi ve beklenen görünüm birlikte değerlendirilir.</p>
<p><a href=""/hizmetler"">Saç bakım hizmetlerini inceleyin</a> veya randevu öncesi 0534 794 43 93 üzerinden bilgi alın. Adres: Gediz Caddesi 21 A, Bornova.</p>",
            "/images/services/keratin.jpg",
            DateTime.UtcNow.AddDays(-8),
            "keratin-treatment-vs-hair-botox",
            "Keratin Treatment or Hair Botox?",
            "Frizz control, shine and repair are not the same need. Compare keratin, hair botox and filler-style care with the right expectations.",
            @"<p>There is no single fix for hair that frizzes, dulls or feels tired after chemical work. Keratin, hair botox and filler-style treatments answer different briefs. <strong>Hair botox here is not medical botox</strong> — it is needle-free salon care.</p>
<h2>When is keratin the right choice?</h2>
<p>Keratin work targets frizz and shape in humidity. Hair usually combs more calmly and looks more even. How long it lasts depends heavily on shampoo choice and heat at home.</p>
<h2>What does hair botox do?</h2>
<p>Hair botox is a cream-based treatment aimed at fullness and shine on worn mid-lengths and ends. It does not reshape hair like keratin; it brings back a healthy, cared-for look.</p>
<h2>When is filler-style care considered?</h2>
<p>It is discussed when sections feel thin or hollow. If breakage continues, we look at heat, lightener history and home habits first.</p>
<h2>Analysis picks the service</h2>
<p>At Celil &amp; Atilla in Kazımdirik we do not choose from menu names alone. Elasticity, porosity, colour history and the look you want are read together.</p>
<p>See our <a href=""/en/services"">hair care services</a> or call 0534 794 43 93 before booking. Gediz Caddesi 21 A, Bornova.</p>",
            "keratin,sac-botoksu,bakim"),

        Post(
            "Yüz Şekline ve Saç Yapısına Göre Kadın Saç Kesimi",
            "Kat, kahkül ve uzunluk tek başına model değildir. İyi bir kadın saç kesimi; yüz hattı, saç yoğunluğu ve günlük şekillendirme alışkanlığı birlikte düşünülerek planlanır.",
            @"<p>Bir modelin fotoğrafta güzel görünmesi, sizin saçınızda aynı sonucu vereceği anlamına gelmez. Kesimin taşıyıcısı yüz hattı kadar saçın yoğunluğu, doğal yönü ve her sabah şekillendirmeye ayırdığınız süredir.</p>
<h2>Katlı kesim hareketi nasıl değiştirir?</h2>
<p>Katlar kalın saçta ağırlığı dağıtabilir, ince saçta ise yanlış yerleştirildiğinde uçları daha zayıf gösterebilir. Bu nedenle katın başladığı nokta ve yoğunluğu kişiye göre belirlenir.</p>
<h2>Kahkül seçerken nelere bakılır?</h2>
<p>Alın genişliği, saçın çıkış yönü ve nemde aldığı form önemlidir. Perde kahkül daha yumuşak bir çerçeve kurarken kısa kahkül daha düzenli düzeltme ister.</p>
<h2>Kısa saç mı, uzun saç mı?</h2>
<p>Kısa kesim her zaman daha az bakım demek değildir. Saçın doğal formu ve kullanım alışkanlığı uygun değilse günlük şekillendirme süresi artabilir. Uzun saçta ise doğru uç temizliği ve iç katlarla daha hafif bir görünüm sağlanabilir.</p>
<h2>Kesim görüşmesi sonucu değiştirir</h2>
<p>Bornova Kazımdirik’teki salonumuzda kesime başlamadan önce neyi sevdiğinizi kadar neyi istemediğinizi de konuşuruz. Böylece model adı yerine günlük hayatta kullanabileceğiniz forma odaklanırız.</p>
<p><a href=""/randevu"">Kadın saç kesimi randevusu alın</a>. Celil &amp; Atilla, Gediz Caddesi 21 A · 0534 794 43 93.</p>",
            "/images/services/kadin-kesim.jpg",
            DateTime.UtcNow.AddDays(-14),
            "womens-haircut-face-shape",
            "Women's Haircuts by Face Shape and Hair Type",
            "Layers, bangs and length are not a style on their own. A good women's cut weighs face shape, density and your daily styling routine together.",
            @"<p>A cut that looks great in a photo is not guaranteed to live the same way on your hair. Face shape matters, but so do density, growth direction and the minutes you spend styling each morning.</p>
<h2>How layers change movement</h2>
<p>Layers can remove weight on thick hair; placed too high on fine hair they can make ends look wispy. Where layers start and how many you carry is personal.</p>
<h2>Choosing bangs</h2>
<p>Forehead width, growth pattern and behaviour in humidity all matter. Curtain bangs frame softly; shorter bangs need more frequent trims.</p>
<h2>Short or long?</h2>
<p>Short is not always low maintenance — if your natural shape fights the silhouette, daily styling can take longer. Long hair can feel lighter with clean ends and internal layers.</p>
<h2>Consultation changes the outcome</h2>
<p>At our Kazımdirik salon we discuss what you dislike as clearly as what you like before scissors touch the hair. The goal is a shape you can actually wear, not a trend label.</p>
<p><a href=""/en/appointment"">Book a women's haircut</a>. Celil &amp; Atilla, Gediz Caddesi 21 A · 0534 794 43 93.</p>",
            "kadin-kesim,kahkul"),

        Post(
            "Fade, Erkek Saç Kesimi ve Sakal Uyumu",
            "Fade yalnızca makine numarası değildir. Yan geçiş, tepe uzunluğu ve sakal hattı birlikte planlandığında daha temiz ve dengeli bir görünüm oluşur.",
            @"<p>Erkek tarafı “üç numara” ile bitmez. Fade, skin fade, makine–makas karışımı aynı ailedir; fark, deriye ne kadar yaklaşıldığı ve tepeyle yanın nasıl bağlandığıdır.</p>
<h2>Kesim ve fade</h2>
<p>İş, okul veya saha için farklı uzunluk istenir. Fotoğraf göstermek işe yarar; “ünlü gibi” yerine “yanlar bu kadar açık” daha nettir.</p>
<h2>Sakal ve tıraş</h2>
<p>Sakal hattı, yanak ve boyun ayrı işlenir. Tam tıraş ile sadece sınır düzeltmek aynı süreye sığmayabilir.</p>
<h2>Erkek boya</h2>
<p>Beyaz kapatma veya hafif ton, kadın global boyadan farklı karışım ister. “Hiç belli olmasın” ile “biraz kapansın” ayrı sonuçtur.</p>
<h2>Kesim ile sakal aynı çizgide olmalı</h2>
<p>Favori geçişi, yanak hattı ve boyun temizliği birbirinden kopuk yapılmaz. Kazımdirik’te düzenli bakım isteyen erkek müşteriler için saç, sakal ve beyaz kapatma aynı randevuda planlanabilir.</p>
<p><a href=""/randevu"">Erkek kesim veya fade randevusu oluşturun</a>. Celil &amp; Atilla, Gediz Caddesi 21 A, Bornova · 0534 794 43 93.</p>",
            "/images/services/erkek-kesim.jpg",
            DateTime.UtcNow.AddDays(-20),
            "fade-mens-haircut-beard",
            "Fade, Men's Haircuts and Beard Balance",
            "A fade is not just a clipper grade. When sides, top and beard line are planned together, the frame looks cleaner and more balanced.",
            @"<p>Men's grooming is not “a number three all over.” Fade, skin fade and clipper-scissor blends share a family; the difference is how close the sides sit and how they meet the top.</p>
<h2>Cut and fade</h2>
<p>Work, school and sport ask for different lengths. Photos help — “this open on the sides” beats “like a celebrity.”</p>
<h2>Beard and shave</h2>
<p>Beard line, cheek and neck are separate decisions. A full shave and a line tidy do not always fit the same time block.</p>
<h2>Men's colour</h2>
<p>Grey blending or subtle tone uses a different mix than global women's colour. “Invisible” and “slightly covered” are different targets.</p>
<h2>Hair and beard should share one line</h2>
<p>Temple fade, cheek edge and neck cleanup are not done in isolation. Regular clients in Kazımdirik often book cut, beard and light colour in one visit.</p>
<p><a href=""/en/appointment"">Book a men's cut or fade</a>. Celil &amp; Atilla, Gediz Caddesi 21 A, Bornova · 0534 794 43 93.</p>",
            "erkek,fade,sakal"),

        Post(
            "Gelin Saçı ve Makyajında Prova Neden Önemli?",
            "Düğün günü ilk deneme günü değildir. Prova; saç modeli, duvak yerleşimi, ten uyumu ve günün akışını önceden netleştirir.",
            @"<p>Gelin saçı, Instagram’daki modelin saçı değildir. Sizin uzunluğunuz, yoğunluğunuz ve duvağın oturduğu yerdir.</p>
<h2>Prova ne işe yarar?</h2>
<p>Prova modeli kilitlemek içindir. Toplu saç mı, yarım açık mı, duvak altı mı — o gün değil, önce konuşulur. Makyaj ten ve gelinlik tonuna göre ayarlanır.</p>
<h2>Ne zaman yazmalısınız?</h2>
<p>Tarih netleşince yazın. Hafta sonu slotları erken dolar. Anne veya kardeş için saç/makyaj ayrı süre ister.</p>
<h2>Aynı çatıda</h2>
<p>İsterseniz manikür veya kaş da aynı salonda kalır. Protez tırnak ve kalıcı oje menüde yoktur.</p>
<h2>Bornova’da gelin hazırlığı için plan</h2>
<p>Kazımdirik’teki salonumuzda prova ve düğün günü randevuları ayrı planlanır. Hazırlanacak diğer kişiler varsa toplam süreye baştan eklenir.</p>
<p><a href=""/randevu"">Gelin saçı ve makyajı için tarih sorun</a>. Celil &amp; Atilla, Gediz Caddesi 21 A · 0534 794 43 93.</p>",
            "/images/services/gelin-sac.jpg",
            DateTime.UtcNow.AddDays(-26),
            "bridal-hair-makeup-trial",
            "Why a Bridal Hair and Makeup Trial Matters",
            "Wedding day is not the first rehearsal. A trial locks the style, veil placement, skin match and timeline before the morning rush.",
            @"<p>Bridal hair is not the hair in an Instagram grid. It is your length, your density and where the veil actually sits.</p>
<h2>What a trial does</h2>
<p>A trial exists to lock the shape — updo, half-up or under-veil — before the day, not on it. Makeup is adjusted to skin and dress tone.</p>
<h2>When to book</h2>
<p>Book once your date is fixed. Weekend slots fill early. Hair or makeup for mum or a sister needs its own time.</p>
<h2>Under one roof</h2>
<p>Manicure or brows can stay in the same salon if you wish. We do not offer extensions or gel polish.</p>
<h2>Planning bridal prep in Bornova</h2>
<p>At our Kazımdirik salon, trial and wedding-day appointments are scheduled separately. Extra guests are added to the timeline upfront.</p>
<p><a href=""/en/appointment"">Ask about bridal hair and makeup dates</a>. Celil &amp; Atilla, Gediz Caddesi 21 A · 0534 794 43 93.</p>",
            "gelin,ozel-gun,makyaj"),

        Post(
            "Klasik Manikür ve Spa Pedikür Arasındaki Fark",
            "Düzenli el ve ayak bakımı için hangi işlem uygun? Klasik manikür, pedikür ve spa pedikürün kapsamını net biçimde karşılaştırıyoruz.",
            @"<p>Tırnak menüsü şişirilmez. Klasik manikür, klasik pedikür ve spa pedikür vardır. <strong>Kalıcı oje ve protez tırnak yapılmaz.</strong></p>
<h2>Klasik manikür</h2>
<p>Şekil, kutikül, tırnak eti ve oje (isterseniz). Kısa, düzenli bir bakımdır.</p>
<h2>Pedikür</h2>
<p>Klasik pedikür nasır ve tırnak düzenine odaklanır. Spa pedikürde bekletme ve yumuşatma artar. Medikal ayak tedavisi değildir; ağrı veya mantar hekime aittir.</p>
<h2>Saç randevusuyla birlikte planlanabilir</h2>
<p>Kazımdirik’teki salonumuzda manikür veya pedikür, uygunluk varsa saç işlemiyle aynı güne alınabilir. Protez tırnak ve kalıcı oje hizmetimiz yoktur.</p>
<p>Randevu: 0534 794 43 93 · Celil &amp; Atilla, Gediz Caddesi 21 A, Bornova.</p>",
            "/images/services/manikur.jpg",
            DateTime.UtcNow.AddDays(-32),
            "classic-manicure-spa-pedicure",
            "Classic Manicure vs Spa Pedicure",
            "Which hand and foot treatment fits a regular routine? We compare classic manicure, pedicure and spa pedicure clearly.",
            @"<p>We keep the nail menu honest: classic manicure, classic pedicure and spa pedicure. <strong>No gel polish or nail extensions.</strong></p>
<h2>Classic manicure</h2>
<p>Shape, cuticle work, nail edges and polish if you want it — a tidy, regular upkeep visit.</p>
<h2>Pedicure</h2>
<p>Classic pedicure focuses on callus care and nail order. Spa pedicure adds longer soak and soften steps. Neither is medical foot care — pain or fungal issues belong with a doctor.</p>
<h2>Can it sit beside a hair appointment?</h2>
<p>At our Kazımdirik salon, manicure or pedicure can share a day with hair work when timing allows. Extensions and gel polish are not offered.</p>
<p>Call 0534 794 43 93 · Celil &amp; Atilla, Gediz Caddesi 21 A, Bornova.</p>",
            "manikur,pedikur,tirnak"),

        Post(
            "Çocuk Saç Kesiminde Rahat Bir Randevu İçin Öneriler",
            "Kız ve erkek çocuk kesiminde doğru saat, kısa bekleme ve net bir model fotoğrafı süreci kolaylaştırır. Rahat bir randevu için küçük hazırlıklar.",
            @"<p>Çocuk kesiminde süre kısadır. Bekleme uzarsa iş zorlaşır. Randevuyu okul çıkışına veya erken saate almak genelde daha sakin olur.</p>
<h2>Ne getirin?</h2>
<p>Nasıl kestiğimizi gösteren bir fotoğraf yeter. “Filmdeki gibi” yerine “kulak üzeri” veya “kahkül kalmasın” daha işe yarar.</p>
<h2>Aynı salonda ebeveyn</h2>
<p>Ebeveyn kesimi veya boyası ile çocuğu aynı güne yazmak mümkün; iki süre ayırın. Pazar 10:00–19:00, hafta içi ve Cumartesi 08:30–20:00.</p>
<h2>Kazımdirik’te çocuk kesimi randevusu</h2>
<p>Çocuğun daha rahat olduğu saat aralığını seçin ve mümkünse yoğun saatleri beklemeyin. Ebeveyn kesimi veya boyası da aynı gün yapılacaksa iki ayrı süre ayırmak gerekir.</p>
<p><a href=""/randevu"">Çocuk saç kesimi için uygun saat sorun</a>. Celil &amp; Atilla, Gediz Caddesi 21 A, Bornova · 0534 794 43 93.</p>",
            "/images/services/cocuk.jpg",
            DateTime.UtcNow.AddDays(-38),
            "kids-haircut-appointment-tips",
            "Tips for a Calm Kids' Haircut Appointment",
            "For girls and boys, the right time slot, minimal waiting and a clear photo make the visit easier. Small prep steps help.",
            @"<p>Kids' cuts are short appointments — long waits make them harder. After-school or early slots are usually calmer.</p>
<h2>What to bring</h2>
<p>One photo showing the length you want is enough. “Above the ear” or “no bangs” beats “like in a film.”</p>
<h2>Parent in the same salon</h2>
<p>You can book your cut or colour the same day; allow two time blocks. Sun 10:00–19:00, Mon–Sat 08:30–20:00.</p>
<h2>Kids' cuts in Kazımdirik</h2>
<p>Pick a window when your child is usually calm and avoid peak waits when you can. Parent services the same day need separate timing.</p>
<p><a href=""/en/appointment"">Ask for a calm kids' slot</a>. Celil &amp; Atilla, Gediz Caddesi 21 A, Bornova · 0534 794 43 93.</p>",
            "cocuk-kesim"),

        Post(
            "Kuaför Randevusundan Önce Neleri Belirtmelisiniz?",
            "İşlem adı, saçın güncel durumu ve tercih edilen saat: doğru bilgiyi baştan paylaşmak randevu süresinin daha net planlanmasını sağlar.",
            @"<p>Randevu mesajının uzun olması gerekmez; doğru bilgiyi içermesi yeterlidir. İşlem türü, saçın güncel durumu ve tercih ettiğiniz saat aralığı planlamayı hızlandırır.</p>
<h2>İşlemi mümkün olduğunca net yazın</h2>
<p>“Saç yaptırmak istiyorum” yerine balyaj, kök rötuşu, kadın kesimi, keratin bakımı veya erkek fade gibi işlem adını belirtin. Birden fazla işlem düşünüyorsanız aynı mesajda yazın.</p>
<h2>Renk ve bakım için saç geçmişini paylaşın</h2>
<p>Daha önce açıcı, koyu boya veya ev boyası kullanıldıysa belirtin. Gün ışığında çekilmiş güncel bir saç fotoğrafı, özellikle renk işlemlerinde ilk değerlendirmeyi kolaylaştırır.</p>
<h2>Birinci ve ikinci saat tercihinizi ekleyin</h2>
<p>Celil &amp; Atilla pazartesiden cumartesiye 08:30–20:00, pazar günü 10:00–19:00 arasında açıktır. İkinci bir saat seçeneği yoğun günlerde daha hızlı eşleşme sağlar.</p>
<h2>Üç kanaldan randevu alın</h2>
<p>Kazımdirik Mahallesi’ndeki salonumuza telefonla, WhatsApp üzerinden veya <a href=""/randevu"">randevu formundan</a> ulaşabilirsiniz.</p>
<p>Celil &amp; Atilla · Gediz Caddesi 21 A, 35100 Bornova / İzmir · 0534 794 43 93.</p>",
            "/images/services/fon.jpg",
            DateTime.UtcNow.AddDays(-1),
            "what-to-share-before-salon-appointment",
            "What to Tell Us Before You Book",
            "Service name, current hair condition and preferred time: sharing the right details upfront helps us plan your visit accurately.",
            @"<p>Your message does not need to be long — it needs to be useful. Service type, current hair condition and a preferred time window speed up planning.</p>
<h2>Name the service clearly</h2>
<p>Instead of “I need my hair done,” say balayage, root touch-up, women's cut, keratin care or a men's fade. List multiple services in one message if you are considering them.</p>
<h2>Share colour and care history</h2>
<p>Mention past lightener, dark dye or box colour. A recent photo in daylight helps enormously for colour work.</p>
<h2>Offer a first and second time choice</h2>
<p>We are open Mon–Sat 08:30–20:00 and Sun 10:00–19:00. A backup slot helps on busy days.</p>
<h2>Three ways to reach us</h2>
<p>Call, WhatsApp or use the <a href=""/en/appointment"">booking form</a> for our Kazımdirik salon.</p>
<p>Celil &amp; Atilla · Gediz Caddesi 21 A, 35100 Bornova / İzmir · 0534 794 43 93.</p>",
            "randevu,genel"),

        Post(
            "Röfle Nedir? Balyajdan Farkı ve Bakım İpuçları",
            "Röfle daha düzenli ve belirgin tutamlar, balyaj ise daha serbest bir ışık geçişi oluşturur. Saçınıza uygun tekniği seçmeden önce farkları ve bakım ihtiyacını öğrenin.",
            @"<p><strong>Röfle</strong>, saçın seçilmiş tutamlarının dipten uca kontrollü biçimde açılmasıdır. Balyajdan temel farkı, ışıklı tutamların daha düzenli ve tekrarlayan bir dağılım göstermesidir. Sonucun ince, belirgin veya yoğun görünmesi kullanılan paket sıklığına ve ton seçimine bağlıdır.</p>
<h2>Röfle ile balyaj arasındaki temel fark nedir?</h2>
<p>Röfle çoğunlukla folyoyla ve dipten başlayan seçili tutamlarla uygulanır. Balyajda ise açık tonlar saçın hareketine göre daha serbest yerleştirilir; dip daha doğal bırakılabilir. Düzenli bir ışıltı isteyenler röfleye, daha yumuşak ve dağınık bir geçiş isteyenler balyaja yönelebilir.</p>
<h2>İnce röfle mi, belirgin röfle mi?</h2>
<p>İnce paketler doğal saçla daha kolay karışır ve genel görünümü aydınlatır. Kalın veya sık paketler daha yüksek kontrast oluşturur. Yüz çevresinde kullanılan açıklık ise ten tonu ve saç kesimiyle birlikte değerlendirilmelidir.</p>
<h2>Önceden boyalı saça röfle yapılır mı?</h2>
<p>Yapılabilir; ancak koyu boya, ev boyası veya daha önce uygulanmış açıcı sonucu doğrudan etkiler. Saçın esnekliği zayıfsa hedef ton tek seansta zorlanmamalıdır. Ön görüşmede boya geçmişini eksiksiz paylaşmak bu nedenle önemlidir.</p>
<h2>Röfle sonrası bakım nasıl olmalı?</h2>
<p>Renk koruyucu ürün, düzenli nem desteği ve kontrollü ısı kullanımı açık tutamların parlak görünmesine yardımcı olur. Mor şampuan her yıkamada kullanılmaz; gereğinden fazla kullanım saçı mat ve kuru gösterebilir.</p>
<h2>Renk kararını birlikte netleştirelim</h2>
<p>Kazımdirik Mahallesi’ndeki salonumuzda röfle yoğunluğu saçın mevcut rengi, kesimi ve beklenen bakım düzenine göre planlanır. <a href=""/randevu"">Renklendirme randevusu oluşturun</a> veya güncel saç fotoğrafınızı 0534 794 43 93 üzerinden paylaşın.</p>
<p>Celil &amp; Atilla · Gediz Caddesi 21 A, Bornova.</p>",
            "/images/services/rofle.jpg",
            DateTime.UtcNow,
            "highlights-vs-balayage-care",
            "What Are Highlights? How They Differ from Balayage",
            "Highlights use a more regular foil pattern; balayage paints light more freely. Learn the difference and the upkeep each needs.",
            @"<p><strong>Highlights</strong> lift chosen sections in a controlled way from root to tip. The main difference from balayage is a more regular, repeating pattern of lighter pieces. Whether the result reads fine, bold or dense depends on foil spacing and tone choice.</p>
<h2>Highlights vs balayage</h2>
<p>Highlights usually use foils from the root. Balayage places light more freely and can leave roots softer. Regular sparkle often suits highlights; softer, scattered light often suits balayage.</p>
<h2>Fine or bold highlights?</h2>
<p>Fine foils blend with natural colour and brighten overall. Bold or dense foils create higher contrast. Face-framing lightness should be weighed with skin tone and cut.</p>
<h2>Can highlighted hair be done on coloured hair?</h2>
<p>Often yes — but dark dye, box colour and past lightener change the ceiling. Weak elasticity should not be forced in one session. Full colour history in consultation matters.</p>
<h2>Aftercare</h2>
<p>Colour-safe products, moisture and controlled heat keep lights bright. Purple shampoo is for when you need it, not every wash — overuse can look dull and dry.</p>
<h2>Let's plan your colour together</h2>
<p>At our Kazımdirik salon, highlight density follows your current colour, cut and upkeep rhythm. <a href=""/en/appointment"">Book a colour appointment</a> or send a recent photo on 0534 794 43 93.</p>
<p>Celil &amp; Atilla · Gediz Caddesi 21 A, Bornova.</p>",
            "rofle,balyaj,renklendirme"),

        Post(
            "Kök Rötuşu Ne Zaman Yapılmalı?",
            "Kök rötuşunun zamanı yalnızca takvime bağlı değildir. Beyaz oranı, doğal saç rengi ve boyalı bölümle oluşan kontrast doğru aralığı belirler.",
            @"<p><strong>Kök rötuşu</strong>, uzayan doğal saçın daha önce boyanmış uzunluklarla yeniden dengelenmesidir. Çoğu kişide ihtiyaç 4–6 hafta içinde belirginleşir; ancak beyaz oranı, renk kontrastı ve saçın uzama hızı bu süreyi değiştirebilir.</p>
<h2>Kök rötuşu kaç haftada bir yapılır?</h2>
<p>Açık renk boya ile koyu doğal saç arasındaki fark yüksekse dip daha erken görünür. Beyaz kapatmada da kişisel tolerans önemlidir. Düşük kontrastlı tonlarda süre uzayabilir. Saç derisini gereksiz yere sık işleme sokmamak için yalnızca takvime değil, gerçek dip uzunluğuna bakılır.</p>
<h2>Boya neden yalnızca diplere uygulanır?</h2>
<p>Daha önce boyanmış uzunluklara her seferinde kalıcı boya taşımak uçlarda renk birikimine, koyulaşmaya ve kuruluğa yol açabilir. İhtiyaç yalnızca uzayan bölümdeyse işlem diplerle sınırlandırılır; boy ve uçlara gerektiğinde tonlama veya bakım uygulanır.</p>
<h2>Beyaz kapatma ile ton yenileme aynı şey mi?</h2>
<p>Hayır. Beyaz kapatma doğal saçtaki beyazları hedefler. Ton yenileme ise solan boy ve uç rengini canlandırır. İki ihtiyaç aynı randevuda değerlendirilebilir fakat kullanılan ürün ve bekleme süresi farklı olabilir.</p>
<h2>Randevu öncesi ne belirtmelisiniz?</h2>
<p>Kullandığınız boya tonu, son işlemin tarihi ve saçta kına ya da ev boyası bulunup bulunmadığı önemlidir. Farklı bir renge geçmek istiyorsanız bunu rötuş başlamadan önce söyleyin.</p>
<h2>Kazımdirik’te kök rötuşu randevusu</h2>
<p>Celil &amp; Atilla’da dip rengi, beyaz oranı ve mevcut boyalı uzunluk birlikte değerlendirilir. <a href=""/randevu"">Kök rötuşu için uygun saat seçin</a> veya 0534 794 43 93 üzerinden bilgi alın.</p>
<p>Gediz Caddesi 21 A, Kazımdirik / Bornova.</p>",
            "/images/services/tek-renk.jpg",
            DateTime.UtcNow.AddDays(-3),
            "root-touch-up-timing",
            "When Should You Book a Root Touch-Up?",
            "Root refresh timing is not calendar-only. Grey percentage, natural depth and contrast with coloured lengths set the right interval.",
            @"<p>A <strong>root touch-up</strong> rebalances growing natural hair with previously coloured lengths. Many clients feel the need within 4–6 weeks, but grey density, contrast and growth speed shift that window.</p>
<h2>How often?</h2>
<p>Light dye on dark natural hair shows early. Grey coverage tolerance is personal. Low-contrast tones can stretch longer. We look at actual regrowth, not the diary alone, to avoid over-processing the scalp.</p>
<h2>Why colour only the roots?</h2>
<p>Repeating permanent colour on already coloured lengths can build up, darken ends and dry them out. When only the regrowth needs work, we stay at the roots and gloss or treat mids and ends if needed.</p>
<h2>Is grey coverage the same as a gloss refresh?</h2>
<p>No. Grey coverage targets natural white hair. A gloss refresh revives faded mids and ends. Both can happen in one visit but may use different products and timings.</p>
<h2>What to mention before booking</h2>
<p>Share your shade, last colour date and any henna or box dye. Say early if you want to change direction — not mid-appointment.</p>
<h2>Root touch-ups in Kazımdirik</h2>
<p>At Celil &amp; Atilla we read root colour, grey percentage and existing lengths together. <a href=""/en/appointment"">Pick a root touch-up slot</a> or call 0534 794 43 93.</p>
<p>Gediz Caddesi 21 A, Kazımdirik / Bornova.</p>",
            "kok-rotusu,renklendirme"),

        Post(
            "Saç Kaynağı Öncesi Bilmeniz Gerekenler",
            "Kaynak yalnızca uzunluk eklemez; doğru uygulandığında yoğunluk ve form da kazandırabilir. Uygun yöntem için saçın taşıma gücü, renk uyumu ve bakım rutini değerlendirilmelidir.",
            @"<p><strong>Saç kaynağı</strong>, mevcut saça uzunluk veya yoğunluk eklemek için uygulanır. Doğal görünüm; yalnızca kullanılan saçın kalitesine değil, bağlantıların doğru dağıtılmasına, renk eşleşmesine ve son kesime bağlıdır.</p>
<h2>Her saça kaynak uygulanır mı?</h2>
<p>Hayır. Çok kırılgan, yoğun dökülme yaşayan veya yakın zamanda ağır açıcı işlemi görmüş saç önce değerlendirilmelidir. Bağlantıların taşıyabileceği sağlamlık yoksa uygulamayı ertelemek daha doğru olabilir.</p>
<h2>Uzunluk mu, yoğunluk mu istediğinizi belirleyin</h2>
<p>İnce saçta amaç her zaman bel hizasında uzunluk değildir. Bazen yanları ve uçları dolduran daha az sayıda tutam, saçın bütün görünmesini sağlar. Hedef baştan netleştiğinde gereksiz ağırlık önlenir.</p>
<h2>Renk uyumu neden önemlidir?</h2>
<p>Tek bir hazır ton çoğu saçla kusursuz eşleşmez. Dip, boy ve uçlardaki renk farklılıkları dikkate alınarak birden fazla ton karıştırılabilir. Kaynak sonrası yapılan bütünleyici kesim de geçiş çizgisini gizler.</p>
<h2>Kaynak saçın bakımı nasıl yapılır?</h2>
<p>Bağlantı noktalarını çekiştirmeden taramak, saçı ıslakken toplamamak ve bakım ürününü diplere yığmamak gerekir. Gece gevşek örgü, dolaşmayı azaltabilir. Bakım ve söküm tarihi kullanılan yönteme ve saçın uzama hızına göre planlanır.</p>
<h2>Uygulama öncesi saç analizi</h2>
<p>Bornova’daki tek adresimiz olan Kazımdirik Mahallesi, Gediz Caddesi 21 A’da kaynak öncesi uzunluk, yoğunluk ve renk hedefini birlikte değerlendiriyoruz. <a href=""/randevu"">Saç kaynağı görüşmesi için randevu isteyin</a> veya 0534 794 43 93 üzerinden güncel fotoğraf gönderin.</p>",
            "/images/services/kaynak.jpg",
            DateTime.UtcNow.AddDays(-5),
            "hair-extensions-before-you-book",
            "What to Know Before Hair Extensions",
            "Extensions are not only about length — done well they add density and shape too. Strength, colour match and home care must be assessed first.",
            @"<p><strong>Hair extensions</strong> add length or density to your own hair. A natural result depends on even bond placement and colour blending as much as hair quality — and on the finishing cut.</p>
<h2>Can every head of hair carry extensions?</h2>
<p>No. Very fragile hair, heavy shedding or recent aggressive lightening should be reviewed first. If bonds cannot be supported safely, waiting is the honest answer.</p>
<h2>Length or fullness?</h2>
<p>On fine hair the goal is not always waist length — fewer pieces at the sides can make hair look whole. Clarifying the brief upfront avoids unnecessary weight.</p>
<h2>Why colour matching matters</h2>
<p>One off-the-shelf shade rarely fits. Root, mid and end differences often need a custom blend. A softening cut after installation hides the line.</p>
<h2>Home care</h2>
<p>Brush gently at bonds, avoid tight updos on wet hair and do not pile conditioner at the roots. A loose braid at night can reduce tangling. Maintenance and removal dates follow the method used.</p>
<h2>Consultation before installation</h2>
<p>At our only Bornova address — Gediz Caddesi 21 A, Kazımdirik — we weigh length, density and colour goals together. <a href=""/en/appointment"">Request an extension consultation</a> or send a recent photo on 0534 794 43 93.</p>",
            "sac-kaynagi"),

        Post(
            "Kahkül Kesimi: Yüz Şekli, Saç Yapısı ve Bakım",
            "Perde kahkül, düz kahkül veya yana düşen bir form: doğru seçim yüz şekli kadar saçın çıkış yönüne ve günlük şekillendirme alışkanlığına bağlıdır.",
            @"<p><strong>Kahkül</strong>, küçük bir kesim alanıyla yüzün bütün oranını değiştirebilir. Bu nedenle yalnızca beğenilen fotoğrafa değil; alın genişliğine, saçın çıkış yönüne, yoğunluğuna ve doğal formuna göre seçilmelidir.</p>
<h2>Perde kahkül kimlere uygundur?</h2>
<p>Ortadan veya hafif yandan ayrılan perde kahkül, yüz çevresine yumuşak bir çerçeve verir. Uzadıkça saçın diğer katlarına daha kolay karıştığı için ilk kez kahkül deneyecek kişiler açısından daha esnek bir seçenektir.</p>
<h2>Düz ve kısa kahkül ne kadar bakım ister?</h2>
<p>Kaş hizasında veya daha kısa kesilen kahkülün çizgisi daha belirgindir. Hızlı uzadığı için düzenli düzeltme gerekir. Sabah oluşan yön ayrılıklarını kontrol etmek için kısa bir fön veya şekillendirme gerekebilir.</p>
<h2>Kıvırcık saça kahkül kesilir mi?</h2>
<p>Evet; fakat saçın kuru halde ne kadar kısaldığı hesaba katılmalıdır. Kıvırcık kahkül ıslakken fazla kısaltılırsa kuruduğunda beklenenden yukarı çıkabilir. Kesim, doğal kıvrım yönü izlenerek kontrollü yapılmalıdır.</p>
<h2>Evde kahkül kesmek neden riskli?</h2>
<p>Saçı fazla gererek veya tek çizgide kesmek, kuruduğunda dengesiz bir sonuç oluşturabilir. Özellikle tepe ayrımı yanlış alınırsa kahkül gereğinden kalınlaşır ve geri uzaması zaman alır.</p>
<h2>Yüzünüze uygun formu birlikte seçin</h2>
<p>Kazımdirik’teki salonumuzda kahkülü mevcut kesimden ayrı düşünmüyoruz; yüz çevresi katlarıyla birlikte planlıyoruz. <a href=""/randevu"">Kahkül veya kadın saç kesimi randevusu alın</a>.</p>
<p>Celil &amp; Atilla · Gediz Caddesi 21 A, Bornova · 0534 794 43 93.</p>",
            "/images/services/kahkul.jpg",
            DateTime.UtcNow.AddDays(-7),
            "bangs-cut-face-shape-care",
            "Bangs: Face Shape, Hair Type and Upkeep",
            "Curtain, blunt or side-swept — the right bangs depend on face shape, growth pattern and how much styling you will do daily.",
            @"<p><strong>Bangs</strong> can change how your whole face reads from a small section of hair. Choose from forehead width, growth direction, density and natural pattern — not from a photo alone.</p>
<h2>Who suits curtain bangs?</h2>
<p>Curtain bangs parted centre or slightly off-centre frame the face softly. They grow into the rest of the cut more easily — a forgiving first bang.</p>
<h2>How much upkeep do blunt bangs need?</h2>
<p>Brow-length or shorter bangs show a sharper line and grow quickly. Expect regular trims and occasional blow-dry to tame morning separation.</p>
<h2>Bangs on curly hair?</h2>
<p>Yes — but shrinkage when dry must be allowed for. Cutting curly bangs too short wet can spring up higher than expected. Follow the natural curl direction.</p>
<h2>Why DIY bangs go wrong</h2>
<p>Pulling hair tight or cutting one flat line often dries unevenly. A wrong top section can leave bangs too thick and slow to grow out.</p>
<h2>Find a shape that fits your face</h2>
<p>At our Kazımdirik salon we plan bangs with face-framing layers, not in isolation. <a href=""/en/appointment"">Book bangs or a women's cut</a>.</p>
<p>Celil &amp; Atilla · Gediz Caddesi 21 A, Bornova · 0534 794 43 93.</p>",
            "kahkul,kadin-kesim"),

        Post(
            "Perma, Nanoplasti ve Brezilya Fönü Arasındaki Farklar",
            "Biri dalga oluşturur, diğerleri kabarma ve düz görünüm üzerinde çalışır. Perma, nanoplasti ve Brezilya fönünü hedeflenen forma göre karşılaştırın.",
            @"<p><strong>Perma, nanoplasti ve Brezilya fönü aynı işlem değildir.</strong> Perma saça dalga veya kıvrım kazandırmayı, nanoplasti ve Brezilya fönü ise kabarmayı azaltıp daha düzgün bir form oluşturmayı hedefler. Doğru seçim saçın mevcut yapısına ve beklenen sonuca göre yapılır.</p>
<h2>Perma ne yapar?</h2>
<p>Perma, düz veya dalgası zayıf saça kalıcıya yakın kıvrım formu kazandıran kimyasal bir işlemdir. Buklenin çapı kullanılan sarım tekniğine göre değişir. Boyalı ve hassas saçta dayanıklılık kontrolü yapılmadan uygulanmamalıdır.</p>
<h2>Nanoplasti ne için tercih edilir?</h2>
<p>Nanoplasti, kabaran ve kontrolü zor saçta daha düzgün bir görünüm hedefleyen form işlemidir. Sonuç saçın kalınlığına, doğal kıvrımına ve geçmiş işlemlerine göre değişir; her saçta aynı düzlük seviyesi beklenmez.</p>
<h2>Brezilya fönü ile keratin aynı mı?</h2>
<p>İsimler piyasada sıkça birbirinin yerine kullanılır. Ürün içeriği ve uygulama protokolü sonucu belirler. Bu nedenle yalnızca işlem adına değil, saçın ihtiyacına ve uygulanacak ürünün amacına bakmak gerekir.</p>
<h2>Hangi işlem daha uzun sürer?</h2>
<p>Süre; saçın uzunluğu, yoğunluğu ve seçilen tekniğe bağlıdır. Form işlemleri çoğunlukla birkaç saat sürer. İşlem sonrası ilk yıkama zamanı ve ev bakım önerileri kullanılan ürüne göre değişebilir.</p>
<h2>İşlem öncesi neden test yapılır?</h2>
<p>Açılmış, elastikiyetini kaybetmiş veya sık ısı görmüş saçta küçük bir tutam testi güvenli sınırı gösterir. Saç işlemi taşımıyorsa önce bakım planı yapmak daha doğrudur.</p>
<h2>Saçınızın hedefini konuşalım</h2>
<p>Celil &amp; Atilla’nın Kazımdirik, Gediz Caddesi 21 A’daki salonunda mevcut form ve işlem geçmişi değerlendirilir; uygun seçenek bundan sonra belirlenir. <a href=""/randevu"">Form işlemleri için görüşme randevusu oluşturun</a> veya 0534 794 43 93’ü arayın.</p>",
            "/images/services/form.jpg",
            DateTime.UtcNow.AddDays(-9),
            "perm-nanoplastics-brazilian-blowout",
            "Perm, Nanoplastics and Brazilian Blowout Compared",
            "One creates curl; the others work on frizz and smoothness. Compare perm, nanoplastics and Brazilian blowout by the shape you want.",
            @"<p><strong>Perm, nanoplastics and Brazilian blowout are not the same service.</strong> A perm adds wave or curl; nanoplastics and Brazilian-style smoothing target frizz and a straighter silhouette. The right pick follows your hair structure and the result you want.</p>
<h2>What does a perm do?</h2>
<p>A perm is a chemical service that sets curl or wave on straight or weakly wavy hair. Curl size follows rod technique. Coloured or fragile hair should pass an elasticity check first.</p>
<h2>When is nanoplastics chosen?</h2>
<p>Nanoplastics aims for a smoother look on frizzy, hard-to-control hair. Results vary with thickness, natural curl and past chemical work — not every head ends equally straight.</p>
<h2>Is Brazilian blowout the same as keratin?</h2>
<p>Names are swapped in marketing. Product and protocol decide the outcome. Look at hair need and product purpose, not the label alone.</p>
<h2>How long do these take?</h2>
<p>Timing follows length, density and technique. Shape services often run several hours. First-wash timing and home care depend on the product used.</p>
<h2>Why strand tests matter</h2>
<p>On over-lightened or heat-fatigued hair, a small test shows safe limits. If hair cannot carry the service, repair comes first.</p>
<h2>Let's talk about your goal</h2>
<p>At Celil &amp; Atilla on Gediz Caddesi 21 A, Kazımdirik, we review current shape and history before recommending a path. <a href=""/en/appointment"">Book a smoothing or perm consultation</a> or call 0534 794 43 93.</p>",
            "perma,form,bakim")
    }.Select(BlogEditorialContent.UpdateFringe)
        .Concat(BlogEditorialContent.Articles().Select(a => a.Post)).ToList();

    static BlogPost Post(
        string title,
        string summary,
        string content,
        string headerImageUrl,
        DateTime publishedAt,
        string slugEn,
        string titleEn,
        string summaryEn,
        string contentEn,
        string tags) =>
        new()
        {
            Title = title,
            Summary = summary,
            Content = content,
            HeaderImageUrl = headerImageUrl,
            PublishedAt = publishedAt,
            UpdatedAt = publishedAt,
            Slug = SlugHelper.FromTitle(title),
            SlugEn = slugEn,
            TitleEn = titleEn,
            SummaryEn = summaryEn,
            ContentEn = contentEn,
            Tags = tags,
            Author = "Celil & Atilla",
            IsPublished = true
        };
}
