import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ReportRequest, TypeSummary, RequestSummary } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  public url: string = environment.apiUrl;
  private readonly endpoint = this.url + 'api/Administrator';
  
  constructor(private http: HttpClient) { }

  getTypeReport(request: ReportRequest){
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    let params = new HttpParams();
    params.append("body", JSON.stringify(request));

    return this.http.get<TypeSummary[]>(`${this.endpoint}/TypesSummary/`, {
      headers: headers,
      params: params
    });
  }
  getRequestReport(request: ReportRequest){
    var token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers
      .set('Content-Type', 'application/json')
      .set('Authorization', token);

    let params = new HttpParams();
    params.append("body", JSON.stringify(request));

    return this.http.get<RequestSummary[]>(`${this.endpoint}/RequestSummary/`, {
      headers: headers,
      params: params
    });
  }
}
