import { LoginService } from 'src/app/services/login.service';
import { Component, OnInit,} from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Session } from 'src/app/models/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent implements OnInit {

  constructor(private router: Router,
    private loginService: LoginService) { }

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
      },
      (error) => {
        this.authenticationError = error.error
          ? error.error 
          : error.message;
      },
      () => {
        this.router.navigate(['/home-page']);
      }
    )
  }
}