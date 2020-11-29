import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import {
  AuthGuardService as AuthGuard
} from './auth/auth-guard.service';
import { FriendComponent } from './components/friend/friend.component';
import { GameComponent } from './components/game/game.component';

const routes: Routes = [
  {
    path: 'friends',
    component: FriendComponent,
    canActivate: [AuthGuard],
    data: {
      expectedRole: 'ADMIN'
    }
  },
  {
    path: 'games',
    component: GameComponent,
    canActivate: [AuthGuard],
    data: {
      expectedRole: 'ADMIN'
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
