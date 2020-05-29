import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class LogoutService {

  public url: string = environment.apiUrl;
  private readonly logoutEndpoint = this.url +'api/Session';

  constructor(private http: HttpClient) { }

  public Logout()
  {
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
    .set('Content-Type', 'application/json')
    .set('Authorization', token);
    return this.http.delete(this.logoutEndpoint, {
      headers: headers,
      responseType: 'text'
    });
  }
}