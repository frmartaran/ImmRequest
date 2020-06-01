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
  public email$ = new Observable<string>();
  public isAuthenticated$: Observable<boolean>;

  constructor(private logoutService: LogoutService,
    public dialog: MatDialog,
    private snackbarService : SnackbarService,
    private router: Router,
    private loginService: LoginService) { }

  ngOnInit() {
    this.isAuthenticated$ = this.loginService.getIsAuthenticated;
    this.email$ = this.loginService.getEmail;
  }

  logoutDialog(){
    let dialogRef = this.dialog.open(ConfirmationComponent, {
      data: {elementDialogText : "Are you sure you want to log out?", elementDialogTitle: "Logout"}
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
            localStorage.removeItem('token');
            localStorage.removeItem('email');
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
}