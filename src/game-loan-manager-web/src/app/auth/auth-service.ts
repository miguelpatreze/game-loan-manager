import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { OnInit, Injectable } from '@angular/core';
import { AuthServiceConfig } from './auth-service-config';

const config: AuthServiceConfig = new AuthServiceConfig;

@Injectable({ providedIn: 'root' })
export class AuthService implements OnInit {

    roles: any;
    claims: any;
    constructor(private oAuthService: OAuthService) {
        this.setClaims();
    }

    ngOnInit(): void {
    }

    configureOAuth() {
        this.oAuthService.configure(config);
        this.oAuthService.tokenValidationHandler = new JwksValidationHandler();
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
        return this.claims['sub'] || '';
    }

    setClaims() {
        this.claims = this.oAuthService.getIdentityClaims() || {};
        this.roles = this.claims['roles'] || [];
    }

}
