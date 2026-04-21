import { Routes } from '@angular/router';
import { authGuard, guestGuard, managerGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    canActivate: [guestGuard],
    loadComponent: () =>
      import('./features/auth/login-component/login.component').then((m) => m.LoginComponent),
  },
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./core/layout/shell.component/shell.component').then((m) => m.ShellComponent),
    children: [
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/dashboard.component').then((m) => m.DashboardComponent),
      },
      // ── Manager-only routes ──────────────────────────────────────────────
      {
        path: 'coaches',
        canActivate: [managerGuard],
        loadComponent: () =>
          import('./features/coaches/coaches-list.component/coaches-list.component').then(
            (m) => m.CoachesListComponent,
          ),
      },
      {
        path: 'coaches/register',
        canActivate: [managerGuard],
        loadComponent: () =>
          import('./features/coaches/coach-register.component/coach-register.component').then(
            (m) => m.CoachRegisterComponent,
          ),
      },
      {
        path: 'coaches/:id',
        canActivate: [managerGuard],
        loadComponent: () =>
          import('./features/coaches/coach-detail.component/coach-detail.component').then(
            (m) => m.CoachDetailComponent,
          ),
      },
      {
        path: 'clients',
        canActivate: [managerGuard],
        loadComponent: () =>
          import('./features/clients/clients-list.component/clients-list.component').then(
            (m) => m.ClientsListComponent,
          ),
      },
      {
        path: 'clients/register',
        canActivate: [managerGuard],
        loadComponent: () =>
          import('./features/clients/client-register.component/client-register.component').then(
            (m) => m.ClientRegisterComponent,
          ),
      },
      {
        path: 'clients/:id',
        canActivate: [managerGuard],
        loadComponent: () =>
          import('./features/clients/client-detail.component/client-detail.component').then(
            (m) => m.ClientDetailComponent,
          ),
      },
      // {
      //   path: 'group-calendar',
      //   canActivate: [managerGuard],
      //   loadComponent: () =>
      //     import('./features/calendar/group-calendar/group-calendar.component').then(
      //       (m) => m.GroupCalendarComponent,
      //     ),
      // },
      // {
      //   path: 'equipment',
      //   canActivate: [managerGuard],
      //   loadComponent: () =>
      //     import('./features/equipment/equipment.component').then((m) => m.EquipmentComponent),
      // },
      // {
      //   path: 'analytics',
      //   canActivate: [managerGuard],
      //   loadComponent: () =>
      //     import('./features/analytics/analytics.component').then((m) => m.AnalyticsComponent),
      // },
      // // ── Coach routes (also accessible by manager) ────────────────────────
      // {
      //   path: 'coach-calendar/:coachId',
      //   loadComponent: () =>
      //     import('./features/calendar/coach-calendar/coach-calendar.component').then(
      //       (m) => m.CoachCalendarComponent,
      //     ),
      // },
      // ─── Default redirect ────────────────────────────────────────────────
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    ],
  },
  { path: '**', redirectTo: 'dashboard' },
];
