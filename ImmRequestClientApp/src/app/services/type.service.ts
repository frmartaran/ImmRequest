import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TopicType } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class TypeService {

  public url: string = environment.apiUrl;
  private readonly endpoint = this.url + 'api/Type';

  constructor(private http: HttpClient) { }

  createType(parentTopicId: number, type: TopicType) {
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    return this.http.post(this.endpoint + "/" + parentTopicId + '/', type, {
      headers: headers,
      responseType: 'text'
    });
  }

  updateType(type: TopicType) {
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    return this.http.put(this.endpoint + "/" + type.id + '/', type, {
      headers: headers,
      responseType: 'text'
    });
  }

  deleteType(id: number) {
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    return this.http.delete(this.endpoint + "/" + id + '/', {
      headers: headers,
      responseType: 'text'
    });
  }

  getAll(parentTopicId: number){
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json');

    return this.http.get<TopicType[]>(this.endpoint + "/All/" + parentTopicId, {
      headers: headers,
      responseType: 'json'
    });
  }

  get(id: number){
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json');

    return this.http.get(this.endpoint + "/" + id + '/', {
      headers: headers,
      responseType: 'json'
    });
  }
}
