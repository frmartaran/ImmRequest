import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImporterService {

  public url: string = environment.apiUrl;
  private readonly endpoint = this.url + 'api/Importer';

  constructor(private http: HttpClient) { }

  getImporters() {
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    return this.http.get<string[]>(this.endpoint, {
      headers: headers,
    });
  }

  import(importer: string, file: File) {
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'multipart/form-data')
      .set('Authorization', token);

    let form = { importer: importer, file: file };
    return this.http.post(this.endpoint, form, {
      headers: headers,
      responseType: "text"
    });
  }
}
