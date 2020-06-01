import { Session } from '../models/models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { environment } from './../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private isAuthenticated = new BehaviorSubject<boolean>(false);
  private email = new BehaviorSubject<string>("");
  public url: string = environment.apiUrl;
  private readonly loginEndpoint = this.url +'api/Session';
  private session: Session;

  constructor(private http: HttpClient) { }

  public Login(model : Session)
  {
    return this.http.post(this.loginEndpoint, model, {
      responseType: 'text'
    });
  }

  authenticateUser(response : string){
    this.session = JSON.parse(response);
    localStorage.setItem("token", this.session.token);
    localStorage.setItem("email", this.session.email);
    this.isAuthenticated.next(true);
    this.email.next(this.session.email);
  }

  get getIsAuthenticated(){
    return this.isAuthenticated; 
  }

  get getEmail(){
    return this.email; 
  }

}