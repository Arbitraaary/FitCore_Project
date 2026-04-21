import { Component, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CoachService } from '../../../core/services/coach.service';
import { SessionService } from '../../../core/services/session.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-coaches-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatTooltipModule,
    RouterLink,
  ],
  templateUrl: './coaches-list.component.html',
  styleUrls: ['./coaches-list.component.scss'],
})
export class CoachesListComponent {
  search = signal('');
  private coachSvc = inject(CoachService);
  coaches = computed(() => {
    const q = this.search().toLowerCase();
    return this.coachSvc
      .getAll()
      .filter(
        (c) =>
          !q ||
          c.firstName.toLowerCase().includes(q) ||
          c.lastName.toLowerCase().includes(q) ||
          c.email.toLowerCase().includes(q) ||
          c.specialization.toLowerCase().includes(q),
      );
  });
  private sessionSvc = inject(SessionService);

  sessionCount(coachId: string): number {
    return (
      this.sessionSvc.getPersonalByCoachId(coachId).length +
      this.sessionSvc.getGroupByCoachId(coachId).length
    );
  }

  specializationIcon(spec: string): string {
    return spec === 'Swim' ? 'pool' : spec === 'Box' ? 'sports_mma' : 'sports_martial_arts';
  }

  initials(firstName: string, lastName: string): string {
    return `${firstName[0]}${lastName[0]}`.toUpperCase();
  }
}
