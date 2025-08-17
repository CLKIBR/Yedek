# AI_CONTEXT
- Tarih: 2025-08-10T19:16:55.057Z
- Kaynak: docs/PROJECT_SNAPSHOT.md, docs/PROJECT_LOG.md, docs/ROADMAP.md, docs/decisions/*
- Kural: Yapay zekâ çıktıları yalnızca bu belgelere ve depoya referansla üretilecek.


---

# PROJECT_SNAPSHOT — Auto

## 0. Yönetici Özeti
- Angular: ^20.1.0
- Önemli bağımlılıklar:
- @auth0/angular-jwt: ^5.2.0
- @coreui/angular: ^5.5.6
- @coreui/chartjs: ^4.0.0
- ngx-scrollbar: ^18.0.0
- ngx-toastr: ^19.0.0
- pixi.js: ^7.4.3
- rxjs: ~7.8.0
- zone.js: ~0.15.0

## 1. Dosya Envanteri (özet)
- Routes: 9
- Guards: 3
- Interceptors: 10
- Services: 11
- Components: 0

## 2. Önemli Dosya Yolları
- src/app/app.routes.ts
- src/app/features/features_components/auth/login/login.routes.ts
- src/app/features/features_components/auth/register/register.routes.ts
- src/app/features/features_components/home/home.routes.ts
- src/app/features/features_components/legal/legal.routes.ts
- src/app/pages/admin/admin.routes.ts
- src/app/pages/parent/parent.routes.ts
- src/app/pages/student/student.routes.ts
- src/app/pages/teacher/teacher.routes.ts
- src/app/core/guards/index.ts
- src/app/core/guards/login/index.ts
- src/app/core/guards/login/login.guard.ts
- src/app/core/interceptors/auth/auth.interceptor.ts
- src/app/core/interceptors/auth/index.ts
- src/app/core/interceptors/caching/index.ts
- src/app/core/interceptors/error/error.interceptor.ts
- src/app/core/interceptors/error/index.ts
- src/app/core/interceptors/index.ts
- src/app/core/interceptors/loading/index.ts
- src/app/core/interceptors/loading/loading.interceptor.ts
- src/app/core/interceptors/logging/index.ts
- src/app/core/interceptors/retry/index.ts

## 3. Ortam Dosyaları
- src/environments/environment.prod.ts
- src/environments/environment.ts


---

# Proje Günlüğü (PROJECT_LOG)

> Kural: Güne başlarken ve bitirirken güncelleyin.

## Genel Bilgi
Platform: (örn. 5–8. sınıf öğrenim platformu)
Teknolojiler: Angular, .NET (Clean Architecture), PostgreSQL

## Günlük Kayıtlar
- 2025-08-10: (Örnek) Auth akışı tamamlandı; öğrenci dashboard ilk taslak bitti.


---

# Yol Haritası (ROADMAP)

Durumlar: Planned ▸ Spec'd ▸ In Dev ▸ Review ▸ Merged ▸ Released

## Epics
- Katalog & İçerik Akışı
- Quiz/Test Sistemi
- İlerleme & Raporlama
- Ödeme & Abonelik
- Admin CMS
- Güvenlik & İzleme

## Backlog
- [Planned] Katalog: Grade ▸ Subject ▸ Unit ▸ Topic gezinimi
- [Planned] Quiz: Start/Next/Submit akışı
- [Planned] Progress: Son test skoru ve % tamamlama paneli
