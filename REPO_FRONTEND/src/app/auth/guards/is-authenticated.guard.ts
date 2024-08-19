import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { map, tap, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { AuthStatus } from '../models/auth-status.enum';

export const isAuthenticatedGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.authStatus() === AuthStatus.authenticated) return true;
  
  router.navigate(['/auth/login']);
  return false;
};
