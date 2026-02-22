
import { Routes } from '@angular/router';

export const AUTH_ROUTES: Routes = [
  {
    path: '',
    children: [
      { path: 'login', loadComponent: () => import('./login/login').then(c => c.Login) },
      { path: 'register', loadComponent: () => import('./register/register').then(c => c.Register) },
      { path: 'forgot-password', loadComponent: () => import('./forget-password/forget-password').then(c => c.ForgetPassword) },
      { path: 'reset-password', loadComponent: () => import('./reset-password/reset-password').then(c => c.ResetPassword) },
      { path: '', redirectTo: 'login', pathMatch: 'full' }
    ]
  }
];