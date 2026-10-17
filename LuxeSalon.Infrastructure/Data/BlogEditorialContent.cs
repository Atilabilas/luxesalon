using LuxeSalon.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Infrastructure.Data;

// Editorial copy supplied on 2026-10-18. Keep the lead separate from search descriptions.
public static class BlogEditorialContent
{
    public const string FringeSlug = "kahkul-kesimi-yuz-sekli-sac-yapisi-ve-bakim";
    public const string FringeSeoTitle = "Kahkül Kesimi Rehberi: Hangi Kahkül Yüzüne Yakışır?";
    public const string FringeMetaDescription = "Kahkül (kakül) kesimi yaptırmadan önce okuyun: düz, perde ve yan kahkülün hangi yüze yakıştığını, evde nasıl kesilir sorusunu ve bakım ipuçlarını anlattık.";
    public const string RevisionKey = "Content_Blog_20261018";

    public sealed record Article(BlogPost Post, string SeoTitle, string MetaDescription,
        string ImageAlt, (string Question, string Answer)[] Faq);

    public static Article? Find(string slug) => Articles().FirstOrDefault(a => a.Post.Slug == slug);

    public static string? ImageAlt(string imageUrl) =>
        Articles().FirstOrDefault(a => a.Post.HeaderImageUrl == imageUrl)?.ImageAlt;

