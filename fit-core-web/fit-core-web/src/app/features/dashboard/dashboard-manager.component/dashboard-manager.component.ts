import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { MatCard } from '@angular/material/card';
import { MatDivider } from '@angular/material/list';
import { MatIcon } from '@angular/material/icon';
import { AuthService } from '../../../core/services/auth.service';
import { ManagerService } from '../../../core/services/manager.service';
import { Client, Coach, GroupTrainingSession, PersonalTrainingSession } from '../../../core/models/types';
import { RouterLink } from '@angular/router';
import { SessionService } from '../../../core/services/session.service';
import { CoachService } from '../../../core/services/coach.service';
import { ClientService } from '../../../core/services/client.service';
import { MOCK_EQUIPMENT } from '../../../core/data/mock.data';

export interface StatValues {
  label: string;
  value: any;
  icon: string;
  color: string;
  route: string;
  visible: boolean;
}

@Component({
  selector: 'app-dashboard-manager.component',
  imports: [DatePipe, MatCard, MatDivider, MatIcon, RouterLink],
  templateUrl: './dashboard-manager.component.html',
  styleUrl: './dashboard-manager.component.scss',
})
export class DashboardManagerComponent implements OnInit {
  auth = inject(AuthService);
  managerSvc = inject(ManagerService);
  coachSvc = inject(CoachService);
  clientSvc = inject(ClientService);
  sessionSvc = inject(SessionService);
  user = computed(() => this.managerSvc.getMe());
  location = computed(() => this.managerSvc.getMyLocation());
  coaches = signal<Coach[]>([]);
  clients = signal<Client[]>([]);
  personal = signal<PersonalTrainingSession[]>([]);
  group = signal<GroupTrainingSession[]>([]);
  upcomingSessions = signal<(PersonalTrainingSession | GroupTrainingSession)[]>([]);
  stats = signal<StatValues[]>([]);

  ngOnInit() {
    if (this.user() === null) return;

    this.sessionSvc.getPersonalByManager(this.user()!.id).subscribe({
      next: (result) => {
        this.personal.set(result);
        this.stats.set(this.computeStats());
        this.upcomingSessions.set(this.computeUpcomingSessions());
      },
      error: (error) => {
        console.log(error);
      },
    });

    this.sessionSvc.getGroupByManager(this.user()!.id).subscribe({
      next: (result) => {
        this.group.set(result);
        this.upcomingSessions.set(this.computeUpcomingSessions());
        this.stats.set(this.computeStats());
      },
      error: (error) => console.log(error)
    });

    this.clientSvc.getAllAndMembership().subscribe({
      next: (result) => {
        this.clients.set(result);
        this.stats.set(this.computeStats());
      },
      error: (error) => console.log(error)
    })

    this.managerSvc.getMyCoaches(this.user()!.id).subscribe({
      next: (result) => {
        this.coaches.set(result);
        this.stats.set(this.computeStats());
      },
      error: (error) => console.log(error)
    })

  }

  greeting = computed(() => {
    const h = new Date().getHours();
    if (h < 12) return 'Good morning';
    if (h < 17) return 'Good afternoon';
    return 'Good evening';
  });

  // ── Manager stats ──────────────────────────────────────────────────────────
  computeStats(){
    const activeMemberships = this.clients()
      .filter((c) => c.activeMembership !== null).length;

    const today = new Date();
    const todayStr = today.toDateString();
    const todaySessions = [...this.personal(), ...this.group()].filter(
      (s) => new Date(s.startTime).toDateString() === todayStr,
    ).length;

    const totalEquipment = MOCK_EQUIPMENT.reduce((s, e) => s + e.quantity, 0);

    return [
      {
        label: 'Total Coaches',
        value: this.coaches().length,
        icon: 'fitness_center',
        color: 'primary',
        route: '/coaches',
        visible: true,
      },
      {
        label: 'Total Clients',
        value: this.clients().length,
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
    ] as StatValues[];
  };

  // ── Upcoming sessions (next 5) ─────────────────────────────────────────────
  computeUpcomingSessions() {
    const today = new Date();
    const todayStr = today.toDateString();
    return [...this.personal(), ...this.group()]
      .filter((s) => new Date(s.startTime).toDateString() === todayStr)
      .sort((a, b) =>
        new Date(a.startTime).getTime() - new Date(b.startTime).getTime());
  }

  quickLinks = computed(() =>
    [
      { label: 'Register Coach', icon: 'person_add', route: '/coaches/register' },
      { label: 'Register Client', icon: 'group_add', route: '/clients/register' },
      { label: 'Group Calendar', icon: 'calendar_month', route: '/group-calendar' },
      { label: 'Equipment', icon: 'sports_gymnastics', route: '/equipment' },
    ]
  );
}
