import { Injectable } from '@angular/core';
import { MatDialog, } from '@angular/material';
import { FriendModalComponent } from './friend-modal.component';

@Injectable({
    providedIn: 'root'
})
export class FriendModalService {
    constructor(private matDialog: MatDialog) { }

    open(data: any) {
        return this.matDialog.open(FriendModalComponent, {
            disableClose: true,
            width: 'auto',
            height: 'auto',
            data: data,
            id: 'friendModal'
        })
    }

    close() {
      this.matDialog.getDialogById('friendModal').close()
    }
}
