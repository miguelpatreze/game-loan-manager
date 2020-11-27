import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthService } from 'src/app/auth/auth-service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: [],
})
export class HeaderComponent {
    constructor(private oauthSrv: OAuthService, private authService: AuthService) { }

    login() {
        this.authService.login();
    }
    logout() {
        this.authService.logout();
    }

    isLogged() {
        return this.authService.isLogged();
    }
    getUserName() {
        return this.authService.getUserName();
    }
}
