const fs = require('node:fs');
const path = require('node:path');
const folder = __dirname;
const imageBase = 'https://celilatilla.com/images/services/';
const allowed = ['kadin-kesim.webp', 'balyaj.webp', 'fon.webp', 'rofle.webp'];
const descriptions = {
  'Kadın saç kesimi': 'Yüz hattı, saç yoğunluğu ve günlük şekillendirme alışkanlığınıza göre planlanan kadın saç kesimi.',
  'Balyaj, ombre ve sombre': 'Doğal ışıltıdan belirgin uçlara: saçınızın geçmişi ve hedeflediğiniz geçişe göre balyaj, ombre veya sombre planlanır.',
  'Keratin bakımı ve saç botoksu': 'Kabarma kontrolü, parlaklık ve yıpranmış uç desteği: keratin ve saç botoksu saç analizi sonrası seçilir.',
  'Röfle Nedir? Balyajdan Farkı ve Bakım İpuçları': 'Röfle daha düzenli ve belirgin tutamlar, balyaj ise daha serbest bir ışık geçişi oluşturur. Saçınıza uygun tekniği seçmeden önce farkları ve bakım ihtiyacını öğrenin.',
  'Kuaför Randevusundan Önce Neleri Belirtmelisiniz?': 'İşlem adı, saçın güncel durumu ve tercih edilen saat: doğru bilgiyi baştan paylaşmak randevu süresinin daha net planlanmasını sağlar.',
  'Balyaj, Ombre ve Sombre Arasındaki Farklar': 'Doğal bir geçiş mi, daha belirgin açık uçlar mı? Balyaj, ombre ve sombre arasındaki farkları saçınızın geçmişi ve istediğiniz sonuç üzerinden anlatıyoruz.'
};
const faq = [
  ['Celil & Atilla Kuaför nerede?', 'Salonumuz Bornova’nın Kazımdirik Mahallesi’nde, Gediz Caddesi 21 A adresindedir. Celil & Atilla’nın başka şubesi yoktur; güncel konumumuz budur.'],
  ['Çalışma saatleri nedir?', 'Pazartesi–Cumartesi 08:30–20:00, Pazar 10:00–19:00. Randevu: 0534 794 43 93 veya WhatsApp.'],
  ['Balyaj, ombre ve sombre farkı nedir?', 'Balyaj ışığı saça serbest biçimde dağıtır, ombre koyu dipten açık uçlara daha belirgin geçer, sombre ise bu geçişin yumuşak yorumudur. Seçim saçın geçmişine ve istediğiniz görünüme göre yapılır.'],
  ['Saç botoksu tıbbi botoks mudur?', 'Hayır. Saç botoksu ve saç dolgusu iğnesiz bakımdır. Yüz botoksu veya tıbbi dolgu yapılmaz.'],
  ['Erkek ve çocuk kabul ediyor musunuz?', 'Evet. Kadın saç tasarımı ana uzmanlığımız; bunun yanında erkek kesimi, fade, sakal, tıraş, erkek boya ve çocuk kesimi için de randevu alınabilir.'],
  ['Randevu nasıl alınır?', '0534 794 43 93’ü arayın, WhatsApp’tan yazın veya sitedeki randevu formunu doldurun. İstediğiniz işlem ile tercih ettiğiniz gün ve saat yeterlidir.']
];
const slugByTitle = {
  'Kadın saç kesimi': 'kadin-sac-kesimi', 'Balyaj, ombre ve sombre': 'balyaj-ombre-sombre',
  'Saç kaynağı': 'sac-kaynagi', 'Gelin saçı': 'gelin-saci', 'Erkek kesim ve fade': 'erkek-kesim-fade',
  'Çocuk saç kesimi': 'cocuk-kesimi', 'Keratin bakımı ve saç botoksu': 'keratin-sac-botoksu',
  'Röfle ve kök rötuşu': 'rofle-kok-rotusu'
};
for (const name of process.argv.slice(2)) {
  const file = path.join(folder, name + '.html');
  let html = fs.readFileSync(file, 'utf8');
  const original = path.join(folder, name + '.stitch-original.html');
  if (!fs.existsSync(original)) fs.writeFileSync(original, html);
  const images = [];
  html = html.replace(/<img\b[^>]*>/gi, tag => {
    const alt = (tag.match(/alt="([^"]*)"/i) || [,''])[1];
    let image;
    if (/röfle|rofle/i.test(alt)) image = 'rofle.webp';
    else if (/balyaj|ombre|sombre/i.test(alt)) image = 'balyaj.webp';
    else if (/keratin|botoks|salon|atmosfer|hikâye|fon|fön/i.test(alt)) image = /salon|atmosfer/i.test(alt) && images.length === 0 ? 'kadin-kesim.webp' : 'fon.webp';
    else if (/kadın|kadin|kesim|hero|açılış/i.test(alt)) image = 'kadin-kesim.webp';
    else throw new Error('Unmapped image alt: ' + alt);
    images.push(image);
    return tag.replace(/src="[^"]*"/i, 'src="' + imageBase + image + '"')
      .replace(/\s+(?:grayscale(?:-\[[^\]]*\])?|contrast-\[[^\]]*\])/g, '')
      .replace(/alt="[^"]*"/i, 'alt="' + alt.replace(/ — temsili görsel$/i, '') + ' — temsili görsel"');
  });
  html = html.replace(/<article\b[^>]*>[\s\S]*?<\/article>/gi, article => {
    const heading = (article.match(/<h3\b[^>]*>([\s\S]*?)<\/h3>/i) || [,''])[1].replace(/<[^>]*>/g, '').trim();
    if (descriptions[heading]) article = article.replace(/(<p\b[^>]*>)[\s\S]*?(<\/p>)/i, '$1' + descriptions[heading] + '$2');
    return article.replace(/<span\b[^>]*>\s*\d+ dk okuma\s*<\/span>/gi, '')
      .replace(/Konsültasyon &amp; Yıkama Dahil|Kişiselleştirilmiş Tonlama|Yoğun Doku Onarımı/g, 'Kesim · Renk · Bakım');
  });
  html = html.replace(/<section\b[^>]*id="(?:salon|hikaye)"[^>]*>[\s\S]*?<\/section>/i, section => {
    const paragraphs = [
      'Hikâyemiz 2000 yılında Bornova’da başladı. Bugün Kazımdirik Mahallesi, Gediz Caddesi 21 A’daki tek salonumuzda aynı özenle çalışmaya devam ediyoruz.',
      'Kadın kesimi, renk ve bakım işin merkezinde. Erkek ve çocuk randevuları da aynı ekibin temposunda ilerliyor. Önce saçınızı dinliyor, sonra gerçekten gereken işlemi konuşuyoruz.'
    ];
    let index = 0;
    return section.replace(/(<p\b[^>]*>)[\s\S]*?(<\/p>)/gi, (all, open, close) => index < paragraphs.length ? open + paragraphs[index++] + close : all);
  });
  let faqIndex = 0;
  html = html.replace(/<details\b[^>]*>[\s\S]*?<\/details>/gi, block => {
    if (/menü|Hizmetler.*Salon/s.test(block)) return block;
    if (faqIndex >= faq.length) return block;
    const [question, answer] = faq[faqIndex++];
    block = block.replace(/(<summary\b[^>]*>[\s\S]*?<span\b[^>]*>)[\s\S]*?(<\/span>)/i, '$1' + question + '$2');
    return block.replace(/(<\/summary>)[\s\S]*?(<\/details>)/i, '$1<p class="mt-4 font-body-md text-body-md text-on-surface-variant leading-relaxed pr-8">' + answer + '</p>$2');
  });
  html = html.replace(/<a\b[^>]*>[\s\S]*?<\/a>/gi, anchor => {
    const label = anchor.replace(/<[^>]*>/g, '').replace(/\s+/g, ' ').trim().replace(/^\d+\.\s*/, '');
    let destination;
    const serviceTitle = Object.keys(slugByTitle).find(title => label.startsWith(title));
    if (serviceTitle) destination = 'https://celilatilla.com/hizmetler/' + slugByTitle[serviceTitle];
    else if (/^(Randevu|RANDEVU)/.test(label)) destination = 'https://celilatilla.com/randevu';
    else if (/^Tüm hizmet|^Hizmetler$/i.test(label)) destination = 'https://celilatilla.com/hizmetler';
    else if (/^Salon/.test(label)) destination = 'https://celilatilla.com/hakkimizda';
    else if (/^Saç rehberi$/i.test(label)) destination = 'https://celilatilla.com/blog';
    else if (/^Gizlilik$/.test(label)) destination = 'https://celilatilla.com/gizlilik';
    else if (/^Site haritası$/.test(label)) destination = 'https://celilatilla.com/sitemap.xml';
    else if (/Instagram/i.test(label)) destination = 'https://www.instagram.com/celil_atilla/';
    if (destination) anchor = anchor.replace(/href="[^"]*"/i, 'href="' + destination + '"');
    return anchor;
  });
  html = html.replace(/ATELİER SERİSİ · FORM VE DURUŞ|ATELİER SERVİSİ · FORM VE DURUŞ/g, 'KESİM · RENK · BAKIM')
    .replace(/Bornova Kazımdirik · Zanaat Alanı/gi, 'Bornova / İzmir')
    .replace(/SALON MİMARİSİ VE FELSEFE/g, 'SALONDAN')
    .replace(/8 Temel Atölye Hizmeti/g, 'Hizmetlerimiz')
    .replace(/Randevu planlaması ve işlem öncesi fotoğraf danışmanlığı için bize WhatsApp hattımızdan doğrudan ulaşabilirsiniz\./g, 'Nasıl bir değişiklik düşündüğünüzü anlatın. İşlemi, süreyi ve saçınıza göre fiyatı randevudan önce birlikte netleştirelim.');
  const outsideImages = [...html.matchAll(/<img\b[^>]*src="([^"]*)"/gi)].filter(m => !allowed.some(a => m[1] === imageBase + a));
  if (outsideImages.length || /aida-public|background-image\s*:/i.test(html)) throw new Error('Unapproved image survived');
  fs.writeFileSync(file, html);
  console.log(JSON.stringify({file:name, images, h1:(html.match(/<h1\b/gi)||[]).length, faq:faqIndex, photoSource:'Only current celilatilla.com photos', productionFilesChanged:false}));
}
