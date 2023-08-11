import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';
import { User } from '../models/user';
import { ApiService } from './api.service';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private apiService: ApiService, private storageService: StorageService) {}

  register(user: any): Observable<any> {
    return this.apiService.register(user);
  }

  login(email: string, password: string): Observable<any> {
    return this.apiService.login({ email, password });
  }

  isAuthenticated(): boolean {
    const token = this.storageService.getToken();
    return !!token;
  }

  logout() {
    this.storageService.removeToken();
  }

  setToken(token: string) {
    this.storageService.setToken(token);
  }

  decodeUser(): User | null {
    let token = this.storageService.getToken();
    if (!token) {
      return null;
    }

    let decodedToken = jwt_decode(token);
    return decodedToken as User;
  }
}
