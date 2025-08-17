import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ai-kisisellestirme',
  template: `
    <h2>AI Kişiselleştirme Modülü</h2>
    <p>Kullanıcıya özel öneriler ve akıllı içerik sunumu için temel arayüz.</p>
    <button (click)="testAI()">Kişiselleştirme Testi</button>
    <div *ngIf="sonuc">Sonuç: {{sonuc}}</div>
  `,
  standalone: true,
  imports: [CommonModule]
})
export class AIKisisellestirmeComponent {
  sonuc = '';

  testAI() {
    // Demo: Kişiselleştirme algoritması örneği
    this.sonuc = 'Kullanıcıya özel öneriler başarıyla oluşturuldu.';
  }
}
