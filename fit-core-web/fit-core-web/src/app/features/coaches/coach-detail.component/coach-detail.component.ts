import { Component, computed, effect, inject, Input, input, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CoachService } from '../../../core/services/coach.service';
import { SessionService } from '../../../core/services/session.service';
import { InfoRowComponent } from '../../../shared/info-row.component/info-row.component';
import { SectionCardComponent } from '../../../shared/section-card.component/section-card.component';
import { RouterLink } from '@angular/router';
import { Coach, GroupTrainingSession, PersonalTrainingSession } from '../../../core/models/types';

@Component({
  selector: 'app-coach-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    InfoRowComponent,
    SectionCardComponent,
    RouterLink,
  ],
  templateUrl: './coach-detail.component.html',
  styleUrls: ['./coach-detail.component.scss'],
})
export class CoachDetailComponent implements OnInit {
  id = input.required<string>();
  private coachSvc = inject(CoachService);

  coach = signal<Coach | null>(null);
  loading = signal(true);
  personal = signal<PersonalTrainingSession[]>([]);
  group = signal<GroupTrainingSession[]>([]);

  ngOnInit() {
    this._updateCoach();
    this._updatePersonal();
    this._updateGroup();

    this.loading.set(false);
  }

  private _updateGroup() {
    this.sessionSvc.getGroupByCoachId(this.id()).subscribe({
      next: data => {
        this.group.set(data);
      },
      error: err => console.log(err),
    })
  }

  private _updatePersonal() {
    this.sessionSvc.getPersonalByCoachId(this.id()).subscribe({
      next: data => {
        this.personal.set(data)
      },
      error: err => console.log(err),
    })
  }

  private _updateCoach() {
    this.coachSvc.getById(this.id()).subscribe({
      next: (data) => {
        this.coach.set(data);
      },
      error: (error) => console.log(error),
    });
  }

  initials = computed(() => {
    const c = this.coach();
    return c ? `${c.firstName[0]}${c.lastName[0]}`.toUpperCase() : '?';
  });

  private sessionSvc = inject(SessionService);

  specializationIcon(spec: string): string {
    return spec === 'Swim' ? 'pool' : spec === 'Box' ? 'sports_mma' : 'sports_martial_arts';
  }
}
