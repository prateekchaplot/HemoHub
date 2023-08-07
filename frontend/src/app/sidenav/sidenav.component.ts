import { Component, Input, TemplateRef } from '@angular/core';
import { AppService } from '../services/app.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent {  
  constructor(public appService: AppService) {
  }
}
