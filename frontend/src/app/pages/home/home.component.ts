import { Component } from '@angular/core';
import { AppService } from '../../services/app.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private appService: AppService) {
    this.appService.setUser();
  }
}
