import { Component, inject, signal, computed, input, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { SessionService } from '../../../core/services/session.service';
import { CoachService } from '../../../core/services/coach.service';
import { ClientService } from '../../../core/services/client.service';
import { RoomService } from '../../../core/services/room.service';
import { AuthService } from '../../../core/services/auth.service';
import { CalendarEvent } from '../shared/calendar-event.model';
import { weekStart, addDays } from '../shared/calendar.utils';
import { WeekGridComponent } from '../shared/week-grid.component/week-grid.component';
import { EventDetailDialogComponent } from '../event-detail-dialog.component/event-detail-dialog.component';

@Component({
  selector: 'app-coach-calendar',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatSelectModule,
    WeekGridComponent,
  ],
  templateUrl: './coach-calendar.component.html',
  styleUrls: ['./coach-calendar.component.scss'],
})
export class CoachCalendarComponent {
  // Route param bound via withComponentInputBinding()
  coachId = input.required<string>();

  private sessionSvc = inject(SessionService);
  private coachSvc = inject(CoachService);
  private clientSvc = inject(ClientService);
  private roomSvc = inject(RoomService);
  private auth = inject(AuthService);
  private dialog = inject(MatDialog);

  isManager = this.auth.isManager;

  currentWeekStart = signal(weekStart(new Date()));

  // Filter: 'all' | 'personal' | 'group'
  filter = signal<'all' | 'personal' | 'group'>('all');

  coach = computed(() => this.coachSvc.getById(this.coachId()));

  coachFullName = computed(() => {
    const c = this.coach();
    return c ? c.firstName + ' ' + c.lastName : '—';
  });

  weekLabel = computed(() => {
    const start = this.currentWeekStart();
    const end = addDays(start, 6);
    const opts: Intl.DateTimeFormatOptions = { month: 'short', day: 'numeric' };
    return (
      start.toLocaleDateString('en-GB', opts) +
      ' – ' +
      end.toLocaleDateString('en-GB', { ...opts, year: 'numeric' })
    );
  });

  showTypes = computed<('personal' | 'group')[]>(() => {
    const f = this.filter();
    if (f === 'personal') return ['personal'];
    if (f === 'group') return ['group'];
    return ['personal', 'group'];
  });

  events = computed<CalendarEvent[]>(() => {
    const id = this.coachId();
    const coach = this.coach();
    if (!coach) return [];

    const coachName = coach.firstName + ' ' + coach.lastName;

    const personal: CalendarEvent[] = this.sessionSvc.getPersonalByCoachId(id).map((s) => {
      const client = this.clientSvc.getById(s.clientId);
      const room = this.roomSvc.getById(s.roomId);
      return {
        id: s.id,
        type: 'personal' as const,
        name: s.name,
        coachId: id,
        coachName,
        startTime: new Date(s.startTime),
        endTime: new Date(s.endTime),
        roomId: s.roomId,
        roomName: room ? room.roomType + ' (cap. ' + room.capacity + ')' : '—',
        clientId: s.clientId,
        clientName: client ? client.firstName + ' ' + client.lastName : '—',
      };
    });

    const group: CalendarEvent[] = this.sessionSvc.getGroupByCoachId(id).map((s) => {
      const room = this.roomSvc.getById(s.roomId);
      return {
        id: s.id,
        type: 'group' as const,
        name: s.name,
        coachId: id,
        coachName,
        startTime: new Date(s.startTime),
        endTime: new Date(s.endTime),
        roomId: s.roomId,
        roomName: room ? room.roomType + ' (cap. ' + room.capacity + ')' : '—',
        capacity: s.capacity,
        enrolledCount: s.enrolledClientIds.length,
        description: s.description,
      };
    });

    return [...personal, ...group];
  });

  totalThisWeek = computed(() => {
    const ws = this.currentWeekStart();
    const we = addDays(ws, 7);
    return this.events().filter((e) => e.startTime >= ws && e.startTime < we).length;
  });

  prevWeek() {
    this.currentWeekStart.update((d) => addDays(d, -7));
  }
  nextWeek() {
    this.currentWeekStart.update((d) => addDays(d, 7));
  }
  goToday() {
    this.currentWeekStart.set(weekStart(new Date()));
  }

  filterOptions: { label: string; value: 'all' | 'personal' | 'group' }[] = [
    { label: 'All', value: 'all' },
    { label: 'Personal', value: 'personal' },
    { label: 'Group', value: 'group' },
  ];

  onEventClick(event: CalendarEvent) {
    this.dialog.open(EventDetailDialogComponent, { data: event, width: '480px' });
  }
}
