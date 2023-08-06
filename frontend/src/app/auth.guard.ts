import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

const isLoggedIn = false;

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  if (!isLoggedIn) {
    router.navigate(['/login'])
  }

  return true;
};

export const loginGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  if (isLoggedIn) {
    router.navigate(['/home'])
  }

  return true;
};