import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { FriendService } from 'src/app/services/friend-service';
import { FriendModalService } from './modal/friend-modal.service';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.css']
})
export class FriendComponent implements AfterViewInit {

  displayedColumns: string[] = ['name', 'cellPhoneNumber', 'created', 'actions'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    private _friendService: FriendService,
    private friendModalService: FriendModalService) {

    this._get();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  addFriend() {
    this._openModal("Adicionar Amigo");
  }

  save(form): void {
    if (form.id) {
      this._patchFriend(form);
    } else {
      this._postFriend(form);
    }
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  detail(id) {
    this._getById(id).subscribe(res => {
      if (res) {
        let friend = res;
        this._openModal("Detalhar Amigo", friend);
      }
    });
  }

  private _get() {
    this._friendService.get()
      .subscribe((res) => {
        this.dataSource.data = res;
      });
  }

  private _getById(id) {
    return this._friendService.getById(id);
  }

  private _postFriend(form) {
    this._friendService.post(form).subscribe(res => {
      if (res) {
        this.friendModalService.close();
        this._get();
        // this.messageDialog.open({ message: "Usuário cadastrado com sucesso." })
        //   .afterClosed().subscribe(() => {
        //     this.router.navigate(['/user']);
        //   });
      }
    }, err => {
      // this.messageDialog.open({ message: err.map((v, i, array) => v.errorMessage).join('\n') });
    });
  }

  private _patchFriend(form) {
    this._friendService.patch(form).subscribe(res => {
      if (res) {
        this.friendModalService.close();
        this._get();
        // this.messageDialog.open({ message: "Usuário cadastrado com sucesso." })
        //   .afterClosed().subscribe(() => {
        //     this.router.navigate(['/user']);
        //   });
      }
    }, err => {
      // this.messageDialog.open({ message: err.map((v, i, array) => v.errorMessage).join('\n') });
    });
  }

  private _openModal(title, friend?) {
    let modalRef = this.friendModalService.open({ title: title });
    modalRef.componentInstance.saveClick.subscribe((data: any) => {
      this.save(data);
    });

    if (friend)
      modalRef.componentInstance.buildForm(friend);

  }
}
