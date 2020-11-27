import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { FriendService } from 'src/app/services/friend-service';
import { FriendModalService } from './modal/friend-modal.service';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.css']
})
export class FriendComponent implements AfterViewInit {

  displayedColumns: string[] = ['name', 'created', 'actions'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    private _friendService: FriendService,
    private friendModalService: FriendModalService) {

    this._friendService.get()
      .subscribe((res) => {
        this.dataSource.data = this.dataSource.data.concat(res.payload)
      });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    console.log(this.dataSource);
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
