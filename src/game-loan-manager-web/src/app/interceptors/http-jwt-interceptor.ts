import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class HttpJwtInterceptor implements HttpInterceptor {

    constructor(private oAuthService: OAuthService)
    { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${this.oAuthService.getAccessToken()}`
            }
        });

        return next.handle(req)
    }
}