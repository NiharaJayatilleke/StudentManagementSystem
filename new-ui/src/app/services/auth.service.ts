import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl =
    'http://localhost:5000/api/auth';

  constructor(
    private http: HttpClient
  ) { }

  register(
    username: string,
    password: string
  ) {

    return this.http.post(
      `${this.apiUrl}/register`,
      {
        username,
        password
      });
  }

  login(
    username: string,
    password: string
  ): Observable<any> {

    return this.http.post(
      `${this.apiUrl}/login`,
      {
        username,
        password
      });
  }

  logout() {

    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {

    return !!localStorage.getItem('token');
  }
}