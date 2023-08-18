import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router, titleService: Title) {
    titleService.setTitle('Login - Hemohub');
  }

  onLogin() {
    this.authService.login(this.email, this.password).subscribe(
      (response: any) => {
        const token = response.token;
        if (token) {
          this.authService.setToken(token);
          this.router.navigate(['/home']);
        }
      });
  }
}
