import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { authState } from './application/auth.state';
import { log } from 'node:console';

debugger;
export const authGuard: CanActivateFn = () => {
  const platformId = inject(PLATFORM_ID);
  const router = inject(Router);

  if (!isPlatformBrowser(platformId)) {
    return true; 
  }
   return authState.isAuthenticated() ? true : router.navigate(['/login']);  
};