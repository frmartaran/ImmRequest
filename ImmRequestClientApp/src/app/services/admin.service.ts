import { Injectable } from '@angular/core';
import { Admin } from '../models/models';
import { environment } from 'src/environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
   providedIn: 'root'
 })

 export class AdminService {
    public url: string = environment.apiUrl;
    private readonly endpoint = this.url + 'api/Administrator';

    constructor(private http: HttpClient) { }

    public CreateAdmin(admin: Admin) {
        var adminToken = localStorage.getItem('token');
        let headers = new HttpHeaders();
        headers = headers
        .set('Content-Type', 'application/json')
        .set('Authorization', adminToken);
    
        return this.http.post(this.endpoint, admin, {
          headers: headers,
          responseType: 'text'
        });
    }

    public UpdateAdmin(admin: Admin) {
        var adminToken = localStorage.getItem('token');
        let headers = new HttpHeaders();
        headers = headers
        .set('Content-Type', 'application/json')
        .set('Authorization', adminToken);
    
        return this.http.put(this.endpoint + '/' + admin.id + '/', admin, {
          headers: headers,
          responseType: 'text'
        });
    }

    public GetAll() {
      var adminToken = localStorage.getItem('token');
      let headers = new HttpHeaders();
      headers = headers
        .set('Content-Type', 'application/json')
        .set('Authorization', adminToken);
  
      return this.http.get(this.endpoint, {
        headers: headers,
      });
    }

    public DeleteAdmin(admin: Admin) {
      var adminToken = localStorage.getItem('token');
      let headers = new HttpHeaders();
      headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', adminToken);
  
      return this.http.delete(this.endpoint + '/' + admin.id , {
        headers: headers,
        responseType: 'text'
      });
  }
 }