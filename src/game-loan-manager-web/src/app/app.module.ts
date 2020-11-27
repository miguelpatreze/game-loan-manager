import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { HttpJwtInterceptor } from './interceptors/http-jwt-interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatIconModule, MatMenuModule, MatMenuTrigger, MatToolbarModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './shared/components/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: OAuthStorage, useValue: localStorage },
    { provide: HTTP_INTERCEPTORS, useClass: HttpJwtInterceptor, multi: true }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
