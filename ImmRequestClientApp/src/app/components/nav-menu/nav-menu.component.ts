import { SnackbarService } from './../../services/snackbar.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LogoutComponent } from 'src/app/modals/logout/logout.component';
import { LogoutService } from 'src/app/services/logout.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public imageUrl = "assets/images/logoIM-home.png";

  constructor(private logoutService: LogoutService,
    public dialog: MatDialog,
    private snackbarService : SnackbarService) { }

  ngOnInit() {
  }

  confirmDialog(){
    let dialogRef = this.dialog.open(LogoutComponent);
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.logoutService.Logout().subscribe(
          (response) => {
            localStorage.removeItem('token');
            localStorage.removeItem('email');
            window.location.href = '/login';
            window.location.reload();
          },
          (error) => {
            this.snackbarService.notifications$.next({
              message: error.error
              ? error.error 
              : error.message,
              action: 'Error!'
            });
          }
        )
      }
    });
  }

}
