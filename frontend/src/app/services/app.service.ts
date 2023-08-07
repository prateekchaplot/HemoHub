import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  user: User | null = null;

  constructor(private authService: AuthService) { }

  setUser() {
    this.user = this.authService.decodeUser();
  }
}
