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
  name: string = '';
  mobile: string = '';
  selectedBloodGroup: string = '';
  streetAddress: string = '';
  city: string = '';
  state: string = '';
  country: string = '';

  bloodGroups = ["A-", "A+", "B-", "B+", "AB-", "AB+", "O+", "O-"];

  constructor(private authService: AuthService, private router: Router) {}

  onRegister(): void {
    const user = {
      name: this.name,
      email: this.email,
      password: this.password,
      mobile: this.mobile,
      bloodGroup: this.selectedBloodGroup,
      address: {
        streetAddress: this.streetAddress,
        city: this.city,
        state: this.state,
        country: this.country
      }
    };

    this.authService.register(user).subscribe(
      (response: any) => {
        const token = response.token;
        if (token) {
          this.authService.setToken(token);
          this.router.navigate(['/home']);
        }
      });;
  }
}
