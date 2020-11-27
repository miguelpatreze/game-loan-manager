import { OAuthService } from 'angular-oauth2-oidc';
import { OnInit, Injectable } from '@angular/core';
import { AuthServiceConfig } from './auth-service-config';
import { filter } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

const config: AuthServiceConfig = new AuthServiceConfig;

@Injectable({ providedIn: 'root' })
export class AuthService implements OnInit {
    public tokenReceived = new BehaviorSubject(null);

    roles: any;
    claims: any;
    constructor(private oAuthService: OAuthService) {
        this.setClaims();
        this.oAuthService.events
        .pipe(filter(e => e.type === 'token_received'))
        .subscribe(token => {
            this.setClaims();   
            this.tokenReceived.next(token);
        });
    }

    ngOnInit(): void {
    }

    configureOAuth() {
        this.oAuthService.configure(config);
        this.oAuthService.loadDiscoveryDocumentAndTryLogin();
    }

    login() {
        this.oAuthService.initLoginFlow();
    }

    logout() {
        this.oAuthService.logOut();
    }

    isLogged() {
        return this.oAuthService.hasValidAccessToken();
    }
    getUserName() {
        return this.claims['name'] || '';
    }

    setClaims() {
        this.claims = this.oAuthService.getIdentityClaims() || {};
        this.roles = this.claims['role'] || [];
    }

}
