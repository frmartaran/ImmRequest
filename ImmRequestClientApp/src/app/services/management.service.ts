import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Area, Topic } from '../models/models';
import { environment } from 'src/environments/environment.prod';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManagementService {

  public url: string = environment.apiUrl;
  private readonly endpoint = this.url + 'api/CitizenRequest';
  
  constructor(private http: HttpClient) { }

  getAllAreas(): Observable<Area[]>{
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json');

    return this.http.get<Area[]>(`${this.endpoint}/Areas/`, {
      headers: headers,
    });
  }

  getAllTopicsFromArea(areaId: number) : Observable<Topic[]>{
    return this.http.get<Topic[]>(`${this.endpoint}/Topics/${areaId}/`);
  }

}
