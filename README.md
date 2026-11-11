# ✈️ TripWise

**TripWise**, kullanıcıların seyahatlerini planlayabilecekleri ve yönetebilecekleri kapsamlı bir Full Stack seyahat planlama platformudur.

Proje, **.NET 11, ASP.NET Core Minimal API ve Angular** teknolojileri kullanılarak geliştirilmektedir. Backend tarafında Clean Architecture yaklaşımı benimsenirken, frontend tarafının Angular ile geliştirilmesi planlanmaktadır.

## 🎯 Projenin Amacı

TripWise ile seyahat planlama sürecinde ihtiyaç duyulan farklı işlemleri tek bir platform altında toplamayı hedefliyorum.

Kullanıcıların seyahat oluşturabilmesi, destinasyonları keşfedebilmesi, gezi rotalarını düzenleyebilmesi, konaklama ve ulaşım planlarını yönetebilmesi ve seyahat bütçelerini takip edebilmesi amaçlanmaktadır.

## 🛠️ Teknolojiler

### Backend
- .NET 11
- ASP.NET Core Minimal API
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation

### Frontend
- Angular *(planlanan)*

## 🏗️ Mimari ve Kullanılan Yaklaşımlar

- **Clean Architecture:** Katmanların sorumluluklarının ayrılması ve bağımlılıkların kontrol altında tutulması.
- **CQRS:** Komut ve sorgu işlemlerinin birbirinden ayrılması.
- **MediatR:** Uygulama içerisindeki istek ve handler akışlarının yönetilmesi.
- **Repository Pattern:** Veri erişim işlemlerinin soyutlanması.
- **Result Pattern:** İşlem sonuçlarının standart bir yapı üzerinden yönetilmesi.
- **FluentValidation:** İsteklerin merkezi olarak doğrulanması.
- **MediatR Pipeline Behavior:** Validasyon işlemlerinin handler çalışmadan önce yürütülmesi.
- **Soft Delete:** Kayıtların fiziksel olarak silinmeden pasif hâle getirilmesi.
- **Audit Fields:** Oluşturulma, güncellenme ve silinme bilgilerinin takip edilmesi.
- **Global Exception Handling:** Beklenmeyen hataların merkezi olarak ele alınması.
- **Pagination:** Listeleme işlemlerinde sayfalama uygulanması.
## ✅ Şu Ana Kadar Tamamlanan Çalışmalar

- [x] Solution ve Clean Architecture katmanlarının oluşturulması.
- [x] Domain katmanında temel Entity yapısının oluşturulması.
- [x] Audit alanları ve Soft Delete altyapısının hazırlanması.
- [x] EF Core ve SQL Server bağlantısının yapılandırılması.
- [x] Repository Pattern altyapısının oluşturulması.
- [x] Trip entity ve TripStatus enum yapısının oluşturulması.
- [x] Trip oluşturma işlemi (Create Trip).
- [x] Trip detaylarını getirme işlemi (Get Trip By Id).
- [x] Trip listeleme işlemi (Get Trips).
- [x] Sayfalama altyapısının uygulanması.
- [x] Trip güncelleme işlemi (Update Trip).
- [x] Soft Delete ile Trip silme işlemi (Delete Trip).
- [x] CQRS ve MediatR entegrasyonu.
- [x] FluentValidation ve Validation Pipeline Behavior entegrasyonu.
- [x] Result Pattern uygulaması.
- [x] Global Exception Handling yapılandırması.
- [x] Scalar ile API dokümantasyonu.
## 🗺️ Geliştirme Yol Haritası

Aşağıdaki özellikler geliştirme sürecinde ele alınması planlanan modüllerdir.

### Backend Geliştirmeleri
- [ ] Kullanıcı kayıt ve giriş işlemleri.
- [ ] Kimlik doğrulama ve yetkilendirme (Authentication & Authorization).
- [ ] Kullanıcı profil yönetimi.
- [ ] Destinasyon yönetimi.
- [ ] Seyahat ve gezi rotası yönetimi.
- [ ] Ziyaret edilecek yerlerin yönetimi.
- [ ] Konaklama yönetimi.
- [ ] Ulaşım planlama.
- [ ] Seyahat bütçesi ve harcama takibi.
- [ ] Gerekli modüller için harici API entegrasyonları.

### Frontend Geliştirmeleri
- [ ] Angular projesinin oluşturulması.
- [ ] Backend API entegrasyonu.
- [ ] Kullanıcı arayüzü ve ortak bileşenlerin geliştirilmesi.
- [ ] Kullanıcı kayıt ve giriş ekranları.
- [ ] Seyahat yönetim ekranları.
- [ ] Destinasyon ve rota planlama ekranları.
- [ ] Bütçe ve harcama takip ekranları.

### İyileştirmeler
- [ ] Kod kalitesini artırmaya yönelik refactoring çalışmaları.
- [ ] Performans iyileştirmeleri.
- [ ] Güvenlik kontrollerinin geliştirilmesi.
- [ ] Uygun noktalarda test altyapısının oluşturulması.

## 📌 Geliştirme Durumu

TripWise aktif olarak geliştirilmektedir. Mevcut aşamada backend altyapısı ve temel seyahat yönetimi işlemleri üzerinde çalışılmaktadır. Angular frontend ve diğer işlevsel modüller sonraki geliştirme aşamalarında projeye eklenecektir.

Proje geliştikçe bu README dosyası da güncellenecektir.

## 👨‍💻 Geliştirici

**Emirhan Hacıoğlu**

GitHub: [github.com/Emirhan547](https://github.com/Emirhan547)

---

*TripWise — Plan your journey, your way.*
