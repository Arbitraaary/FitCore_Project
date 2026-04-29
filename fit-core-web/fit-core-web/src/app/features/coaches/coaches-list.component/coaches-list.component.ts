import { Component, computed, inject, OnInit, signal } from '@angular/core';
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
import { Coach, CoachWithSessionCount } from '../../../core/models/types';
import { tap } from 'rxjs';
import { ManagerService } from '../../../core/services/manager.service';
import { UserService } from '../../../core/services/user.service';

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
export class CoachesListComponent implements OnInit {
  userSvc = inject(UserService);
  managerSvc = inject(ManagerService);
  sessionSvc = inject(SessionService);
  user = this.userSvc.currentUser;
  search = signal('');
  allCoaches = signal<CoachWithSessionCount[]>([]);
  ngOnInit() {
    if(this.user === null) return;
    this.managerSvc.getMyCoachesWithSessionCount(this.user()!.id).subscribe({
      next: data => {
      this.allCoaches.set(data);
      },
      error: error => console.log(error)
    });
  }

  coaches = computed(() => {
    const q = this.search().toLowerCase();
    return this.allCoaches().filter(
      (c) =>
        !q ||
        c.firstName.toLowerCase().includes(q) ||
        c.lastName.toLowerCase().includes(q) ||
        c.email.toLowerCase().includes(q) ||
        c.specialization.toLowerCase().includes(q),
    );
  });

  sessionCount(coachId: string): number {
    return (
      this.sessionSvc.getPersonalByCoachIdRaw(coachId).length +
      this.sessionSvc.getGroupByCoachIdRaw(coachId).length
    );
  }

  specializationIcon(spec: string): string {
    return spec === 'Swim' ? 'pool' : spec === 'Box' ? 'sports_mma' : 'sports_martial_arts';
  }

  initials(firstName: string, lastName: string): string {
    return `${firstName[0]}${lastName[0]}`.toUpperCase();
  }
}
