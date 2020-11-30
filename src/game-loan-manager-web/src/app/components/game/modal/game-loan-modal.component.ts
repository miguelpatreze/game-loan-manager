import {
  Input,
  Component,
  Output,
  EventEmitter,
  Inject,
  OnInit,
} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { FriendService } from 'src/app/services/friend-service';
import { GameService } from 'src/app/services/game-service';

@Component({
  selector: 'game-loan-modal',
  templateUrl: './game-loan-modal.component.html',
  styleUrls: [],
})
export class GameLoanModalComponent implements OnInit {
  @Output() saveClick = new EventEmitter();

  form: FormGroup;
  friends: any[];
  filteredFriends: Observable<string[]>;

  constructor(
    private matDialog: MatDialog,
    public dialogRef: MatDialogRef<GameLoanModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder,
    private _friendService: FriendService,
    private _gameService: GameService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      friend: ['', Validators.required],
    });

    this._get();
  }

  close() {
    this.dialogRef.close();
  }

  onCancelClick() {
    this.close();
  }

  onSaveClick() {
    this.saveClick.emit(this.form.value);
  }

  displayFn(friend): string {
    return friend ? friend.name : friend;
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.friends.filter(friend => friend.name.toLowerCase().indexOf(filterValue) === 0);
  }

  private _get() {
    this._friendService.get()
      .subscribe((res) => {
        this.friends = res;

        this.filteredFriends = (this.form.controls['friend'] as FormControl).valueChanges.pipe(
          startWith(''),
          map(value => typeof value === 'string' ? value : value.name),
          map(name => name ? this._filter(name) : this.friends.slice())
        );
      }, err => {
        this.friends = [];
      });
  }
}
