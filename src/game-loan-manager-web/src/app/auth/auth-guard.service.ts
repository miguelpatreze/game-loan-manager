import { state } from '@angular/animations';
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth-service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
    constructor(public auth: AuthService, public router: Router) { }
    canActivate(
        route: ActivatedRouteSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

        if (!this.auth.isLogged())
            return this.router.parseUrl('/');

        let expectedRole = route.data.expectedRole;

        if (expectedRole) {
            if (!this.auth.userHasRole(expectedRole))
                return this.router.parseUrl('/');
        }

        return true;
    }
}
