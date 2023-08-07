import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AppService } from '../services/app.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  
  constructor(
    private authService: AuthService,
    public appService: AppService,
    private router: Router) {
  }

  onLogout() {
    this.authService.logout();
    this.appService.setUser();
    this.router.navigate(['/login'])
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
}
