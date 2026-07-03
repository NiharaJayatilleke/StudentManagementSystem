import { Component, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from './shared/loader/loader';

@Component({
  selector: 'app-root',
  imports: [RouterLink, RouterOutlet,   LoaderComponent, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('student-management-ui');
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  logout() {

    this.authService.logout();

    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
  return this.authService.isLoggedIn();
}
}