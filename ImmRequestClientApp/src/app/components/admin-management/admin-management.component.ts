import { SnackbarService } from './../../services/snackbar.service';
import { AdminService } from './../../services/admin.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Button, Column, Admin } from 'src/app/models/models';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { HtmlHelpers } from 'src/app/helpers/html.helper';
import { ConfirmationComponent } from 'src/app/modals/confirmation/confirmation.component';
import { Router } from '@angular/router';
import { ManagementComponent } from '../management/management.component';

@Component({
  selector: 'app-admin-management',
  templateUrl: './admin-management.component.html',
  styleUrls: ['./admin-management.component.css']
})
export class AdminManagementComponent implements OnInit {

  public buttons: Button[];

  public columns: Column[];

  public title: string;

  public loggedInId: number;

  public dataSource: BehaviorSubject<MatTableDataSource<any>>;

  constructor(private adminService: AdminService, 
    private snackbarService: SnackbarService,
    private dialog: MatDialog,
    private router: Router) { }

    @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;
    
  ngOnInit() {
    let source = new MatTableDataSource<any>();
    this.dataSource = new BehaviorSubject(source);

    this.getAllAdmins();
    this.title = "Manage Admins";
    this.columns = this.setManageAdminsColumns();
    this.buttons = this.setManageAdminsButtons();
    this.loggedInId = +localStorage.getItem('id');
  }

  setManageAdminsButtons(): Button[] {
    let editAdmin: Button = {
      buttonTooltip: "Edit Admin",
      iconName: "edit",
      callback: (admin) => {this.editAdmin(admin)}
    }
    let deleteAdmin: Button = {
      buttonTooltip: "Delete Admin",
      iconName: "delete",
      callback: (admin) => {this.deleteElement(admin)}
    }
    return [editAdmin, deleteAdmin];
  }

  deleteElement(admin: any) {
    this.deleteAdminDialog(admin);
  }
  editAdmin(admin: any) {
    let idToSend = admin.id;
    let emailToSend = admin.email;
    let usernameToSend = admin.username;
    this.router.navigateByUrl('/', {skipLocationChange: true})
    .then(()=> this.router.navigate(['/modify-admin'], {state: 
      {
        action: 'Edit', 
        id: idToSend, 
        email: emailToSend, 
        username: usernameToSend, 
        isLoggedIn: false}
      }));
  }

  setManageAdminsColumns(): Column[] {
    let usernameColumn: Column = {
      columnClass: "username",
      columnName: "Username",
      hasButtons: false
    }
    let emailColumn: Column = {
      columnClass: "email",
      columnName: "Email",
      hasButtons: false
    }
    let actionsColumn: Column = {
      columnClass: "actions",
      columnName: "Actions",
      hasButtons: true
    }
    return [usernameColumn, emailColumn, actionsColumn];
  }

  getAllAdmins(){
    this.adminService.GetAll()
    .subscribe(
      (response) => {
        var responseString = JSON.stringify(response);
        console.log(responseString);
        let source = new MatTableDataSource<any>();
        source.data = JSON.parse(responseString);
        source.data = source.data.filter(admin => admin.id != this.loggedInId);
        source.paginator = this.managementeComponent.paginator;
        this.dataSource.next(source);
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackbarService.configError
        });
      }
    )
  }

  deleteAdminDialog(admin: Admin){
    let dialogRef = this.dialog.open(ConfirmationComponent, {
      data: {
        elementDialogText : "Are you sure you want to delete this admin?", 
        elementDialogTitle: "Delete Admin"}
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.adminService.DeleteAdmin(admin).subscribe(
          (response) => {
            this.snackbarService.notifications$.next({
              message: "Admin has been deleted!",
              action: 'Success!',
              config: this.snackbarService.configSuccess
            });
            this.getAllAdmins();
          },
          (error) => {
            this.snackbarService.notifications$.next({
              message: HtmlHelpers.getHtmlErrorMessage(error),
              action: 'Error!',
              config: this.snackbarService.configError
            });
          }
        )
      }
    });
  }
}
