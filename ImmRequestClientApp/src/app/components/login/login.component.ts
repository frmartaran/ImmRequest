import { LoginService } from 'src/app/services/login.service';
import { Component, OnInit,} from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Session } from 'src/app/models/models';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { HtmlHelpers } from '../../helpers/html.helper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent implements OnInit {

  constructor(private router: Router,
    private loginService: LoginService,
    private snackbarService : SnackbarService) { }

  public credentials: Session = {
    email: '',
    password: ''
  };
  public authenticationError: string = "";

  public user: any = {};

  ngOnInit() {
  }

  public Login(loginForm: NgForm) {
    this.credentials.email = loginForm.value.email;
    this.credentials.password = loginForm.value.password;
    this.loginService.Login(this.credentials)
    .subscribe(
      (response) => {
        this.loginService.authenticateUser(response);
        this.snackbarService.notifications$.next({
          message: "You are now logged in!",
          action: 'Success!',
          config: this.snackbarService.configSuccess
        });
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackbarService.configError
        });
      },
      () => {
        this.router.navigate(['/home-page']);
      }
    )
  }
}