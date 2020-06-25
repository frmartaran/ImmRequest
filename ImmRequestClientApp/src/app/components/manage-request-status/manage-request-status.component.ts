import { ShowRequestValuesComponent } from './../../modals/show-request-values/show-request-values.component';
import { CitizenRequest, RequestValue } from './../../models/models';
import { ManagementService } from 'src/app/services/management.service';
import { Component, OnInit } from '@angular/core';
import { Button, Column } from 'src/app/models/models';
import { BehaviorSubject } from 'rxjs';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { HtmlHelpers } from 'src/app/helpers/html.helper';
import { StatusHelper } from 'src/app/helpers/status.helper';
import { EditStatusComponent } from 'src/app/modals/edit-status/edit-status.component';

@Component({
  selector: 'app-manage-request-status',
  templateUrl: './manage-request-status.component.html',
  styleUrls: ['./manage-request-status.component.css']
})
export class ManageRequestStatusComponent implements OnInit {

  public buttons: Button[];

  public columns: Column[];

  public title: string;

  public dataSource$: BehaviorSubject<MatTableDataSource<any>>;

  public requests: any[];

  constructor(private managementService: ManagementService,
    private snackbarService: SnackbarService,
    private dialog: MatDialog) { }

  ngOnInit() {
    let source = new MatTableDataSource<any>();
    this.dataSource$ = new BehaviorSubject<MatTableDataSource<any>>(source);
    this.requests = [];
    this.title = "Requests Management";
    this.initializeColumns();
    this.initializeButtons();
    this.getCitizenRequests();
  }

  initializeColumns() {
    this.columns = []
    let requestNumber: Column = {
        columnClass: "id",
        columnName: "Request Number",
        hasButtons: false
    };
    let requestDescription: Column = {
      columnClass: "description",
      columnName: "Request Description",
      hasButtons: false
    };
    let citizenName: Column = {
      columnClass: "citizenName",
      columnName: "Citizen Name",
      hasButtons: false
    };
    let email: Column = {
      columnClass: "email",
      columnName: "Email",
      hasButtons: false
    };
    let phone: Column = {
      columnClass: "phone",
      columnName: "Phone",
      hasButtons: false
    };
    let areaName: Column = {
      columnClass: "areaName",
      columnName: "Area Name",
      hasButtons: false
    };
    let topicName: Column = {
      columnClass: "topicName",
      columnName: "Topic Name",
      hasButtons: false
    };
    let topicTypeName: Column = {
      columnClass: "topicTypeName",
      columnName: "Topic Type Name",
      hasButtons: false
    };
    let requestStatus: Column = {
      columnClass: "status",
      columnName: "Status",
      hasButtons: false
    };
    let actionsColumn: Column = {
      columnClass: "actions",
      columnName: "Actions",
      hasButtons: true
    };
    this.columns.push(requestNumber);
    this.columns.push(requestDescription);
    this.columns.push(citizenName);
    this.columns.push(email);
    this.columns.push(phone);
    this.columns.push(areaName);
    this.columns.push(topicName);
    this.columns.push(topicTypeName);
    this.columns.push(requestStatus);
    this.columns.push(actionsColumn);
  }

  initializeButtons() {
      let editButton: Button = {
          buttonTooltip: "Edit Status",
          iconName: "edit",
          callback: (request) => {this.editRequestStatus(request)}
      }
      let viewAdditionalFieldsButton: Button = {
        buttonTooltip: "View Additional Fields",
        iconName: "visibility",
        callback: (request) => {this.openShowRequestValuesModal(request.values)}
    }
      this.buttons = [editButton, viewAdditionalFieldsButton];
  }

  getCitizenRequests(){
    this.managementService.getCitizenRequests()
    .subscribe(
      (response) => {
        try{
          let source = new MatTableDataSource<any>();
          this.requests = response.map(req => this.getDataSourceFromRequest(req));
          source.data = this.requests;
          this.dataSource$.next(source);
        }catch(exception){
          this.snackbarService.notifications$.next({
            message: exception,
            action: "Error!",
            config: Object.assign({}, {duration:3000}, this.snackbarService.configError)
          });
        }
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

  editRequestStatus(request:any){
    let editRequestStatus = this.dialog.open(EditStatusComponent);
    editRequestStatus.afterClosed().subscribe((res) => {
        if (res) {
          try{
            let status = StatusHelper.getStatusNumberFromName(res);
            this.managementService.updateCitizenRequestStatus(request.id, status)
            .subscribe(
              (response) => {
                this.snackbarService.notifications$.next({
                  message: response,
                  action: "Success!",
                  config: Object.assign({}, {duration:3000}, this.snackbarService.configSuccess)
                });
                let source = new MatTableDataSource<any>();
                this.requests = this.requests.map(req => {
                  if(req.id == request.id)
                    req.status = res;
                  return req;
                });
                source.data = this.requests;
                this.dataSource$.next(source);
              },
              (error) => {
                this.snackbarService.notifications$.next({
                  message: HtmlHelpers.getHtmlErrorMessage(error),
                  action: 'Error!',
                  config: Object.assign({}, {duration:3000}, this.snackbarService.configError)
                });
              }
            )
          }catch(error){
            this.snackbarService.notifications$.next({
              message: error,
              action: "Error!",
              config: Object.assign({}, {duration:3000}, this.snackbarService.configError)
            });
          }
        }
    });
  }

  openShowRequestValuesModal(values:RequestValue[]){
    this.dialog.open(ShowRequestValuesComponent, {
      data: values
    });
  }

  getDataSourceFromRequest(request: CitizenRequest){
    return {
      description: request.description,
      citizenName: request.citizenName,
      id: request.id,
      status: StatusHelper.getStatusNameFromNumber(request.status),
      email: request.email,
      phone: request.phone,
      areaId: request.areaId,
      areaName: request.areaName,
      topicId: request.topicId,
      topicName: request.topicName,
      topicTypeId: request.topicTypeId,
      topicTypeName: request.topicTypeName,
      values: request.values
    }
  }
}
