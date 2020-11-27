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
  selector: 'friend-modal',
  templateUrl: './friend-modal.component.html',
  styleUrls: ['./friend-modal.component.scss'],
})
export class FriendModalComponent  implements OnInit {
  @Input() title: String;
  @Input() saveText: String = 'Salvar';
  @Input() cancelText: String = 'Cancelar';

  @Input() delete: boolean = false;

  @Output() saveClick = new EventEmitter();
  @Output() cancelClick = new EventEmitter();

  form: FormGroup;

  constructor(
    private matDialog: MatDialog,
    public dialogRef: MatDialogRef<FriendModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder
  ) { }
  
  ngOnInit() {
    this.form = this.formBuilder.group({
      'name': [null, Validators.required]
    });
  }

  close() {
    this.dialogRef.close();
  }

  onSaveClick() {
    this.saveClick.emit();
  }

  onCancelClick() {
    this.cancelClick.emit();
    this.close();
  }

}
