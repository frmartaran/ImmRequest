import { SnackbarService } from './../../services/snackbar.service';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { MatTableDataSource } from '@angular/material';
import { Column } from 'src/app/models/models';
import { ManagementService } from 'src/app/services/management.service';
import { HtmlHelpers } from 'src/app/helpers/html.helper';

@Component({
  selector: 'app-get-request-status',
  templateUrl: './get-request-status.component.html',
  styleUrls: ['./get-request-status.component.css']
})
export class GetRequestStatusComponent implements OnInit {

  public numberValue: number;

  public requestDataSource$: BehaviorSubject<MatTableDataSource<any>>;

  public columns: Column[];

  public title: string;

  constructor(private snackbarService: SnackbarService,
    private managementService: ManagementService) { }

  ngOnInit() {
    this.title = "Request";
    let source = new MatTableDataSource<any>();
    this.requestDataSource$ = new BehaviorSubject<MatTableDataSource<any>>(source);
    this.initializeColumns();
  }

  initializeColumns() {
    this.columns = []
    let requestNumber: Column = {
        columnClass: "id",
        columnName: "Request Number",
        hasButtons: false
    }
    let requestStatus: Column = {
      columnClass: "status",
      columnName: "Status",
      hasButtons: false
    }
    this.columns.push(requestNumber);
    this.columns.push(requestStatus);
  }

  getRequestByNumber(){
    this.managementService.getCitizenRequestStatus(this.numberValue)
    .subscribe(
      (response) => {
        let source = new MatTableDataSource<any>();
        let requestStatus = {
          id: this.numberValue,
          status: response
        }
        source.data = [requestStatus];
        this.requestDataSource$.next(source)
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: Object.assign({}, {duration:3000}, this.snackbarService.configError)
        });
      }
    )
  }

}
