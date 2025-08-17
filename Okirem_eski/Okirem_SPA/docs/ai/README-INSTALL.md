# Kurulum Adımları

1) Bu klasör ve dosyaları repoya kopyalayın.
2) `package.json` içine aşağıdaki scriptleri ekleyin:
```json
{
  "scripts": {
    "ai:context": "node scripts/ai/docgen.mjs"
  }
}
```
3) `npm run ai:context` komutunu çalıştırın; `docs/PROJECT_SNAPSHOT.md` ve `docs/ai/AI_CONTEXT.md` üretilecektir.
4) Her yeni ChatGPT oturumunda `docs/ai/AI_CONTEXT.md` içeriğini başa yapıştırın.
5) GitHub'da `Issues` ve `Projects` kullanarak ROADMAP maddelerini iş olarak yönetin.
6) PR açarken şablon otomatik gelecektir (DoD kontrol listesi).

Not: Backend .NET eklendiğinde docgen scriptini genişletebilirsiniz.
