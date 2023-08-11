import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  baseUrl = 'http://localhost:5165';
  
  constructor(private http: HttpClient, private storageService: StorageService) { }

  /* Auth Endpoints */
  login(body: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/auth/login`, body);
  }

  register(body: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/auth/register`, body);
  }

  /* User Endpoints */
  fetchStudentIdAndNames(text: string): Observable<any> {
    const headers = this.getHeaders();
    return this.http.get(`${this.baseUrl}/api/user/getstudentidandnames?searchText=${text}`, { headers });
  }

  /* Utility Methods */
  private getHeaders(): HttpHeaders {
    const token = this.storageService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
}
