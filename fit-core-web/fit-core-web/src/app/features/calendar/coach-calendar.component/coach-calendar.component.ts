import { Component, inject, signal, computed, input, effect, OnInit } from '@angular/core';
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
import { Coach } from '../../../core/models/types';
import { UserService } from '../../../core/services/user.service';

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
export class CoachCalendarComponent implements OnInit {
  // Route param bound via withComponentInputBinding()
  coachId = input.required<string>();

  private sessionSvc = inject(SessionService);
  private coachSvc = inject(CoachService);
  private userSvc = inject(UserService);
  private dialog = inject(MatDialog);

  isManager = this.userSvc.isManager;
  currentWeekStart = signal(weekStart(new Date()));
  // Filter: 'all' | 'personal' | 'group'
  filter = signal<'all' | 'personal' | 'group'>('all');
  groupEvents = signal<CalendarEvent[]>([]);
  personalEvents = signal<CalendarEvent[]>([]);
  events = signal<CalendarEvent[]>([]);
  coach = signal<Coach | null>(null);
  ngOnInit() {
    this.coachSvc.getById(this.coachId()).subscribe({
      next: data => {
        this.coach.set(data)
      },
      error: error => console.log(error),
    });
    this._updateGroupCalendarEvents();
    this._updatePersonalCalendarEvents();
  }

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

  private _setAllEvents(){
    this.events.set([...this.personalEvents(), ...this.groupEvents()]);
  }
  private _updateGroupCalendarEvents() {
    this.sessionSvc.getAllGroupWithCoachAndRoomById(this.coachId()).subscribe({
      next: (gts) => {
        let newData = gts.map((s): CalendarEvent => {
          return {
            id: s.id,
            type: 'group' as const,
            name: s.name,
            coachId: s.coachId,
            coachName: s.coach ? s.coach.firstName + ' ' + s.coach.lastName : '—',
            startTime: new Date(s.startTime),
            endTime: new Date(s.endTime),
            roomId: s.roomId,
            roomName: s.room ? s.room.roomType + ' (cap. ' + s.room.capacity + ')' : '—',
            capacity: s.capacity,
            enrolledCount: s.enrolledClientIds.length,
            description: s.description,
          };
        });

        this.groupEvents.set(newData);
        this._setAllEvents();
      },
      error: (err) => console.log(err),
    });
  }
  private _updatePersonalCalendarEvents() {
    this.sessionSvc.getAllPersonalWithCoachAndRoomById(this.coachId()).subscribe({
      next: (gts) => {
        let newData = gts.map((s): CalendarEvent => {
          return {
            id: s.id,
            type: 'personal' as const,
            name: s.name,
            coachId: s.coachId,
            coachName: s.coach ? s.coach.firstName + ' ' + s.coach.lastName : '—',
            startTime: new Date(s.startTime),
            endTime: new Date(s.endTime),
            roomId: s.roomId,
            roomName: s.room ? s.room.roomType + ' (cap. ' + s.room.capacity + ')' : '—'
          };
        });

        this.personalEvents.set(newData);
        this._setAllEvents();
      },
      error: (err) => console.log(err),
    });
  }

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
