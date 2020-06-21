import { CitizenRequest } from './../models/models';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Area, Topic } from '../models/models';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class ManagementService {

  public url: string = environment.apiUrl;
  private readonly endpoint = this.url + 'api/CitizenRequest';
  
  constructor(private http: HttpClient) { }

  getAllAreas(){
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json');

    return this.http.get<Area[]>(`${this.endpoint}/Areas/`, {
      headers: headers,
    });
  }

  getAllTopicsFromArea(areaId: number){
    return this.http.get<Topic[]>(`${this.endpoint}/Topics/${areaId}/`);
  }

  createCitizenRequest(request: CitizenRequest){
    return this.http.post(this.endpoint, request, {
      responseType: 'text'
    });
  }

}
