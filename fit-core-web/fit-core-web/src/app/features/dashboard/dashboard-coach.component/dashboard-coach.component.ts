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
  personal = signal<PersonalTrainingSession[] | null>(null);
  group = signal<GroupTrainingSession[] | null>(null);
  upcomingSessions = signal<(PersonalTrainingSession | GroupTrainingSession)[]>([])

  ngOnInit() {
    if(this.user() !== null){
      this.sessionSvc.getPersonalByCoachId(this.user()!.id).subscribe({
        next: result => {
          this.personal.set(result)
          this.upcomingSessions.set(this.computeUpcomingSessions());
        },
        error: error => {
          console.log(error);
        }
      });

      this.sessionSvc.getGroupByCoachId(this.user()!.id).subscribe({
        next: (result) => {
          this.group.set(result);
          this.upcomingSessions.set(this.computeUpcomingSessions());
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

  computeUpcomingSessions(){
    const now = new Date();
    return [...this.personal() ?? [], ...this.group() ?? []]
      .filter((s) => s.startTime >= now)
      .sort((a, b) => a.startTime.getTime() - b.startTime.getTime())
      .slice(0, 5);
  }

  quickLinks = computed(() => [
    { label: 'My Calendar', icon: 'event', route: `/coach-calendar/${this.user()?.id}` },
  ]);
}
