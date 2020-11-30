import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-information-dialog',
  templateUrl: './information-dialog.component.html',
  styleUrls: []
})
export class InformationDialogComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }
}
