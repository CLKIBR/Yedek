import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { FeatureFlagService } from '../../../core/services/feature-flag.service';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class FeatureFlagGuard implements CanActivate {
  constructor(private flagService: FeatureFlagService) {}

  canActivate(): Observable<boolean> {
    // Örneğin: 'newDashboard' flag'i aktifse route açılır
    return this.flagService.getByName('newDashboard').pipe(
      map(flag => !!flag?.enabled),
      catchError(() => of(false))
    );
  }
}
