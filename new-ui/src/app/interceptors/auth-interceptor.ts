import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, finalize } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { NotificationService } from '../services/notification';

export const authInterceptor: HttpInterceptorFn = (

  req,
  next

) => {

  const router = inject(Router);
  const loadingService = inject(LoadingService);
  const notification = inject(NotificationService);

  // Skipping authentication endpoints

  if (
    req.url.includes('/login') ||
    req.url.includes('/register')
  ) {

    return next(req);
  }

  // Reading JWT from storage

  const token =
    localStorage.getItem(
      'token');

  // Attach token if available

  if (token) {

    req = req.clone({

      setHeaders: {

        Authorization:
          `Bearer ${token}`
      }
    });
  }

  // Logging the request

  console.log(

    `[HTTP] ${req.method} ${req.url}`

  );

  const started =
    Date.now();

  loadingService.show();

  return next(req).pipe(

    catchError(error => {

      console.error(

        `[HTTP ERROR] ${req.method} ${req.url}`,
        error

      );

      switch (error.status) {

        case 400:
          const badRequestMessage =
            error?.error?.message ||
            'Invalid request.';

          notification.error(badRequestMessage);

          break;

        case 401:

          notification.error('Your session has expired. Please log in again.');

          localStorage.removeItem(
            'token');

          router.navigate(
            ['/login']);

          break;

        case 403:

          notification.error('You are not authorized to perform this action.');

          break;

        case 404:

          notification.error('Requested resource was not found.');

          break;

        case 500:

          notification.error('An internal server error occurred.');

          break;

        default:

          notification.error('An unexpected error occurred.');
      }

      return throwError(
        () => error);

    }),
        
    finalize(() => {

      loadingService.hide();

    }),
  );
};
