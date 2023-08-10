import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AppService } from '../services/app.service';
import { SidenavService } from '../services/sidenav.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {  
  constructor(
    public appService: AppService,
    private authService: AuthService,
    private sidenavService: SidenavService,
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

  onToggle() {
    this.sidenavService.toggle();
  }
}