    internal static Article[] Articles() => new Article[]
    {
        new(
            new BlogPost
            {
                Slug = "artisan-balayage-nedir-klasik-balyajdan-farki",
                Title = "Artisan Balayage Nedir? Klasik Balyajdan Farkı",
                Summary = "Artisan balayage, açık tonların saça fırçayla, kişinin kesimine ve saçın düşüşüne göre yerleştirildiği, dipte yumuşak bir geçiş hedefleyen bir balyaj yaklaşımıdır. Klasik balyajdan farkı çoğu zaman tekniğin adından çok, yerleşimin ne kadar kişiye göre planlandığındadır.",
                Content = """
                <p>“Artisan balayage nedir, normal balyajdan farkı ne?” Bu ifadeyle referans fotoğraflarda giderek daha sık karşılaşılıyor; kimi zaman bir paylaşımın altında, kimi zaman bir rengin adı gibi. Oysa bu isim, sektörde herkesin aynı şeyi kastettiği standart bir teknik tanımı değil. Bu yüzden önce ne anlatıldığını, sonra sizin saçınızda neyin gerçekçi olduğunu konuşmayı tercih ediyoruz.</p>
                <h2>Artisan balayage tam olarak ne demek?</h2>
                <p>“Artisan” zanaatkâr anlamına gelir. Buradaki vurgu, rengin kalıp bir düzenle değil, saçın kesimine, hareketine ve yüz hattına bakılarak elle yerleştirilmesidir. Açık tonlar fırçayla serbest biçimde uygulanır; hangi tutamın ne kadar açılacağına saçın nasıl düştüğüne bakılarak karar verilir. Amaç, dipten uca sert bir çizgi değil, saç uzadıkça bile göze batmayan yumuşak bir geçiştir. Randevu menümüzde Artisan Balayage ayrı bir işlem olarak yer alıyor; ne kastettiğimizi ve sizin saçınızda nasıl uygulanacağını ön görüşmede birlikte netleştiriyoruz.</p>
                <h2>Klasik balyajdan farkı ne?</h2>
                <p>Klasik balyaj da açık tonları saça serbest biçimde yerleştirir; bu yönüyle röfledeki düzenli folyo dağılımından ayrılır. Bu ikisinin farkını <a href="/blog/rofle-nedir-balyajdan-farki-ve-bakim-ipuclari">röfle ve balyaj yazımızda</a> ayrıntılı anlattık. Artisan yaklaşımda fark daha çok ayrıntıdadır: yüz çevresine düşen ışığın yoğunluğu, ense ile tepe arasındaki denge, saçı dalgalı ya da düz kullandığınızda rengin nasıl görüneceği tek tek düşünülür. Klasik balyajda daha genel bir aydınlanma hedeflenebilirken, artisan yerleşimde her bölgenin ayrı bir görevi vardır. Sonuçta ikisi de balyajdır; aradaki fark keskin bir çizgi değil, bir yaklaşım farkıdır. Balyajın ombre ve sombreden nasıl ayrıldığını merak ediyorsanız <a href="/blog/balyaj-ombre-ve-sombre-arasindaki-farklar">balyaj, ombre ve sombre karşılaştırmamıza</a> göz atabilirsiniz.</p>
                <h2>Kimlere uygun, kimlere değil?</h2>
                <p>Dibinin doğal kalmasını isteyen, sık rötuşa gelmek istemeyen ve saçı uzadıkça rengin sert bir çizgiyle bölünmesinden hoşlanmayanlar için güçlü bir seçenektir. Dalgalı saçta ya da saçını sık sık dalgalı şekillendirenlerde elle yerleştirilen ışık daha doğal okunur. Buna karşılık tüm saçın belirgin biçimde açılmasını, net ve düzenli bir ışıltıyı ya da tek seansta çok açık bir sonucu isteyenler için başka bir teknik daha uygun olabilir. Koyu boya geçmişi, ev boyası veya kına varsa açılabilecek seviye sınırlanabilir. Bunu ilk görüşmede açıkça söylemek, ikimizin de gerçekçi bir hedef koymasını sağlar.</p>
                <h2>Fotoğraftaki renk benim saçımda nasıl görünür?</h2>
                <p>Getirdiğiniz referans fotoğraf çok değerlidir ama tek başına yeterli değildir. Sosyal medyadaki birçok balyaj görseli gün ışığında, şekillendirilmiş ve bazen filtrelenmiş saçta çekilir. Aynı renk, sizin doğal tonunuzda, saç kalınlığınızda ve günlük şekillendirme alışkanlığınızda farklı durur. Bu yüzden farklı ışıklarda çekilmiş birkaç örnek getirmenizi, sevmediğiniz örnekleri de göstermenizi öneriyoruz; neyi istemediğinizi bilmek çoğu zaman neyi istediğinizi bilmek kadar yol göstericidir. Saçın mevcut durumu fotoğraftaki açıklığa tek seansta izin vermiyorsa bunu baştan söyleriz; gerekirse hedefe aşamalı ilerlemek saç için daha sağlıklı bir yoldur.</p>
                <h2>Artisan balayage sonrası bakım nasıl olmalı?</h2>
                <p>Açılan tutamlar doğal saça göre nemi daha çabuk kaybeder. Renk koruyucu bir şampuan, düzenli nem desteği ve kontrollü ısı kullanımı tonun daha uzun süre canlı kalmasına yardımcı olur. Sarıya kaçan tonlarda mor şampuan işe yarar ama her yıkamada kullanılmamalıdır; fazlası saçı mat ve kuru gösterebilir. Dip doğal bırakıldığı için kök rötuşu baskısı daha azdır, yine de zamanla tonlama ya da ışıkların tazelenmesi gerekebilir. Bunun zamanını takvimden çok saçınızın nasıl solduğu belirler.</p>
                <h2>Sık sorulanlar</h2>
                <h3>Artisan balayage tek seansta yapılır mı?</h3>
                <p>Saçın mevcut rengi ve geçmişi uygunsa çoğu zaman tek seansta planlanır. Koyu boya ya da renk birikimi varsa hedefe ikinci seansla ulaşmak gerekebilir; bunu işlemden önce konuşuruz.</p>
                <h3>Boyalı saça artisan balayage yapılabilir mi?</h3>
                <p>Yapılabilir, ancak önceki boyanın tonu ve kaç kez uygulandığı sonucu doğrudan etkiler. Son işlemlerinizi ve yaklaşık tarihlerini paylaşmanız bu yüzden önemlidir.</p>
                <h3>Ne kadar sürer?</h3>
                <p>Balyaj işlemleri saç uzunluğu ve boya geçmişine göre çoğunlukla 2–3 saat sürer; düzeltme gereken saçlarda bu süre uzayabilir. Artisan Balayage için ayırmanız gereken süreyi randevu alırken sorun.</p>
                <h3>Ücret nasıl belirlenir?</h3>
                <p>Ücret saçın uzunluğuna, yoğunluğuna ve gereken aşamalara göre netleşir. Güncel saç fotoğrafınızla randevu öncesinde bize sorabilirsiniz.</p>
                <h2>Bornova’da artisan balayage randevusu</h2>
                <p>Celil &amp; Atilla’nın Kazımdirik Mahallesi, Gediz Caddesi 21 A’daki salonunda Artisan Balayage, saçınızın mevcut rengi, geçmişi ve kesimine bakılarak planlanır. <a href="/randevu?serviceId=4">Artisan Balayage için randevu talebi</a> oluşturabilir ya da güncel saç fotoğrafınızı ve beğendiğiniz örnekleri 0534 794 43 93 üzerinden WhatsApp’tan paylaşabilirsiniz. Diğer balyaj seçeneklerini <a href="/hizmetler/balyaj-ombre-sombre">balyaj, ombre ve sombre sayfamızda</a> bulabilirsiniz. <a href="/izmir-ilceleri">İzmir’in başka bir ilçesinden</a> geliyorsanız ararken belirtin; saati yol sürenize göre ayarlayalım.</p>
                """,
                HeaderImageUrl = "/images/blog/artisan-balayage.jpg",
                PublishedAt = new DateTime(2026, 10, 21),
                UpdatedAt = new DateTime(2026, 10, 21),
                Author = "Celil",
                Tags = "balyaj,ombre,sombre",
                IsPublished = true
            },
            "Artisan Balayage Nedir? Balyajdan Farkı | Celil & Atilla",
            "Artisan balayage nedir, klasik balyajdan nasıl ayrılır? Elle yerleştirilen ışık kimlere uyar, fotoğraftaki renk saçınızda nasıl durur, anlatıyoruz.",
            "Elle yerleştirilmiş yumuşak açık tonlarla dalgalı uzun saç; temsili görsel",
            new (string Question, string Answer)[]
            {
                ("Artisan balayage tek seansta yapılır mı?", "Saçın mevcut rengi ve geçmişi uygunsa çoğu zaman tek seansta planlanır. Koyu boya ya da renk birikimi varsa hedefe ikinci seansla ulaşmak gerekebilir; bunu işlemden önce konuşuruz."),
                ("Boyalı saça artisan balayage yapılabilir mi?", "Yapılabilir, ancak önceki boyanın tonu ve kaç kez uygulandığı sonucu doğrudan etkiler. Son işlemlerinizi ve yaklaşık tarihlerini paylaşmanız bu yüzden önemlidir."),
                ("Ne kadar sürer?", "Balyaj işlemleri saç uzunluğu ve boya geçmişine göre çoğunlukla 2–3 saat sürer; düzeltme gereken saçlarda bu süre uzayabilir. Artisan Balayage için ayırmanız gereken süreyi randevu alırken sorun."),
                ("Ücret nasıl belirlenir?", "Ücret saçın uzunluğuna, yoğunluğuna ve gereken aşamalara göre netleşir. Güncel saç fotoğrafınızla randevu öncesinde bize sorabilirsiniz."),
            }),
        new(
            new BlogPost
            {
                Slug = "sac-rengi-duzeltme-gercekci-beklenti",
                Title = "Saç Rengi Düzeltme: Neler Mümkün, Ne Beklemeli?",
                Summary = "Saç rengi düzeltme, istenmeyen bir tonu, lekeli bir boyayı ya da beklenenden koyu veya açık çıkan bir rengi toparlamak için yapılan planlı bir renk çalışmasıdır. Ne kadar düzelebileceğini saçın geçmişi ve taşıma gücü belirler; bazen ikinci seans gerekir.",
                Content = """
                <p>Renk düzeltme randevuları çoğu zaman biraz endişeyle başlar. Evde denenen bir boya beklenenden koyu çıkmıştır, açıcı sonrası saç turuncuya dönmüştür ya da diplerle uçlar arasında birbirini tutmayan bantlar oluşmuştur. Bu durumda ilk istek hızlı bir çözüm olur. Oysa en doğru başlangıç, saçın şu an neyi taşıyabileceğini dürüstçe değerlendirmektir.</p>
                <h2>Saç rengi düzeltme ne zaman gerekir?</h2>
                <p>Renk düzeltme, hedeflenen tona ulaşmadan önce saçın mevcut renginin dengelenmesi gerektiğinde gündeme gelir. Sık görülen durumlar şunlardır: evde uygulanan koyu boyanın istenenden ağır durması, açıcı sonrası sarı ya da turuncu kalan tonlar, farklı zamanlarda yapılan boyalarla oluşan lekeli görünüm ve dip ile uçlar arasındaki belirgin renk farkı. Bazen sorun yalnızca tondadır ve doğru bir tonlama yeterli olur. Bazen de uzunluklarda biriken renk yükü ele alınmadan istenen sonuca gitmek mümkün değildir.</p>
                <h2>Boya boyayı açar mı?</h2>
                <p>Renk düzeltmede en çok sorulan sorulardan biri bu ve cevabı genellikle hayır. Kalıcı boya, daha önce boyanmış ve koyulaşmış saçı tek başına açmaz; üstüne açık bir boya sürmek çoğu zaman dipte açık, uçlarda koyu kalan bantlar oluşturur. Koyu bir rengi açmak için önce renk yükünün kontrollü biçimde azaltılması gerekir ve bu aşama saçı yorabilir. Tersi daha kolaydır: açık bir rengi koyulaştırmak genellikle daha az aşamayla mümkündür. Ancak çok açılmış saçta koyu ton beklenmedik renkler verebilir; bu yüzden önce saça eksik pigmentin kazandırılması gerekebilir. “Bir sonraki boyada düzeltiriz” demek yerine planlı bir sıra izlemek bu yüzden önemlidir.</p>
                <h2>Tek seansta düzelir mi?</h2>
                <p>Bazen evet, çoğu zaman tamamen değil. Hafif bir ton kayması ya da solmuş bir renk tek randevuda toparlanabilir. Ancak yıllar içinde üst üste binmiş koyu boya, kına ya da sık açıcı uygulanmış saçta hedefe tek seansta zorlayarak gitmek saçın esnekliğini kaybetmesine yol açabilir. Hizmet menümüzde de yazdığımız gibi: bazen ikinci seans gerekir ve vaadi abartmayız. İlk seansın amacı çoğu zaman saçı hedefe yaklaştırmak ve sağlığını korumaktır; iki seans arasında saçın toparlanmasına zaman tanınır.</p>
                <h2>Renk düzeltmeden önce neleri söylemelisiniz?</h2>
                <p>Düzeltme planının temeli saçınızın renk geçmişidir. Son bir iki yılda uygulanan boyaları, bunların evde mi salonda mı yapıldığını, açıcı ya da röfle olup olmadığını, kına veya bitkisel boya kullanıp kullanmadığınızı mümkün olduğunca net anlatın. Hatırlamadığınız bir şey varsa bunu da söyleyin; tahmin etmek yerine “bilmiyorum” demek daha yararlıdır. Saçınızın gün ışığında çekilmiş güncel bir fotoğrafı ön değerlendirmeyi kolaylaştırır. Kök rötuşu düzeninizi de belirtin; dip ile uç arasındaki farkın nereden geldiğini <a href="/blog/kok-rotusu-ne-zaman-yapilmali">kök rötuşu yazımızda</a> anlattık. Gerekirse işleme başlamadan önce küçük bir tutamda test yapılarak saçın güvenli sınırı görülebilir.</p>
                <h2>Renk düzeltme sonrası saçınızı nasıl korursunuz?</h2>
                <p>Düzeltme sonrasında saç birkaç kimyasal aşamadan geçmiş olabilir ve daha nazik bir bakım ister. Renk koruyucu şampuan, düzenli nem desteği ve düşük ısı hem tonun hem de saçın dayanıklılığının korunmasına yardımcı olur. İkinci seans planlandıysa arada evde boya, açıcı ya da sert bir form işlemi denememek çok önemlidir. Açık tonlara geçtiyseniz mor şampuanı yalnızca ihtiyaç halinde kullanın; açık tutamların bakımına dair ayrıntıları <a href="/blog/rofle-nedir-balyajdan-farki-ve-bakim-ipuclari">röfle yazımızda</a> bulabilirsiniz.</p>
                <h2>Sık sorulanlar</h2>
                <h3>Ev boyası lekelerini düzeltebilir misiniz?</h3>
                <p>Çoğu durumda görünümü iyileştirmek mümkündür; ne kadar düzeleceği boyanın kaç kat uygulandığına ve saçın durumuna bağlıdır. Fotoğrafla ön görüşme yapmak en doğru başlangıçtır.</p>
                <h3>Kınalı saçta renk düzeltme yapılır mı?</h3>
                <p>Kına, salon boyası ve açıcıyla öngörülmesi zor tepkiler verebilir. Kına kullandıysanız mutlaka belirtin; işleme başlamadan önce küçük bir tutamda test yapmak gerekebilir.</p>
                <h3>Pastel tonlara renk düzeltmeyle aynı randevuda geçilir mi?</h3>
                <p>Pastel bir ton için saçın oldukça açık ve dengeli olması gerekir. Düzeltme gereken saçta bu hedef ikinci aşamaya kalabilir; sıralamayı ön görüşmede birlikte belirleriz.</p>
                <h3>Ücret nasıl belirlenir?</h3>
                <p>Saçın uzunluğuna, yoğunluğuna ve gereken aşama sayısına göre netleşir. Olası ek seansın nasıl ele alınacağını randevu öncesinde sormanızı öneririz.</p>
                <h2>Bornova’da saç rengi düzeltme randevusu</h2>
                <p>Kazımdirik Mahallesi, Gediz Caddesi 21 A’daki salonumuzda renk düzeltme, saçın mevcut durumu ve geçmişi değerlendirildikten sonra planlanır. <a href="/randevu?serviceId=17">Pastel ve renk düzeltme için randevu talebi</a> oluşturabilir veya saçınızın gün ışığında çekilmiş fotoğrafını 0534 794 43 93 üzerinden paylaşabilirsiniz. Dip dengesi ve ışıltı ihtiyaçlarınız için <a href="/hizmetler/rofle-kok-rotusu">röfle ve kök rötuşu sayfamıza</a> da bakabilirsiniz. <a href="/izmir-ilceleri">İzmir’in farklı ilçelerinden</a> geliyorsanız ararken söyleyin, saati yol sürenize göre ayarlayalım.</p>
                """,
                HeaderImageUrl = "/images/blog/sac-rengi-duzeltme.jpg",
                PublishedAt = new DateTime(2026, 10, 28),
                UpdatedAt = new DateTime(2026, 10, 28),
                Author = "Celil",
                Tags = "rofle,kok-rotusu,renklendirme",
                IsPublished = true
            },
            "Saç Rengi Düzeltme: Ne Beklemeli? | Celil & Atilla",
            "Evde boya koyu mu çıktı, saç turuncuya mı döndü? Saç rengi düzeltmede neyin tek seansta mümkün olduğunu, neyin zaman istediğini dürüstçe anlatıyoruz.",
            "Kuaför koltuğunda renk düzeltme öncesi saçı incelenen kadın; temsili görsel",
            new (string Question, string Answer)[]
            {
                ("Ev boyası lekelerini düzeltebilir misiniz?", "Çoğu durumda görünümü iyileştirmek mümkündür; ne kadar düzeleceği boyanın kaç kat uygulandığına ve saçın durumuna bağlıdır. Fotoğrafla ön görüşme yapmak en doğru başlangıçtır."),
                ("Kınalı saçta renk düzeltme yapılır mı?", "Kına, salon boyası ve açıcıyla öngörülmesi zor tepkiler verebilir. Kına kullandıysanız mutlaka belirtin; işleme başlamadan önce küçük bir tutamda test yapmak gerekebilir."),
                ("Pastel tonlara renk düzeltmeyle aynı randevuda geçilir mi?", "Pastel bir ton için saçın oldukça açık ve dengeli olması gerekir. Düzeltme gereken saçta bu hedef ikinci aşamaya kalabilir; sıralamayı ön görüşmede birlikte belirleriz."),
                ("Ücret nasıl belirlenir?", "Saçın uzunluğuna, yoğunluğuna ve gereken aşama sayısına göre netleşir. Olası ek seansın nasıl ele alınacağını randevu öncesinde sormanızı öneririz."),
            }),
        new(
            new BlogPost
            {
                Slug = "saca-protein-bakimi-mi-nem-bakimi-mi",
                Title = "Saça Protein Bakımı mı, Nem Bakımı mı Gerekir?",
                Summary = "Saça protein mi nem mi gerektiği, saçın nasıl davrandığına bakılarak anlaşılır: kuru, mat ve kolay dolaşan saç çoğunlukla neme, ıslakken aşırı esneyen ve formunu tutmayan saç ise proteine ihtiyaç duyar. Saç derisindeki sorunlar ise ayrı bir konudur ve ayrı değerlendirilir.",
                Content = """
                <p>Bakım reyonunda neredeyse her ürün “onarıcı” olduğunu söylüyor; bu da saçınıza gerçekte neyin iyi geleceğini kestirmeyi zorlaştırıyor. Salonda bakım seçerken ürünün adından değil, saçın kendisinden yola çıkıyoruz. Hizmet menümüzde bunu kısaca şöyle yazdık: kuru saçta nem, yorulmuş saçta protein, kepek veya yağlı dipte saç derisi bakımı; hangisinin gerektiği saç görülünce seçilir.</p>
                <h2>Saçımın proteine mi neme mi ihtiyacı var, nasıl anlarım?</h2>
                <p>Neme ihtiyaç duyan saç genellikle kuru hissettirir, ışığı az yansıtır, kolay dolaşır ve kabarır; uçlar sert ve pürüzlü olabilir. Proteine ihtiyaç duyan saç ise farklı davranır: ıslakken fazla yumuşar, lastik gibi esner, kuruduğunda formunu tutmaz ve fön ya da maşa sonrası çabuk düşer. Bu tablo özellikle açıcı, sık boya ve yüksek ısı görmüş saçlarda belirginleşir. Evde basit bir gözlem yapabilirsiniz: ıslak bir telin iki ucundan tutup nazikçe çekin. Hiç esnemeden hemen kopuyorsa saç büyük olasılıkla neme, çok esneyip eski haline dönmüyorsa proteine ihtiyaç duyuyordur. Bu yalnızca bir fikir verir; saçın dip, boy ve uç bölümleri farklı ihtiyaçlar taşıyabilir.</p>
                <h2>Protein bakımı fazla yapılırsa ne olur?</h2>
                <p>Protein yorulmuş saça yapı desteği verir ama fazlası da sorun yaratır. Gereğinden sık protein uygulanan saç sertleşir, saman gibi bir his verir ve kırılmaya daha açık hale gelebilir. Nemde de denge önemlidir: yapısı zayıfken yalnızca nem verilen saç daha da yumuşayıp formunu kaybedebilir. Bu yüzden “hangisi daha iyi” sorusunun tek bir cevabı yoktur; doğru soru “saçım şu an neye ihtiyaç duyuyor” olmalıdır. Çoğu saçta ikisinin dönemsel olarak dengelenmesi gerekir ve boya ya da açıcı sonrasında bu ihtiyaç değişebilir.</p>
                <h2>Saç derisi bakımı ne zaman gündeme gelir?</h2>
                <p>Bakım çoğu zaman boy ve uçlara odaklanır ama dipte yaşanan sorunlar ayrı bir değerlendirme ister. Kepeklenme ya da çabuk yağlanan dipler, uçlara uygulanan bakımla düzelmez; menümüzde bu ihtiyaçlar için saç derisi bakımı ayrı bir seçenek olarak yer alır. Burada önemli bir sınır var: saç derisi bakımı kozmetik bir uygulamadır. Geçmeyen kaşıntı, kızarıklık, yara, yoğun pullanma ya da belirgin dökülme varsa bunun nedenini bir dermatoloğun değerlendirmesi gerekir. Böyle bir durum fark ettiğinizde bakım randevusundan önce hekiminize danışmanızı öneririz.</p>
                <h2>Keratin ya da saç botoksu bu bakımların yerine geçer mi?</h2>
                <p>Hayır, farklı ihtiyaçlara yönelirler. Keratin bakımı kabarma ve form üzerinde çalışır; saç botoksu ise yıpranmış görünen boy ve uçlara dolgunluk ve parlaklık kazandırmayı hedefler. İkisinin farkını <a href="/blog/keratin-bakimi-mi-sac-botoksu-mu">keratin mi saç botoksu mu yazımızda</a> anlattık. Protein ve nem bakımı ise saçın temel dengesini hedefler; çoğu zaman bu işlemlere hazırlık ya da destek olarak da düşünülebilir. Nanoplasti gibi bir form işlemi aklınızdaysa, saçın bunu taşıyıp taşımayacağını görmek için önce bir bakım dönemi gerekebilir. Bu konuya <a href="/blog/perma-nanoplasti-ve-brezilya-fonu-arasindaki-farklar">perma, nanoplasti ve Brezilya fönü yazımızda</a> değindik.</p>
                <h2>Evde bakımı nasıl planlamalısınız?</h2>
                <p>Salon bakımı bir başlangıçtır; sonucu evdeki alışkanlıklar korur. Kuru saçta nem içerikli maske ve durulanmayan ürünler, yorgun saçta zaman zaman protein içeren bir bakım işe yarar. Etiketlerde keratin ya da hidrolize protein gibi içerikleri fark etmeye alışmak, farkında olmadan aşırı protein kullanmanızı önler. Isıyı düşük tutmak, ıslak saçı sert fırçalamamak ve uçları düzenli aldırmak hangi bakımı seçerseniz seçin sonucu uzatır. İhtiyacınız mevsime, boya takviminize ve şekillendirme alışkanlığınıza göre değişir; bir dönem işe yarayan rutin sonraki dönemde değiştirilmeyi gerektirebilir.</p>
                <h2>Sık sorulanlar</h2>
                <h3>Protein bakımı ne sıklıkla yapılmalı?</h3>
                <p>Sabit bir sıklık yoktur; saçın durumuna, boya ve açıcı geçmişine göre değişir. Saç sertleşmeye başladıysa protein aralığını açmak gerekir.</p>
                <h3>Boyalı saça protein bakımı yapılır mı?</h3>
                <p>Yapılabilir; açıcı ve sık boya gören saç çoğu zaman yapı desteğinden fayda görür. Hangi bakımın ne zaman yapılacağı renk takviminizle birlikte planlanmalıdır.</p>
                <h3>Saç derisi bakımı dökülmeyi durdurur mu?</h3>
                <p>Saç derisi bakımı tıbbi bir tedavi değildir ve dökülmeyi durdurma iddiası taşımaz. Belirgin dökülmede önce bir dermatoloğa danışmanızı öneririz.</p>
                <h2>Bornova’da saç bakımı randevusu</h2>
                <p>Kazımdirik Mahallesi, Gediz Caddesi 21 A’daki salonumuzda bakım seçimi, saçınızın esnekliği, boya geçmişi ve dipteki ihtiyaç birlikte değerlendirilerek yapılır. <a href="/randevu?serviceId=20">Protein, nem ve saç derisi bakımı için randevu talebi</a> oluşturabilir ya da sorularınızı 0534 794 43 93 üzerinden iletebilirsiniz. Kabarma ve parlaklık için seçenekleri <a href="/hizmetler/keratin-sac-botoksu">keratin bakımı ve saç botoksu sayfamızda</a> bulabilirsiniz. <a href="/izmir-ilceleri">İzmir’in başka bir ilçesinden</a> geliyorsanız ararken belirtin, saati yol sürenize göre ayarlayalım.</p>
                """,
                HeaderImageUrl = "/images/blog/protein-nem-bakimi.jpg",
                PublishedAt = new DateTime(2026, 11, 4),
                UpdatedAt = new DateTime(2026, 11, 4),
                Author = "Celil",
                Tags = "bakim,keratin,sac-botoks",
                IsPublished = true
            },
            "Saça Protein mi Nem mi Gerekir? | Celil & Atilla",
            "Saçınız kuru mu, yoksa ıslakken lastik gibi mi esniyor? Protein ve nem ihtiyacını ayırt etmenin yolları ve saç derisi bakımının sınırları.",
            "Arkadan görünen uzun, parlak ve dalgalı saç; temsili görsel",
            new (string Question, string Answer)[]
            {
                ("Protein bakımı ne sıklıkla yapılmalı?", "Sabit bir sıklık yoktur; saçın durumuna, boya ve açıcı geçmişine göre değişir. Saç sertleşmeye başladıysa protein aralığını açmak gerekir."),
                ("Boyalı saça protein bakımı yapılır mı?", "Yapılabilir; açıcı ve sık boya gören saç çoğu zaman yapı desteğinden fayda görür. Hangi bakımın ne zaman yapılacağı renk takviminizle birlikte planlanmalıdır."),
                ("Saç derisi bakımı dökülmeyi durdurur mu?", "Saç derisi bakımı tıbbi bir tedavi değildir ve dökülmeyi durdurma iddiası taşımaz. Belirgin dökülmede önce bir dermatoloğa danışmanızı öneririz."),
            }),
        new(
            new BlogPost
            {
                Slug = "kas-laminasyonu-nedir-kimlere-uygun",
                Title = "Kaş Laminasyonu Nedir, Kimlere Uygun?",
                Summary = "Kaş laminasyonu, kaş kıllarının yönünü yukarı ve düzenli bir hatta yatıran kozmetik bir uygulamadır. Kaşları daha dolgun ve taranmış gösterir ama kıl eklemez ve kaşın çerçevesini tek başına değiştirmez; sonuç mevcut kaşınıza bağlıdır.",
                Content = """
                <p>Taranmış, doğal ama dolgun görünen kaşlar son yıllarda sıkça isteniyor ve kaş laminasyonu bu görünüme ulaşmanın yollarından biri. Ancak her kaşta aynı sonucu vermez ve kalıcı makyaj ya da kaş ekimi gibi bir değişim beklentisiyle yapılmamalıdır. Randevudan önce ne yaptığını ve ne yapmadığını bilmek, sonuçtan memnun kalmanın ilk adımıdır.</p>
                <h2>Kaş laminasyonu nedir, ne işe yarar?</h2>
                <p>Laminasyon, kaş kıllarını yumuşatan bir ürünle başlar; kıllar istenen yönde taranıp sabitlenir, ardından bu yönü koruyan ikinci bir ürün uygulanır. Hizmet menümüzde kısaca yazdığımız gibi, laminasyon kıl yönünü yatırır. Böylece aşağı ya da farklı yönlere uzayan kıllar düzenli bir çizgide durur, kaşlar daha dolgun ve bakımlı görünür. Her sabah kaşını jel ya da sabunla taramaya alışkın olanlar için bu düzeni bir süre korumanın pratik bir yoludur. Laminasyon kıl eklemez; seyrek bölgelerde kılların yönü değiştirilerek alan biraz daha dolu gösterilebilir ama boşluk ortadan kalkmaz.</p>
                <h2>Kimler için uygundur?</h2>
                <p>Kılları yeterince uzun ama düzensiz uzayan, aşağı doğru yatan ya da taramadan kontrol edilemeyen kaşlarda laminasyon belirgin bir fark yaratabilir. Kaşlarını doğal ama dolgun göstermek ve günlük kaş makyajını azaltmak isteyenler için de iyi bir seçenektir. Çok kısa ya da çok seyrek kaşlarda ise yönü değiştirilecek yeterli kıl olmadığı için sonuç sınırlı kalır. Kaş bölgesinde tahriş, yara ya da aktif bir cilt sorunu varsa, daha önce kimyasal bir işleme tepki verdiyseniz işlemi ertelemek ve gerekirse hekiminize danışmak daha doğrudur. Hamileyseniz, emziriyorsanız ya da göz çevresinde yakın zamanda bir işlem yaptırdıysanız bunu da randevu öncesinde belirtin.</p>
                <h2>Laminasyon kaşın şeklini değiştirir mi?</h2>
                <p>Laminasyon kılların yönünü değiştirir, kaşın çerçevesini değil. Kaşın başlangıç, kemer ve bitiş noktası şekillendirme ile belirlenir. Salonumuzda kaş şekillendirme cımbız veya iple yapılır ve sınır yüz hatlarına göre çizilir; randevu menümüzde şekillendirme ve laminasyon aynı başlık altında yer alır. Burada ölçü önemlidir: laminasyon kılları yukarı taşıdığı için kaş hattı biraz daha yüksek ve dolu görünür. Bu nedenle şekillendirmede fazla kıl almak yerine doğal hattı koruyup yalnızca dağınık kılları temizlemek çoğu zaman daha dengeli bir sonuç verir.</p>
                <h2>Etkisi ne kadar sürer?</h2>
                <p>Süre kılların uzama hızına, kaş derisinin yağlanmasına ve işlem sonrası bakıma göre değişir; laminasyon genellikle birkaç hafta boyunca etkisini korur. Kıllar doğal döngüleriyle yenilendikçe yeni çıkan kıllar kendi yönünde uzar ve etki yavaş yavaş azalır. Bu yüzden belirli bir hafta sayısı vaat etmek yerine, ilk uygulamadan sonra kendi kaşınızın nasıl davrandığını gözlemlemenizi öneririz. Ne zaman tekrarlanacağına takvim değil, kılların yeniden ne kadar dağıldığı karar verir. Kaşı çok sık kimyasal işleme sokmamak da kılların sağlığı için önemlidir.</p>
                <h2>İşlem sonrası nelere dikkat etmelisiniz?</h2>
                <p>İlk saatlerde kaşları ıslatmamak, ovuşturmamak ve makyajla kapatmamak, kılların yeni yönüne yerleşmesine yardımcı olur; ne kadar süre kaçınmanız gerektiğini işlem sonrasında net olarak sorun. Sonraki günlerde kaşları her sabah temiz bir fırçayla yukarı taramak düzeni korur. Laminasyon sonrası kıllar biraz kuruyabilir; kaşa uygun hafif bir besleyici ürünle nem desteği yararlı olabilir. Yoğun buhar, sauna ve kaş bölgesine sürülen sert peeling ürünleri etkiyi kısaltabilir. Düğün ya da özel bir gün hazırlığındaysanız laminasyonu son güne bırakmayın; <a href="/blog/gelin-saci-ve-makyajinda-prova-neden-onemli">gelin saçı ve makyajında prova yazımızda</a> anlattığımız gibi, önemli bir gün öncesinde denenmiş bir sonuç her zaman daha rahattır.</p>
                <h2>Sık sorulanlar</h2>
                <h3>Kaş laminasyonu kaş boyası mıdır?</h3>
                <p>Hayır. Laminasyon kılların yönünü değiştirir, rengini değil. Kaş renginizle ilgili bir beklentiniz varsa bunu randevu öncesinde sorun.</p>
                <h3>Kaş laminasyonu kalıcı mı?</h3>
                <p>Hayır, geçicidir. Kıllar yenilendikçe etki azalır ve kaşlar zamanla eski düzenine döner.</p>
                <h3>İşlem acıtır mı?</h3>
                <p>Laminasyonun kendisi genellikle acı veren bir işlem değildir; şekillendirmede cımbız veya iple kıl alınırken hafif bir rahatsızlık hissedilebilir. Hassas bir cildiniz varsa önceden belirtin.</p>
                <h3>Kirpik liftingle aynı gün yapılabilir mi?</h3>
                <p>Menümüzde kirpik lifting de yer alıyor. İki işlemi aynı gün planlamak isteyip istemediğinizi ve toplam süreyi randevu alırken sorun.</p>
                <h2>Bornova’da kaş laminasyonu randevusu</h2>
                <p>Kazımdirik Mahallesi, Gediz Caddesi 21 A’daki salonumuzda kaş şekillendirme ve laminasyon, kaşınızın mevcut yoğunluğu ve yüz hatlarınız birlikte değerlendirilerek planlanır. <a href="/randevu?serviceId=25">Kaş şekillendirme ve laminasyon için randevu talebi</a> oluşturabilir ya da kaşlarınızın gün ışığında çekilmiş yakın bir fotoğrafını 0534 794 43 93 üzerinden paylaşabilirsiniz. Randevu formunu gönderdiğinizde WhatsApp açılır; mesajı iletmeden randevu kesinleşmez. Randevu notuna neleri yazmanın işe yaradığını <a href="/blog/kuafor-randevusundan-once-neleri-belirtmelisiniz">kuaför randevusu öncesi yazımızda</a> bulabilir, diğer bakım seçenekleri için <a href="/hizmetler">hizmetler sayfamıza</a> bakabilirsiniz. <a href="/izmir-ilceleri">İzmir’in başka bir ilçesinden</a> geliyorsanız ararken söyleyin, saati yol sürenize göre ayarlayalım.</p>
                """,
                HeaderImageUrl = "/images/blog/kas-laminasyonu.jpg",
                PublishedAt = new DateTime(2026, 11, 11),
                UpdatedAt = new DateTime(2026, 11, 11),
                Author = "Celil & Atilla",
                Tags = "kas,bakim",
                IsPublished = true
            },
            "Kaş Laminasyonu Nedir, Kimlere Uygun? | Celil & Atilla",
            "Kaş laminasyonu kılları yukarı ve düzenli bir hatta yatırır ama kıl eklemez. Kimlere uygun, ne kadar sürer, sonrasında nelere dikkat edilir?",
            "Yukarı doğru taranmış, düzenli ve dolgun görünen kaşların yakın çekimi; temsili görsel",
            new (string Question, string Answer)[]
            {
                ("Kaş laminasyonu kaş boyası mıdır?", "Hayır. Laminasyon kılların yönünü değiştirir, rengini değil. Kaş renginizle ilgili bir beklentiniz varsa bunu randevu öncesinde sorun."),
                ("Kaş laminasyonu kalıcı mı?", "Hayır, geçicidir. Kıllar yenilendikçe etki azalır ve kaşlar zamanla eski düzenine döner."),
                ("İşlem acıtır mı?", "Laminasyonun kendisi genellikle acı veren bir işlem değildir; şekillendirmede cımbız veya iple kıl alınırken hafif bir rahatsızlık hissedilebilir. Hassas bir cildiniz varsa önceden belirtin."),
                ("Kirpik liftingle aynı gün yapılabilir mi?", "Menümüzde kirpik lifting de yer alıyor. İki işlemi aynı gün planlamak isteyip istemediğinizi ve toplam süreyi randevu alırken sorun."),
            })
    };

