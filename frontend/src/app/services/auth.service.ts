import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5165';

  constructor(private http: HttpClient) {}

  register(email: string, password: string): Observable<any> {
    const requestBody = { email, password };
    return this.http.post(`${this.apiUrl}/api/auth/register`, requestBody);
  }

  login(email: string, password: string): Observable<any> {
    const requestBody = { email, password };
    return this.http.post(`${this.apiUrl}/api/auth/login`, requestBody);
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  decodeUser(): User | null {
    let token = localStorage.getItem('token');
    if (!token) {
      return null;
    }

    let decodedToken = jwt_decode(token);
    return decodedToken as User;
  }
}
