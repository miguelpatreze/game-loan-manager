import {
  Input,
  Component,
  Output,
  EventEmitter,
  Inject,
  OnInit,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'game-modal',
  templateUrl: './game-modal.component.html',
  styleUrls: [],
})
export class GameModalComponent implements OnInit {
  @Output() saveClick = new EventEmitter();
  @Output() cancelClick = new EventEmitter();

  form: FormGroup;

  constructor(
    private matDialog: MatDialog,
    public dialogRef: MatDialogRef<GameModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    if (!this.form) {
      this.form = this.formBuilder.group({
        'id': [null],
        'name': [null, [Validators.required, Validators.maxLength(50)]]
      });
    }
  }

  buildForm(game) {
    if (game) {
      this.form = this.formBuilder.group({
        'id': [game.id],
        'name': [game.name, Validators.required]
      });
    }
  }

  close() {
    this.dialogRef.close();
  }

  onSaveClick() {
    this.saveClick.emit(this.form.value);
  }

  onCancelClick() {
    this.cancelClick.emit();
    this.close();
  }

}
