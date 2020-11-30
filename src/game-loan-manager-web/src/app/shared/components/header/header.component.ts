import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthService } from 'src/app/auth/auth-service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: [],
})
export class HeaderComponent {
    public userName:string = this.authService.getUserName();

    constructor(private authService: AuthService) { 
        this.authService.tokenReceived.subscribe(token => {
          if (token) this.userName = this.authService.getUserName();
        });
    }

    login() {
        this.authService.login();
    }
    logout() {
        this.authService.logout();
    }

    isLogged() {
        return this.authService.isLogged();
    }
}
