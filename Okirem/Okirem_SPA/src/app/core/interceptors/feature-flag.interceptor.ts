import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class FeatureFlagInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Gerekirse istekleri flag’lere göre manipüle edebilirsiniz
    // Örneğin, belirli bir flag kapalıysa isteği iptal edebilir veya değiştirebilirsiniz
    return next.handle(req);
  }
}
