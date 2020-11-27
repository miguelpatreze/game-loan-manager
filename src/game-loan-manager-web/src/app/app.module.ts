import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { HttpJwtInterceptor } from './interceptors/http-jwt-interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatInputModule, MatMenuModule, MatPaginatorModule, MatTableModule, MatToolbarModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './shared/components/header/header.component';
import { FriendComponent } from './components/friend/friend.component';
import { AppRoutingModule } from './app-routing.module';
import { FriendModalComponent } from './components/friend/modal/friend-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FriendComponent,
    FriendModalComponent
  ],
  imports: [
    ReactiveFormsModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    AppRoutingModule,
    FormsModule,
    BrowserModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [
    { provide: OAuthStorage, useValue: localStorage },
    { provide: HTTP_INTERCEPTORS, useClass: HttpJwtInterceptor, multi: true }

  ],
  bootstrap: [AppComponent],
  entryComponents: [
    FriendModalComponent
  ]
})
export class AppModule { }
