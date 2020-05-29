import { SnackbarService } from './../../services/snackbar.service';
import { Observable, Subject } from 'rxjs';
import { LoginService } from 'src/app/services/login.service';
import { Component } from '@angular/core';
import { SnackbarInput } from 'src/app/models/models';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'ImmRequest';
  isAuthenticated$: Observable<boolean>;

  constructor(private loginService: LoginService, private snackbarService: SnackbarService, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.isAuthenticated$ = this.loginService.getIsAuthenticated;
    this.initializeSnackbar();
  }

  initializeSnackbar(){
    this.snackbarService.notifications$.subscribe(input => {
      this.snackBar.open(input.message, input.action, 
        {duration: 3000}
      );
    });
  }  
}
