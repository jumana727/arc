import { Routes } from '@angular/router';
import { authRoutes } from './layouts/auth/auth.routes';
import { applicationRoutes } from './layouts/application/application.routes';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  ...applicationRoutes,
  ...authRoutes,
];
