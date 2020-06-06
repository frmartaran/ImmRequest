import { LoginService } from './../../services/login.service';
import { SnackbarService } from './../../services/snackbar.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ConfirmationComponent } from 'src/app/modals/confirmation/confirmation.component';
import { LogoutService } from 'src/app/services/logout.service';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { HtmlHelpers } from 'src/app/helpers/html.helper';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public imageUrl = "assets/images/logoIM-home.png";
  public adminUsername$ = new Observable<string>();
  public isAuthenticated$: Observable<boolean>;

  constructor(private logoutService: LogoutService,
    public dialog: MatDialog,
    private snackbarService : SnackbarService,
    private router: Router,
    private loginService: LoginService) { }

  ngOnInit() {
    this.isAuthenticated$ = this.loginService.getIsAuthenticated;
    this.adminUsername$ = this.loginService.getAdminUsername;
  }

  logoutDialog(){
    let dialogRef = this.dialog.open(ConfirmationComponent, {
      data: {
        elementDialogText : "Are you sure you want to log out?", 
        elementDialogTitle: "Logout"}
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.logoutService.Logout().subscribe(
          (response) => {
            this.snackbarService.notifications$.next({
              message: "You have been logged out!",
              action: 'Success!',
              config: this.snackbarService.configSuccess
            });
            localStorage.removeItem('username');
            localStorage.removeItem('token');
            localStorage.removeItem('email');
            localStorage.removeItem('id');
            window.location.href = '/login';
            window.location.reload();
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

  login(){
    this.router.navigate(['/login'])
  }

  homePage(){
    this.router.navigate(['/home-page'])
  }

  createAdmin(){
    this.router.navigateByUrl('/', {skipLocationChange: true})
    .then(()=> this.router.navigate(['/modify-admin'], {state: {action: 'Create'}}));
  }

  editAdmin(){
    let idToSend = localStorage.getItem('id');
    let emailToSend = localStorage.getItem('email');
    let usernameToSend = localStorage.getItem('username');
    this.router.navigateByUrl('/', {skipLocationChange: true})
    .then(()=> this.router.navigate(['/modify-admin'], {state: 
      {
        action: 'Edit', 
        id: idToSend, 
        email: emailToSend, 
        username: usernameToSend, 
        isLoggedIn: true}
      }));
  }
}