# 🌐 GelecekBlog

Doğuş Teknoloji – **Geleceğe Giriş Programı** kapsamında geliştirilmiş, tam işlevsel ve modern bir **blog platformu**dur. Geliştirici: **[aralozaltin](https://github.com/aralozaltin)**

---

## 🚀 Özellikler

### 👥 Kullanıcı Sistemi
- Kayıt ol, giriş yap
- Admin & Kullanıcı rolleri
- Rol tabanlı yetkilendirme (Yorum/Post silme, yönetim panelleri)

### 📝 Postlar
- Gönderi oluşturma, silme, detay görüntüleme
- Kategoriye göre filtreleme
- Görsel yükleme (uploads klasörü)

### 💬 Yorumlar
- Giriş yapan kullanıcı yorum ekleyebilir
- Giriş yapmayan sadece okuyabilir
- Admin ve yorum sahibi yorumu silebilir

### 🛠️ Admin Paneli
- Kullanıcı yönetimi (admin yap/kaldır, sil)
- Kategori yönetimi (CRUD)
- Dashboard (istatistik ve son içerikler)

---

## 💾 Kullanılan Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core (SQLite)
- ASP.NET Identity
- Bootstrap 5
- Bogus (Fake Data)
- Repository Pattern + DI

---

## 📸 Görseller

Uygulama çalıştığında `wwwroot/uploads/` klasöründeki görsellerden rastgele postlara atanır.

---

## 🧪 Seed Data

Uygulama ilk çalıştığında:
- 🔐 Admin oluşturulur  
  `admin@site.com` / `Admin123*`
- 📁 5 kategori eklenir
- 📝 150 sahte gönderi üretilir
- 📸 Yüklenmiş görsellerden rastgele atanır

---

## 🛠️ Kurulum

```bash
git clone https://github.com/aralozaltin/gelecekblog.git
cd gelecekblog

dotnet restore
dotnet ef database update
dotnet run
