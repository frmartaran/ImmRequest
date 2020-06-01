import { SnackbarService } from './../../services/snackbar.service';
import { LoginService } from 'src/app/services/login.service';
import { Component } from '@angular/core';
import { MatSnackBar} from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'ImmRequest';

  constructor(private loginService: LoginService, 
    private snackbarService: SnackbarService, 
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.initializeSnackbar();
  }

  initializeSnackbar(){
    this.snackbarService.notifications$.subscribe(input => {
      this.snackBar.open(input.message, input.action, 
        Object.assign({}, {duration:3000}, input.config)
      );
    });
  }  
}
