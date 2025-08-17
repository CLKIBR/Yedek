import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AIKisisellestirmeService {
  getOneriler(kullaniciId: string): string[] {
    // Demo: Kullanıcıya özel öneri listesi
    return [
      'Matematik için kişiselleştirilmiş test',
      'Video: Zorlandığın konular için AI önerisi',
      'Okuma materyali: Son ilgi alanlarına göre seçildi'
    ];
  }
}
