import { Component, computed, inject, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CoachService } from '../../../core/services/coach.service';
import { SessionService } from '../../../core/services/session.service';
import { InfoRowComponent } from '../../../shared/info-row.component/info-row.component';
import { SectionCardComponent } from '../../../shared/section-card.component/section-card.component';
import { RouterLink } from '@angular/router';

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
export class CoachDetailComponent {
  id = input.required<string>();
  private coachSvc = inject(CoachService);
  coach = computed(() => this.coachSvc.getById(this.id()));
  initials = computed(() => {
    const c = this.coach();
    return c ? `${c.firstName[0]}${c.lastName[0]}`.toUpperCase() : '?';
  });
  private sessionSvc = inject(SessionService);
  personal = computed(() => this.sessionSvc.getPersonalByCoachId(this.id()));
  group = computed(() => this.sessionSvc.getGroupByCoachId(this.id()));

  specializationIcon(spec: string): string {
    return spec === 'Swim' ? 'pool' : spec === 'Box' ? 'sports_mma' : 'sports_martial_arts';
  }
}
