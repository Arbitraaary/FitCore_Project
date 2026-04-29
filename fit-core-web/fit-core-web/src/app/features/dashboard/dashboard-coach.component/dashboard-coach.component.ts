import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { MatCard } from '@angular/material/card';
import { MatDivider } from '@angular/material/list';
import { MatIcon } from '@angular/material/icon';
import { AuthService } from '../../../core/services/auth.service';
import { CoachService } from '../../../core/services/coach.service';
import {
  GroupTrainingSession,
  PersonalTrainingSession,
} from '../../../core/models/types';
import { RouterLink } from '@angular/router';
import { SessionService } from '../../../core/services/session.service';

@Component({
  selector: 'app-dashboard-coach.component',
  imports: [DatePipe, MatCard, MatDivider, MatIcon, RouterLink],
  templateUrl: './dashboard-coach.component.html',
  styleUrl: './dashboard-coach.component.scss',
})
export class DashboardCoachComponent implements OnInit {
  auth = inject(AuthService);
  coachSvc = inject(CoachService);
  sessionSvc = inject(SessionService);
  user = computed(() => this.coachSvc.getMe());
  location = computed(() => this.coachSvc.getMyLocation());
  personal = signal<PersonalTrainingSession[]>([]);
  group = signal<GroupTrainingSession[]>([]);
  upcomingSessions = signal<(PersonalTrainingSession | GroupTrainingSession)[]>([]);

  ngOnInit() {
    if (this.user() !== null) {
      this.sessionSvc.getPersonalByCoachId(this.user()!.id).subscribe({
        next: (result) => {
          this.personal.set(result);
          this.upcomingSessions.set(this._computeUpcomingSessions());
        },
        error: (error) => {
          console.log(error);
        },
      });

      this.sessionSvc.getGroupByCoachId(this.user()!.id).subscribe({
        next: (result) => {
          this.group.set(result);
          this.upcomingSessions.set(this._computeUpcomingSessions());
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }

  greeting = computed(() => {
    const h = new Date().getHours();
    if (h < 12) return 'Good morning';
    if (h < 17) return 'Good afternoon';
    return 'Good evening';
  });

  private _computeUpcomingSessions() {
    const today = new Date();
    const todayStr = today.toDateString();
    return [...this.personal(), ...this.group()]
      .filter((s) => new Date(s.startTime).toDateString() === todayStr)
      .sort((a, b) => new Date(a.startTime).getTime() - new Date(b.startTime).getTime());
  }

  quickLinks = computed(() => [
    { label: 'My Calendar', icon: 'event', route: `/coach-calendar/${this.user()?.id}` },
  ]);
}
