import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { SidenavService } from './services/sidenav.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  @ViewChild('sidenav') sidenav: MatSidenav | null = null;
  
  sidemenu = [
    {
      'id': 1,
      'label': 'Home',
      'path': '/home',
      'icon': 'home'
    },
    {
      'id': 2,
      'label': 'Search',
      'path': '/home',
      'icon': 'search'
    },
    {
      'id': 3,
      'label': 'Exchange',
      'path': '/home',
      'icon': 'swap_horiz'
    },
    {
      'id': 4,
      'label': 'Requests',
      'path': '/home',
      'icon': 'request_quote'
    },
    {
      'id': 5,
      'label': 'Analytics',
      'path': '/home',
      'icon': 'bar_chart'
    }
  ]

  constructor(private sidenavService: SidenavService, private authService: AuthService) {
  }
  
  ngAfterViewInit(): void {
    this.sidenavService.setSidenav(this.sidenav);
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
}
