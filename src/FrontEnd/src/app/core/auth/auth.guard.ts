import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { authState } from './application/auth.state';

export const authGuard: CanActivateFn = () => {
  const platformId = inject(PLATFORM_ID);
  const router = inject(Router);

  if (!isPlatformBrowser(platformId)) {
    return true; 
  }

  const loggedIn = authState.isAuthenticated();
  if (!loggedIn) {
    return router.parseUrl('/login');
  }

  return true;
};