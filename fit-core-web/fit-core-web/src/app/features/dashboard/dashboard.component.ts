import { Component, inject, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { AuthService } from '../../core/services/auth.service';
import {
  MOCK_COACHES,
  MOCK_CLIENTS,
  MOCK_CLIENT_MEMBERSHIPS,
  MOCK_PERSONAL_SESSIONS,
  MOCK_GROUP_SESSIONS,
  MOCK_EQUIPMENT,
  MOCK_LOCATION,
} from '../../core/data/mock.data';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  auth = inject(AuthService);
  user = this.auth.currentUser;
  isManager = this.auth.isManager;

  location = MOCK_LOCATION;

  greeting = computed(() => {
    const h = new Date().getHours();
    if (h < 12) return 'Good morning';
    if (h < 17) return 'Good afternoon';
    return 'Good evening';
  });

  // ── Manager stats ──────────────────────────────────────────────────────────
  stats = computed(() => {
    const activeMemberships = MOCK_CLIENT_MEMBERSHIPS.filter((m) => m.status === 'active').length;

    const today = new Date();
    const todayStr = today.toDateString();
    const todaySessions = [...MOCK_PERSONAL_SESSIONS, ...MOCK_GROUP_SESSIONS].filter(
      (s) => new Date(s.startTime).toDateString() === todayStr,
    ).length;

    const totalEquipment = MOCK_EQUIPMENT.reduce((s, e) => s + e.quantity, 0);

    return [
      {
        label: 'Total Coaches',
        value: MOCK_COACHES.length,
        icon: 'fitness_center',
        color: 'primary',
        route: '/coaches',
        visible: true,
      },
      {
        label: 'Total Clients',
        value: MOCK_CLIENTS.length,
        icon: 'group',
        color: 'accent',
        route: '/clients',
        visible: true,
      },
      {
        label: 'Active Memberships',
        value: activeMemberships,
        icon: 'card_membership',
        color: 'success',
        route: '/clients',
        visible: true,
      },
      {
        label: "Today's Sessions",
        value: todaySessions,
        icon: 'event',
        color: 'warn',
        route: '/group-calendar',
        visible: true,
      },
      {
        label: 'Equipment Units',
        value: totalEquipment,
        icon: 'sports_gymnastics',
        color: 'neutral',
        route: '/equipment',
        visible: true,
      },
    ];
  });

  // ── Upcoming sessions (next 5) ─────────────────────────────────────────────
  upcomingSessions = computed(() => {
    const coachId = this.user()?.coachId;
    const now = new Date();

    const personal = MOCK_PERSONAL_SESSIONS.filter((s) => !coachId || s.coachId === coachId).map(
      (s) => ({
        id: s.id,
        name: s.name,
        type: 'Personal' as const,
        startTime: new Date(s.startTime),
        coachId: s.coachId,
      }),
    );

    const group = MOCK_GROUP_SESSIONS.filter((s) => !coachId || s.coachId === coachId).map((s) => ({
      id: s.id,
      name: s.name,
      type: 'Group' as const,
      startTime: new Date(s.startTime),
      coachId: s.coachId,
    }));

    return [...personal, ...group]
      .filter((s) => s.startTime >= now)
      .sort((a, b) => a.startTime.getTime() - b.startTime.getTime())
      .slice(0, 5);
  });

  coachName(coachId: string): string {
    const c = MOCK_COACHES.find((c) => c.id === coachId);
    return c ? `${c.firstName} ${c.lastName}` : '—';
  }

  quickLinks = computed(() =>
    this.isManager()
      ? [
          { label: 'Register Coach', icon: 'person_add', route: '/coaches/register' },
          { label: 'Register Client', icon: 'group_add', route: '/clients/register' },
          { label: 'Group Calendar', icon: 'calendar_month', route: '/group-calendar' },
          { label: 'Equipment', icon: 'sports_gymnastics', route: '/equipment' },
        ]
      : [{ label: 'My Calendar', icon: 'event', route: `/coach-calendar/${this.user()?.coachId}` }],
  );
}
