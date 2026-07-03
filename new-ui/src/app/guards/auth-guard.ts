// import { CanActivateFn } from '@angular/router';

// export const authGuard: CanActivateFn = (route, state) => {
//   return true;
// };

import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const snackBar = inject(MatSnackBar);
  const token = localStorage.getItem('token');

  if (token) {
    return true;
  }

  const initialCheckKey = 'auth-guard-initial-check';
  const hasCompletedInitialCheck = sessionStorage.getItem(initialCheckKey) === 'done';

  if (!hasCompletedInitialCheck) {
    sessionStorage.setItem(initialCheckKey, 'done');
    router.navigate(['/login']);
    return false;
  }

  snackBar.open('Please log in first', 'Close', {
    duration: 4000,
    horizontalPosition: 'center',
    verticalPosition: 'top',
    panelClass: ['error-snackbar']
  });

  router.navigate(['/login']);
  return false;
};
