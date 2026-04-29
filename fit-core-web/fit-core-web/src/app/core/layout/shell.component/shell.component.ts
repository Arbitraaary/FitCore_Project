import { Component, computed, signal, inject, OnInit } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthService } from '../../services/auth.service';
import { UserType } from '../../models/types';
import { UserService } from '../../services/user.service';

interface NavItem {
  label: string;
  icon: string;
  route: string;
  roles: UserType[];
}

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatDividerModule,
    MatTooltipModule,
  ],
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss'],
})
export class ShellComponent implements OnInit {
  userSvc = inject(UserService);
  auth = inject(AuthService);
  sidenavOpen = signal(true);
  user = this.userSvc.currentUser;
  role = computed(() => this.user()?.userType ?? 'coach');
  nav_items : NavItem[] = [];
  ngOnInit(): void {
    this.nav_items = [
      {
        label: 'Dashboard',
        icon: 'dashboard',
        route: `/dashboard/${this.role()}`,
        roles: ['manager', 'coach'],
      },
      { label: 'Coaches', icon: 'fitness_center', route: '/coaches', roles: ['manager'] },
      { label: 'Clients', icon: 'group', route: '/clients', roles: ['manager'] },
      {
        label: 'Group Calendar',
        icon: 'calendar_month',
        route: '/group-calendar',
        roles: ['manager'],
      },
      { label: 'My Calendar', icon: 'event', route: '/coach-calendar/', roles: ['coach'] },
      { label: 'Equipment', icon: 'sports_gymnastics', route: '/equipment', roles: ['manager'] },
      { label: 'Analytics', icon: 'bar_chart', route: '/analytics', roles: ['manager'] },
    ];
  }

  initials = computed(() => {
    const u = this.user();
    return u ? `${u.firstName[0]}${u.lastName[0]}`.toUpperCase() : '?';
  });
  fullName = computed(() => {
    const u = this.user();
    return u ? `${u.firstName} ${u.lastName}` : '';
  });

  navItems = computed(() => {
    const r = this.role();
    return this.nav_items.filter((n) => n.roles.includes(r)).map((n) => ({
      ...n,
      route: n.route === '/coach-calendar/' ? `/coach-calendar/${this.user()?.id ?? ''}` : n.route,
    }));
  });

  toggleSidenav() {
    this.sidenavOpen.update((v) => !v);
  }

  logout() {
    this.auth.logout();
  }
}
