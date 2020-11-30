import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { GameService } from 'src/app/services/game-service';
import { ConfirmDialogService } from 'src/app/shared/components/dialogs/confirm-dialog/confirm-dialog/confirm-dialog.service';
import { InformationDialogService } from 'src/app/shared/components/dialogs/information-dialog/information-dialog/information-dialog.service';
import { GameLoanModalService } from './modal/game-loan-modal.service';
import { GameModalService } from './modal/game-modal.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements AfterViewInit {

  displayedColumns: string[] = ['name', 'created', 'situation', 'actions'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    private _gameService: GameService,
    private _gameModalService: GameModalService,
    private _confirmDialogService: ConfirmDialogService,
    private _informationDialogService: InformationDialogService,
    private _gameLoanModalService: GameLoanModalService,) {

    this._get();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  addGame() {
    this._openModal("Adicionar Jogo");
  }

  save(form): void {
    if (form.id) {
      this._patchGame(form);
    } else {
      this._postGame(form);
    }
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  detail(id) {
    this._getById(id).subscribe(res => {
      if (res) {
        let game = res;
        this._openModal("Detalhar Jogo", game);
      }
    });
  }

  remove(id) {
    this._confirmDialogService
      .open({ messageType: "info", message: "Deseja realmente Deletar seu jogo?" })
      .afterClosed()
      .subscribe((ok) => {
        if (ok)
          this._gameService.delete(id).subscribe(res => {
            if (res) {
              this._informationDialogService.open({ message: "Jogo deletado com sucesso." });
              this._get();
            }
          });
      });
  }

  loan(id) {
    let modalRef = this._gameLoanModalService.open({ title: "Emprestar Jogo" });
    modalRef.componentInstance.saveClick.subscribe((data: any) => {
      this._gameService.postLoan({ gameId: id, friendId: data.friend.id })
        .subscribe(res => {
          if (res) {
            this._informationDialogService.open({ message: "Jogo emprestado com sucesso." });
            this._get();
          }
        })
    });
  }

  return(id) {
    this._confirmDialogService
      .open({ messageType: "info", message: "Deseja realmente Receber o jogo de volta?" })
      .afterClosed()
      .subscribe((ok) => {
        if (ok)
          this._gameService.postDevolution({ gameId: id }).subscribe(res => {
            if (res) {
              this._informationDialogService.open({ message: "Jogo recebido com sucesso." });
              this._get();
            }
          });
      });
  }
  private _get() {
    this._gameService.get()
      .subscribe((res) => {
        this.dataSource.data = res;
      }, err => {
        this.dataSource.data = [];
      });
  }

  private _getById(id) {
    return this._gameService.getById(id);
  }

  private _postGame(form) {
    this._gameService.post(form).subscribe(res => {
      if (res) {
        this._informationDialogService.open({ message: "Jogo cadastrado com sucesso." });
        this._gameModalService.close();
        this._get();
      }
    });
  }

  private _patchGame(form) {
    this._gameService.patch(form).subscribe(res => {
      if (res) {
        this._informationDialogService.open({ message: "Jogo alterado com sucesso." });
        this._gameModalService.close();
        this._get();
      }
    });
  }

  private _openModal(title, game?) {
    let modalRef = this._gameModalService.open({ title: title });
    modalRef.componentInstance.saveClick.subscribe((data: any) => {
      this.save(data);
    });

    if (game)
      modalRef.componentInstance.buildForm(game);

  }
}
