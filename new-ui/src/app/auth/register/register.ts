import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { NotificationService } from '../../services/notification';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './register.html'
})
export class RegisterComponent {

  username = '';
  password = '';
  showPassword = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private notification: NotificationService
  ) { }

  register() {

    this.authService
      .register(
        this.username,
        this.password
      )
      .subscribe({
        next: () => {
          this.notification.success('Registration successful. Please Login to continue.');
          this.router.navigate(['/login']);
        },

        error: err => {
          const message =
            err?.error?.message ||
            'Registration failed';

          this.notification.error(message);
        }
      });
  }

  togglePassword() {

    this.showPassword =
      !this.showPassword;
  }
}