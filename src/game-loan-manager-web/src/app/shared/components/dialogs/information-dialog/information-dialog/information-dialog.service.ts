import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { InformationDialogComponent } from './information-dialog.component';

@Injectable({
    providedIn: 'root'
})
export class InformationDialogService {

    constructor(private matDialog: MatDialog) { }

    open(data: any) {
        return this.matDialog.open(InformationDialogComponent, {
            disableClose: true,
            width: 'auto',
            data: data,
            id: 'informationDialog'
        })
    }

    close() {
        this.matDialog.getDialogById('informationDialog').close()
    }
}
