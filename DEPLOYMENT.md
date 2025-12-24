# 🚀 LuxeSalon - Docker ile Deployment Kılavuzu

Bu kılavuz, LuxeSalon projesini Railway veya Render üzerinde nasıl canlıya alacağınızı adım adım açıklar.

---

## 📋 Ön Gereksinimler

1. **GitHub Hesabı** - Projeyi GitHub'a yüklemek için
2. **Railway veya Render Hesabı** - Ücretsiz hesap yeterli
3. **Git** - Bilgisayarınızda kurulu olmalı

---

## 🔧 Adım 1: GitHub'a Yükleme

### 1.1 GitHub'da Yeni Repository Oluşturma
1. [github.com/new](https://github.com/new) adresine gidin
2. Repository adı: `luxesalon` (veya istediğiniz bir isim)
3. **Private** seçeneğini işaretleyin (önerilir)
4. **Create repository** butonuna tıklayın

### 1.2 Projeyi Git'e Ekleme
PowerShell veya Terminal'de şu komutları çalıştırın:

```powershell
cd C:\Users\ati\Desktop\LuxeSalon

# Git'i başlat
git init

# Tüm dosyaları ekle
git add .

# İlk commit
git commit -m "Initial commit - LuxeSalon Premium Salon Website"

# GitHub remote ekle (YOUR_USERNAME yerine kendi kullanıcı adınızı yazın)
git remote add origin https://github.com/YOUR_USERNAME/luxesalon.git

# Ana branch'i main olarak ayarla
git branch -M main

# GitHub'a gönder
git push -u origin main
```

---

## 🚂 Adım 2: Railway ile Deploy

### 2.1 Railway Hesabı Oluşturma
1. [railway.app](https://railway.app/) adresine gidin
2. **GitHub ile Giriş Yap** seçeneğini kullanın

### 2.2 Yeni Proje Oluşturma
1. Dashboard'da **New Project** butonuna tıklayın
2. **Deploy from GitHub repo** seçeneğini seçin
3. `luxesalon` repository'nizi seçin
4. Railway otomatik olarak Dockerfile'ı algılayacak

### 2.3 Ortam Değişkenlerini Ayarlama
Railway dashboard'unda **Variables** sekmesine gidin ve şu değişkenleri ekleyin:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:$PORT
```

### 2.4 Domain Ayarlama
1. **Settings** sekmesine gidin
2. **Networking** bölümünde **Generate Domain** butonuna tıklayın
3. Artık siteniz `your-app.up.railway.app` adresinde yayında!

### 2.5 Özel Domain (Opsiyonel)
Kendi domain'inizi bağlamak için:
1. **Settings > Networking > Custom Domain**
2. Domain adresinizi girin
3. DNS ayarlarınızda CNAME kaydı oluşturun

---

## 🎨 Adım 3: Render ile Deploy (Alternatif)

### 3.1 Render Hesabı Oluşturma
1. [render.com](https://render.com/) adresine gidin
2. **GitHub ile Giriş Yap** seçeneğini kullanın

### 3.2 Web Service Oluşturma
1. Dashboard'da **New > Web Service** seçin
2. GitHub repository'nizi bağlayın
3. Şu ayarları yapın:
   - **Name**: luxesalon
   - **Environment**: Docker
   - **Region**: Frankfurt (veya size yakın bir bölge)
   - **Instance Type**: Free (başlangıç için)

### 3.3 Ortam Değişkenleri
```
ASPNETCORE_ENVIRONMENT=Production
```

---

## 🗄️ Veritabanı Notları

### SQLite (Varsayılan - Geliştirme için)
- Proje varsayılan olarak SQLite kullanıyor
- Container yeniden başlatıldığında veriler sıfırlanır
- **Sadece demo/test için uygundur**

### PostgreSQL (Prodüksiyon için Önerilir)
Railway veya Render'da PostgreSQL eklemek için:

1. **Railway**: Dashboard > New > Database > PostgreSQL
2. **Render**: Dashboard > New > PostgreSQL

Ardından `appsettings.Production.json` dosyasını güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=<HOST>;Database=<DB>;Username=<USER>;Password=<PASS>"
  }
}
```

---

## 🔒 Güvenlik Kontrol Listesi

- [ ] `appsettings.json` içindeki admin şifresini değiştirin
- [ ] HTTPS'in etkin olduğundan emin olun
- [ ] Prodüksiyon ortamında detaylı hata mesajlarını kapatın
- [ ] Veritabanı yedekleme stratejisi belirleyin

---

## 🆘 Sorun Giderme

### Build Hatası
```bash
# Lokal'de Docker build test edin
docker build -t luxesalon .
docker run -p 8080:8080 luxesalon
```

### Uygulama Başlamıyor
- Railway/Render loglarını kontrol edin
- `ASPNETCORE_URLS` değişkeninin doğru ayarlandığından emin olun
- Port 8080'in Dockerfile'da açıldığından emin olun

### Veritabanı Bağlantı Hatası
- Connection string'in doğru olduğundan emin olun
- Veritabanı servisinin çalıştığından emin olun

---

## 📞 Destek

Sorularınız için:
- GitHub Issues açın
- Railway/Render dökümantasyonlarını inceleyin

---

**🎉 Tebrikler! LuxeSalon artık canlıda!**
