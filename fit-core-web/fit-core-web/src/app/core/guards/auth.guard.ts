import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const authGuard: CanActivateFn = () => {
  const userSvc = inject(UserService);
  const router = inject(Router);

  if (!userSvc.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }
  return true;
};

export const managerGuard: CanActivateFn = () => {
  const userSvc = inject(UserService);
  const router = inject(Router);

  if (!userSvc.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }
  if (!userSvc.isManager()) {
    router.navigate(['/dashboard']);
    return false;
  }
  return true;
};

export const coachGuard: CanActivateFn = () => {
  const userSvc = inject(UserService);
  const router = inject(Router);

  if (!userSvc.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }
  if (!userSvc.isCoach()) {
    router.navigate(['/dashboard']);
    return false;
  }
  return true;
};

export const guestGuard: CanActivateFn = () => {
  const userSvc = inject(UserService);
  const router = inject(Router);

  if (userSvc.isLoggedIn()) {
    router.navigate(['/dashboard']);
    return false;
  }
  return true;
};
