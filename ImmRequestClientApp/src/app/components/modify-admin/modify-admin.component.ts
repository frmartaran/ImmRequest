import { LoginService } from 'src/app/services/login.service';
import { SnackbarService } from './../../services/snackbar.service';
import { AdminService } from './../../services/admin.service';
import { Admin } from './../../models/models';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HtmlHelpers } from 'src/app/helpers/html.helper';

@Component({
  selector: 'app-modify-admin',
  templateUrl: './modify-admin.component.html',
  styleUrls: ['./modify-admin.component.css']
})
export class ModifyAdminComponent implements OnInit {

  constructor(private adminService: AdminService,
    private snackbarService: SnackbarService,
    private loginService: LoginService) { }

  public action : string;

  public id: string;

  public username: string;

  public email: string;
 
  public password: string;

  public isLoggedIn: boolean = false;

  ngOnInit() {
    this.id = window.history.state.id;
    this.username = window.history.state.username;
    this.action = window.history.state.action;
    this.email = window.history.state.email;
    this.isLoggedIn = window.history.state.isLoggedIn;
  }

  public Submit(adminForm: NgForm){
    if(this.action == 'Create'){
      this.Create(adminForm);
    }
    else{
      this.Edit(adminForm)
    }
  }

  public Create(adminForm: NgForm) {
    let admin: Admin = {
      id: '',
      username: '',
      email: '',
      password: ''
    };
    admin.username = adminForm.value.username;
    admin.email = adminForm.value.email;
    admin.password = adminForm.value.password;
    this.adminService.CreateAdmin(admin)
    .subscribe(
      (response) => {
        this.snackbarService.notifications$.next({
          message: response,
          action: 'Success!',
          config: Object.assign({}, {duration:3000}, this.snackbarService.configSuccess)
        });
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

  public Edit(adminForm: NgForm) {
    let admin: Admin = {
      id: '',
      username: '',
      email: '',
      password: ''
    };
    admin.id = this.id;
    admin.username = adminForm.value.username;
    admin.email = adminForm.value.email;
    admin.password = adminForm.value.password;
    this.adminService.UpdateAdmin(admin)
    .subscribe(
      (response) => {
        this.snackbarService.notifications$.next({
          message: response,
          action: 'Success!',
          config: Object.assign({}, {duration:3000}, this.snackbarService.configSuccess)
        });
        if(this.isLoggedIn){
          this.loginService.setAdminUsername(admin.username);
          localStorage.setItem('username', admin.username);
          localStorage.setItem("email", admin.email);
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

}
