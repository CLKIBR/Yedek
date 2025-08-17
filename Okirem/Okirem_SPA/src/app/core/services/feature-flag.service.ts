import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FeatureFlag } from '../../app/core/models/feature-flag.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class FeatureFlagService {
  private apiUrl = '/api/FeatureFlags';

  constructor(private http: HttpClient) {}

  getAll(): Observable<FeatureFlag[]> {
    return this.http.get<{ items: FeatureFlag[] }>(this.apiUrl).pipe(
      map(res => res.items)
    );
  }

  getById(id: string): Observable<FeatureFlag> {
    return this.http.get<FeatureFlag>(`${this.apiUrl}/${id}`);
  }

  getByName(name: string): Observable<FeatureFlag | undefined> {
    return this.getAll().pipe(
      map(flags => flags.find(f => f.name === name))
    );
  }

  create(flag: Omit<FeatureFlag, 'id'>): Observable<FeatureFlag> {
    return this.http.post<FeatureFlag>(this.apiUrl, flag);
  }

  update(flag: FeatureFlag): Observable<FeatureFlag> {
    return this.http.put<FeatureFlag>(this.apiUrl, flag);
  }

  delete(id: string): Observable<{ id: string }> {
    return this.http.delete<{ id: string }>(`${this.apiUrl}/${id}`);
  }
}
