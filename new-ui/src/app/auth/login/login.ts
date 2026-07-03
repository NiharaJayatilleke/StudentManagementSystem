import { Component, inject, PLATFORM_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink,FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {

  username = '';
  password = '';
  errorMessage = '';
  showPassword = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  login() {

    this.authService
      .login(
        this.username,
        this.password
      )
      .subscribe({

        next: result => {

          localStorage.setItem(
            'token',
            result.token);

          this.router.navigate(
            ['/students']);
        },

        error: () => {

          this.errorMessage =
            'Invalid username or password';
        }
      });
  }

  togglePassword() {

    this.showPassword =
      !this.showPassword;
  }
}