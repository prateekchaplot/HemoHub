import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onRegister(): void {
    this.authService.register(this.email, this.password).subscribe(
      (response: any) => {
        const token = response.token;
        if (token) {
          this.authService.setToken(token);
          this.router.navigate(['/home']);
        }
      });;
  }
}
