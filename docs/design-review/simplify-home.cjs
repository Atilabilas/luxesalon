const fs = require('node:fs');
const path = require('node:path');
const css = `<style id="home-simplification">
.home-block{max-width:1280px;margin:auto;padding:48px 32px}.home-heading{display:flex;align-items:baseline;justify-content:space-between;gap:20px;margin-bottom:24px}.home-block h2{font:400 32px/1.2 'Playfair Display',serif}.home-link{display:inline-flex;align-items:center;min-height:44px;font-size:12px;text-decoration:underline;text-underline-offset:5px}.home-grid{display:grid;grid-template-columns:repeat(3,minmax(0,1fr));gap:24px}.home-service{color:inherit;text-decoration:none;display:block}.home-service img{width:100%;height:180px;object-fit:cover}.home-service h3{font:400 23px/1.3 'Playfair Display',serif;margin:14px 0 6px}.home-service p{font-size:14px;line-height:1.6;color:#57524c}.home-caption{font-size:11px;color:#6f685c;margin-top:5px}.home-story{background:#f5f2ed;border-top:1px solid #e8e4dc}.home-story .home-block{display:flex;align-items:center;justify-content:space-between;gap:40px;padding-top:32px;padding-bottom:32px}.home-story h2{font-size:27px;margin-bottom:8px}.home-story p{font-size:14px;line-height:1.6;max-width:640px;color:#57524c}.home-booking{background:#1a1a1a;color:#faf9f7}.home-booking .home-block{display:flex;align-items:center;justify-content:space-between;gap:32px;padding-top:40px;padding-bottom:40px}.home-booking p{font-size:14px;color:#d4cec4;margin-top:8px}.home-actions{display:flex;align-items:center;gap:20px;flex-shrink:0}.home-cta{display:inline-flex;align-items:center;justify-content:center;min-height:48px;padding:12px 24px;background:#faf9f7;color:#1a1a1a;font-size:12px;font-weight:600;text-decoration:none}.home-footer{padding:24px 32px;max-width:1280px;margin:auto;display:flex;justify-content:space-between;gap:24px;color:#635d55;font-size:12px;line-height:1.8}.home-footer a{min-height:44px;display:inline-flex;align-items:center;margin-right:20px;color:inherit}header nav a{min-height:44px;display:inline-flex;align-items:center}
@media(max-width:767px){.home-block{padding:32px 20px}.home-heading{gap:12px;margin-bottom:20px}.home-block h2{font-size:27px}.home-grid{grid-template-columns:1fr;gap:0}.home-service{display:grid;grid-template-columns:88px 1fr;gap:16px;align-items:center;padding:16px 0;border-top:1px solid #e8e4dc}.home-service img{height:100px}.home-service h3{font-size:22px;margin:0 0 4px}.home-service p{font-size:13px}.home-caption{font-size:10px}.home-story .home-block,.home-booking .home-block{display:block;padding:28px 20px}.home-story .home-link{margin-top:8px}.home-actions{margin-top:22px;flex-wrap:wrap}.home-footer{padding:20px;display:block}.home-footer nav{margin-top:10px}}
</style>`;
const services = [
  ['Kadın kesim','Yüze uygun kesim, kahkül ve fön','kadin-kesim.webp','kadin-sac-kesimi'],
  ['Renklendirme','Balyaj, ombre, röfle ve kök rötuşu','balyaj.webp','balyaj-ombre-sombre'],
  ['Bakım','Keratin, saç botoksu ve onarım','fon.webp','keratin-sac-botoksu']
];
const serviceSection = `<section class="home-block" id="hizmetler" aria-labelledby="home-services-title"><div class="home-heading"><h2 id="home-services-title">Saçınıza göre.</h2><a class="home-link" href="https://celilatilla.com/hizmetler">Tüm hizmetler ↗</a></div><div class="home-grid">${services.map(([title,description,image,slug])=>`<a class="home-service" href="https://celilatilla.com/hizmetler/${slug}"><img src="https://celilatilla.com/images/services/${image}" alt="${title} — temsili görsel" loading="lazy"><div><h3>${title} ↗</h3><p>${description}</p><div class="home-caption">Temsili görsel</div></div></a>`).join('')}</div></section>`;
const story = `<section class="home-story" id="salon"><div class="home-block"><div><h2>2000’den beri Bornova’da.</h2><p>Kazımdirik’teki tek salonumuzda kadın, erkek ve çocuk saçına aynı özenle devam ediyoruz.</p></div><a class="home-link" href="https://celilatilla.com/hakkimizda">Salonu ve ekibi tanıyın ↗</a></div></section>`;
const booking = `<section class="home-booking" id="iletisim"><div class="home-block"><div><h2>Önce biraz konuşalım.</h2><p>İşlemi ve uygun zamanı birlikte netleştirelim.</p></div><div class="home-actions"><a class="home-cta" href="https://celilatilla.com/randevu">Randevu al ↗</a><a class="home-link" href="https://wa.me/905347944393" target="_blank" rel="noopener">WhatsApp ↗</a></div></div></section>`;
const footer = `<footer class="home-footer"><div><a href="#top" aria-label="Celil ve Atilla ana sayfa">Celil &amp; Atilla</a><p>Kazımdirik Mah. Gediz Cd. 21 A · Bornova / İzmir<br><a href="tel:+905347944393">0534 794 43 93</a></p></div><nav aria-label="Alt menü"><a href="https://celilatilla.com/blog">Saç rehberi ↗</a><a href="https://www.instagram.com/celil_atilla/" target="_blank" rel="noopener">Instagram ↗</a><a href="https://celilatilla.com/gizlilik">Gizlilik</a></nav></footer>`;
for(const name of ['desktop','mobile']){
  const file=path.join(__dirname,name+'.html');
  let html=fs.readFileSync(file,'utf8');
  const backup=path.join(__dirname,name+'.v1.html');
  if(!fs.existsSync(backup))fs.writeFileSync(backup,html);
  const hero=html.match(/<main>\s*(?:<!--[\s\S]*?-->\s*)?(<section\b[\s\S]*?<\/section>)/i)?.[1];
  if(!hero)throw new Error('Hero missing: '+name);
  let compactHero=hero.replace('pt-16 pb-24','pt-10 pb-12').replace(/aspect-\[4\/5\]/g,'aspect-[4/3]')
    .replace(/Atelier Seri[s]?i · Form ve Duruş|Atelier Servisi · Form ve Duruş/gi,'Kesim · Renk · Bakım');
  html=html.replace(/<main>[\s\S]*?<\/main>/i,'<main>'+compactHero+serviceSection+story+booking+'</main>')
    .replace(/<footer\b[\s\S]*?<\/footer>/i,footer)
    .replace(/<aside\b[\s\S]*?<\/aside>/gi,'')
    .replace(/<a\b[^>]*href="#sss"[^>]*>[\s\S]*?<\/a>/gi,'')
    .replace(/(<body\b[^>]*)(>)/i,'$1 id="top"$2')
    .replace(/(<header\b[\s\S]*?<a\b[^>]*href=")[^"]*(")/i,'$1#top$2')
    .replace(/(<nav\b[^>]*>)/i,'$1<a class="home-link" href="#top">Ana sayfa</a>')
    .replace('</head>',css+'</head>');
  fs.writeFileSync(file,html);
  console.log(JSON.stringify({name,sections:(html.match(/<section\b/g)||[]).length,guideCards:html.includes('İyi saçın küçük notları'),faq:html.includes('<details'),photoCount:(html.match(/<img\b/g)||[]).length}));
}
