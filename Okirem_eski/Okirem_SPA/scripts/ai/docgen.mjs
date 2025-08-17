// Minimal repo introspection to generate PROJECT_SNAPSHOT.md and AI_CONTEXT.md
import { promises as fs } from 'fs';
import path from 'path';
import glob from 'glob';

const readJson = async (p) => JSON.parse(await fs.readFile(p, 'utf-8'));
const exists = async (p) => fs.access(p).then(() => true).catch(() => false);

const root = process.cwd();

const outDir = path.join(root, 'docs');
await fs.mkdir(outDir, { recursive: true });

const pkgPath = path.join(root, 'package.json');
const angularJsonPath = path.join(root, 'angular.json');

let pkg = null, angularJson = null;

if (await exists(pkgPath)) pkg = await readJson(pkgPath);
if (await exists(angularJsonPath)) angularJson = await readJson(angularJsonPath);

const angularVersion = pkg?.dependencies?.['@angular/core'] || pkg?.devDependencies?.['@angular/core'] || 'Bulunamadı';
const deps = Object.entries({ ...(pkg?.dependencies||{}), ...(pkg?.devDependencies||{}) })
  .filter(([name]) => ['@coreui/angular','@auth0/angular-jwt','ngx-toastr','ngx-scrollbar','@coreui/chartjs','pixi.js','rxjs','zone.js'].includes(name))
  .map(([name, version]) => `- ${name}: ${version}`).join('\n');

// Collect routes, guards, interceptors, components, services
const routeFiles = glob.sync('src/app/**/*.routes.ts', { cwd: root });
const guardFiles = glob.sync('src/app/**/guards/**/*.ts', { cwd: root });
const interceptorFiles = glob.sync('src/app/**/interceptors/**/*.ts', { cwd: root });
const serviceFiles = glob.sync('src/app/**/services/**/*.ts', { cwd: root });
const componentFiles = glob.sync('src/app/**/*.component.ts', { cwd: root });

const envFiles = glob.sync('src/environments/*.ts', { cwd: root });

const snapshot = `# PROJECT_SNAPSHOT — Auto
\n## 0. Yönetici Özeti
- Angular: ${angularVersion}
- Önemli bağımlılıklar:\n${deps || '- Bulunamadı'}
\n## 1. Dosya Envanteri (özet)
- Routes: ${routeFiles.length}
- Guards: ${guardFiles.length}
- Interceptors: ${interceptorFiles.length}
- Services: ${serviceFiles.length}
- Components: ${componentFiles.length}
\n## 2. Önemli Dosya Yolları
${[...routeFiles, ...guardFiles, ...interceptorFiles].map(f => `- ${f}`).join('\n') || '- Bulunamadı'}
\n## 3. Ortam Dosyaları
${envFiles.map(f => `- ${f}`).join('\n') || '- Bulunamadı'}
`;

await fs.writeFile(path.join(outDir, 'PROJECT_SNAPSHOT.md'), snapshot, 'utf-8');

// AI_CONTEXT: concatenation header to paste into ChatGPT
const head = `# AI_CONTEXT
- Tarih: ${new Date().toISOString()}
- Kaynak: docs/PROJECT_SNAPSHOT.md, docs/PROJECT_LOG.md, docs/ROADMAP.md, docs/decisions/*
- Kural: Yapay zekâ çıktıları yalnızca bu belgelere ve depoya referansla üretilecek.
`;

let projectLog = '';
let roadmap = '';
try { projectLog = await fs.readFile(path.join(outDir, 'PROJECT_LOG.md'), 'utf-8'); } catch {}
try { roadmap = await fs.readFile(path.join(outDir, 'ROADMAP.md'), 'utf-8'); } catch {}

const aiContext = [head, snapshot, projectLog, roadmap].join('\n\n---\n\n');
await fs.writeFile(path.join(root, 'docs/ai/AI_CONTEXT.md'), aiContext, 'utf-8');
console.log('Generated docs/PROJECT_SNAPSHOT.md and docs/ai/AI_CONTEXT.md');
