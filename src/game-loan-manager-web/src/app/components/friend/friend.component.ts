import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { FriendService } from 'src/app/services/friend-service';
import { FriendModalService } from './modal/friend-modal.service';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.css']
})
export class FriendComponent implements AfterViewInit {

  displayedColumns: string[] = ['name', 'created', 'actions'];
  data: any[] = this._friendService.get();
  dataSource: MatTableDataSource<any>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(
    private _friendService: FriendService,
    private friendModalService: FriendModalService) {

    this.dataSource = new MatTableDataSource(this.data);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  addFriend() {

    let modalRef = this.friendModalService.open({ title: "Adicionar Amigo" });
    modalRef.componentInstance.saveClick.subscribe((data: any) => {
      this.save();
    });
  }

  save(): void {
    console.log('save pressed');
  }
}
