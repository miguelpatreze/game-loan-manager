import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { HttpJwtInterceptor } from './interceptors/http-jwt-interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAutocompleteModule, MatButtonModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatInputModule, MatMenuModule, MatPaginatorIntl, MatPaginatorModule, MatSelectModule, MatSortModule, MatTableModule, MatToolbarModule, MatTooltipModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './shared/components/header/header.component';
import { FriendComponent } from './components/friend/friend.component';
import { AppRoutingModule } from './app-routing.module';
import { FriendModalComponent } from './components/friend/modal/friend-modal.component';
import { ConfirmDialogComponent } from './shared/components/dialogs/confirm-dialog/confirm-dialog/confirm-dialog.component';
import { InformationDialogComponent } from './shared/components/dialogs/information-dialog/information-dialog/information-dialog.component';
import { HttpErrorInterceptor } from './interceptors/http-error-interceptor';
import { GameComponent } from './components/game/game.component';
import { GameModalComponent } from './components/game/modal/game-modal.component';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { getBrPaginatorIntl } from './shared/components/paginator/br-paginator.intl';
import { GameLoanModalComponent } from './components/game/modal/game-loan-modal.component';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = {};
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FriendComponent,
    GameComponent,
    FriendModalComponent,
    GameModalComponent,
    GameLoanModalComponent,
    ConfirmDialogComponent,
    InformationDialogComponent
  ],
  imports: [
    NgxMaskModule.forRoot(options),
    ReactiveFormsModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    MatSortModule,
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
    MatDialogModule,
    MatTooltipModule,
    MatAutocompleteModule,
    MatSelectModule
  ],
  providers: [
    { provide: OAuthStorage, useValue: localStorage },
    { provide: HTTP_INTERCEPTORS, useClass: HttpJwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
    { provide: MatPaginatorIntl, useValue: getBrPaginatorIntl() }

  ],
  bootstrap: [AppComponent],
  entryComponents: [
    FriendModalComponent,
    GameModalComponent,
    GameLoanModalComponent,
    ConfirmDialogComponent,
    InformationDialogComponent
  ]
})
export class AppModule { }
