import { Injectable } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

@Injectable({
  providedIn: 'root'
})
export class SidenavService {
  private sidenav: MatSidenav | null = null;
  
  setSidenav(sidenav: MatSidenav | null) {
    this.sidenav = sidenav;
  }

  toggle() {
    this.sidenav?.toggle();
  }
}
