import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from './confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {

  constructor(private matDialog: MatDialog) { }

  open(data: any) {
    const dialogRef = this.matDialog.open(ConfirmDialogComponent, {
      width: 'auto',
      data: data,
      id: 'confirmDialog'
    });

    return dialogRef;
  }
}
