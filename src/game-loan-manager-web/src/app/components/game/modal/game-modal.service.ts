import { Injectable } from '@angular/core';
import { MatDialog, } from '@angular/material';
import { GameModalComponent } from './game-modal.component';

@Injectable({
    providedIn: 'root'
})
export class GameModalService {
    constructor(private matDialog: MatDialog) { }

    open(data: any) {
        return this.matDialog.open(GameModalComponent, {
            disableClose: true,
            width: 'auto',
            data: data,
            id: 'gameModal'
        })
    }

    close() {
      this.matDialog.getDialogById('gameModal').close()
    }
}
