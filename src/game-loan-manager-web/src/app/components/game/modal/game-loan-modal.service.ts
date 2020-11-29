import { Injectable } from '@angular/core';
import { MatDialog, } from '@angular/material';
import { GameLoanModalComponent } from './game-loan-modal.component';
import { GameModalComponent } from './game-modal.component';

@Injectable({
    providedIn: 'root'
})
export class GameLoanModalService {
    constructor(private matDialog: MatDialog) { }

    open(data: any) {
        return this.matDialog.open(GameLoanModalComponent, {
            disableClose: true,
            width: 'auto',
            data: data,
            id: 'gameLoanModal'
        })
    }

    close() {
      this.matDialog.getDialogById('gameLoanModal').close()
    }
}
