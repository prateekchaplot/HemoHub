import { Component } from '@angular/core';
import { AppService } from '../../services/app.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private appService: AppService, titleService: Title) {
    this.appService.setUser();
    titleService.setTitle('Home - Hemohub');
  }
}
