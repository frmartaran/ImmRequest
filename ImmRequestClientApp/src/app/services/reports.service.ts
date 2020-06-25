import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ReportRequest, TypeSummary, RequestSummary } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  public url: string = environment.apiUrl;
  private readonly endpoint = this.url + 'api/Reports';
  
  constructor(private http: HttpClient) { }

  getTypeReport(request: ReportRequest){
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    return this.http.post<TypeSummary[]>(`${this.endpoint}/TypesSummary/`, request, {
      headers: headers,
    });
  }

  getRequestReport(request: ReportRequest){
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    return this.http.post<RequestSummary[]>(`${this.endpoint}/RequestSummary/`, request, {
      headers: headers,
    });
  }
}