    internal static BlogPost UpdateFringe(BlogPost post)
    {
        if (post.Slug != FringeSlug)
            return post;

        post.Title = "Kahkül Kesimi: Yüzüne Göre Düz, Perde ve Yan Kahkül Modelleri";
        if (!post.Content.Contains("<h2>Düz kahkül kesimi kimlere yakışır?</h2>", StringComparison.Ordinal))
        {
            const string curtainHeading = "<h2>Perde kahkül kimlere uygundur?</h2>";
            var curtain = post.Content.IndexOf(curtainHeading, StringComparison.Ordinal);
            var nextSection = curtain < 0 ? -1 : post.Content.IndexOf("<h2>", curtain + curtainHeading.Length, StringComparison.Ordinal);
            if (nextSection < 0)
                throw new InvalidOperationException("Kahkül yazısında güncellenecek perde kahkül bölümü bulunamadı.");
            post.Content = post.Content.Insert(nextSection, """
                <h2>Düz kahkül kesimi kimlere yakışır?</h2>
                <p>Düz kahkül, alnı kapatan ve kaşın hemen üstünde ya da kaş hizasında biten net bir çizgiyle kesilir. Uzun ve oval yüzlerde yüzün dikey hattını kısaltarak dengeli bir görünüm sağlar. Alnı geniş olanlarda da alanı daha orantılı gösterir. Yuvarlak veya kare yüzlerde ise düz kahkül yüzü daha geniş gösterebilir; bu yüz şekillerinde kenarlara doğru hafif uzayan, yumuşak geçişli bir kesim daha iyi sonuç verir. Saç yapısı da belirleyicidir. Düz ve orta kalınlıktaki saçta düz kahkül kolay şekil alır. Çok ince telli saçta gün içinde yağlanıp ayrılabilir, belirgin bir çıkış yönü (girdap) olan saçta ise yukarı kalkabilir. Düz kahkül gözlerin önüne düştüğünde kısa sürede rahatsız edici olur, bu yüzden boyunu korumak için birkaç haftada bir uç düzeltmesi gerekir.</p>
                <img src="/images/blog/duz-kahkul.webp" srcset="/images/blog/duz-kahkul.480.webp 480w, /images/blog/duz-kahkul.800.webp 800w, /images/blog/duz-kahkul.webp 1280w" sizes="(max-width: 767px) 100vw, 720px" alt="Kaş hizasında düz kahkül kesimi, temsili görsel" loading="lazy" decoding="async" width="1280" height="720" />
                <h2>Yan kahkül nasıl şekillendirilir?</h2>
                <p>Yan kahkül, saçın ayrılma çizgisinden bir yana doğru uzayarak yüzü çapraz bir hatla çerçeveler. Hemen her yüz şekline uyum sağlar ve uzamaya bırakıldığında saçın geri kalanına kolayca karışır. Günlük şekillendirmede kahkülü saçın geri kalanından önce, nemliyken kurutmak en önemli adımdır. Fön makinesini yukarıdan aşağı tutup kahkülü yuvarlak bir fırçayla gitmesini istediğiniz yöne doğru çekerek kurutun. Bu, saçın kök kısmının o yöne yerleşmesini sağlar. Kahkülün sürekli öne düşmesini istemiyorsanız köklere az miktarda hacim veren bir ürün kullanabilirsiniz. Gün içinde dağılırsa, saçı yeniden ıslatmadan parmaklarınızla yönünü vermeniz genellikle yeterlidir. Saçınızın çıkış yönü, yan kahkülün hangi tarafa daha kolay yerleşeceğini belirler; kesim sırasında bu yön mutlaka dikkate alınmalıdır.</p>
                <img src="/images/blog/yan-kahkul.webp" srcset="/images/blog/yan-kahkul.480.webp 480w, /images/blog/yan-kahkul.800.webp 800w, /images/blog/yan-kahkul.webp 1280w" sizes="(max-width: 767px) 100vw, 720px" alt="Yana doğru şekillendirilmiş yan kahkül, temsili görsel" loading="lazy" decoding="async" width="1280" height="720" />
                """ + "\n");
        }

        post.Content = post.Content.Replace("<h2>Evde kahkül kesmek neden riskli?</h2>",
            "<h2>Kahkül nasıl kesilir, evde kesmek neden riskli?</h2>", StringComparison.Ordinal);
        const string intro = "Kahkül, saç ıslakken değil, kuru ve günlük taradığınız şekildeyken kesilmelidir. Islak saç kuruduğunda kısalır ve evde kesilen kahkül beklenenden kısa kalabilir.";
        if (!post.Content.Contains(intro, StringComparison.Ordinal))
        {
            var heading = post.Content.IndexOf("<h2>Kahkül nasıl kesilir, evde kesmek neden riskli?</h2>", StringComparison.Ordinal);
            var paragraph = heading < 0 ? -1 : post.Content.IndexOf("<p>", heading, StringComparison.Ordinal);
            if (paragraph < 0)
                throw new InvalidOperationException("Kahkül yazısında güncellenecek evde kesim paragrafı bulunamadı.");
            post.Content = post.Content.Insert(paragraph + 3, intro + " ");
        }
        post.UpdatedAt = new DateTime(2026, 10, 18);
        return post;
    }

    // Apply only this delivery once on existing databases; later CMS edits survive restarts.
    public static void Apply(ApplicationDbContext context)
    {
        if (context.SiteSettings.Any(s => s.Key == RevisionKey))
            return;

        using var transaction = context.Database.BeginTransaction();
        foreach (var article in Articles())
        {
            if (!context.BlogPosts.Any(p => p.Slug == article.Post.Slug))
                context.BlogPosts.Add(article.Post);
        }

        var fringe = context.BlogPosts.SingleOrDefault(p => p.Slug == FringeSlug);
        if (fringe == null)
            throw new InvalidOperationException("Güncellenecek kahkül yazısı bulunamadı.");
        UpdateFringe(fringe);
        context.SiteSettings.Add(new SiteSetting
        {
            Key = RevisionKey,
            Value = "2026-10-18",
            Description = "Dört Türkçe blog yazısı ve kahkül güncellemesi uygulandı.",
            Group = "Content"
        });
        context.SaveChanges();
        transaction.Commit();
    }
}
