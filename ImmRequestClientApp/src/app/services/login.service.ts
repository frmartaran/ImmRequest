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
  private adminUsername = new BehaviorSubject<string>("");
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
    localStorage.setItem('username', this.session.username);
    localStorage.setItem("id", this.session.id.toString());
    localStorage.setItem("token", this.session.token);
    localStorage.setItem("email", this.session.email);
    this.isAuthenticated.next(true);
    this.adminUsername.next(this.session.username);
  }

  get getIsAuthenticated(){
    return this.isAuthenticated; 
  }

  get getAdminUsername(){
    return this.adminUsername; 
  }

  setAdminUsername(username:string){
    this.adminUsername.next(username);
  }

}