import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SessionService } from '../../../core/services/session.service';
import { CoachService } from '../../../core/services/coach.service';
import { RoomService } from '../../../core/services/room.service';
import { CalendarEvent } from '../shared/calendar-event.model';
import { weekStart, addDays } from '../shared/calendar.utils';
import { WeekGridComponent } from '../shared/week-grid.component/week-grid.component';
import { EventDetailDialogComponent } from '../event-detail-dialog.component/event-detail-dialog.component';
import { Coach, GroupTrainingSession } from '../../../core/models/types';

@Component({
  selector: 'app-group-calendar',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatTooltipModule, WeekGridComponent],
  templateUrl: './group-calendar.component.html',
  styleUrls: ['./group-calendar.component.scss'],
})
export class GroupCalendarComponent {
  private sessionSvc = inject(SessionService);
  private coachSvc = inject(CoachService);
  private roomSvc = inject(RoomService);
  private dialog = inject(MatDialog);

  currentWeekStart = signal(weekStart(new Date()));

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

  events = computed<CalendarEvent[]>(() =>
    this.sessionSvc.getAllGroupRaw().map((s) => {
      const coach = this.coachSvc.getByIdRaw(s.coachId);
      const room = this.roomSvc.getByIdRaw(s.roomId);
      return {
        id: s.id,
        type: 'group' as const,
        name: s.name,
        coachId: s.coachId,
        coachName: coach ? coach.firstName + ' ' + coach.lastName : '—',
        startTime: new Date(s.startTime),
        endTime: new Date(s.endTime),
        roomId: s.roomId,
        roomName: room ? room.roomType + ' (cap. ' + room.capacity + ')' : '—',
        capacity: s.capacity,
        enrolledCount: s.enrolledClientIds.length,
        description: s.description,
      };
    }),
  );

  prevWeek() {
    this.currentWeekStart.update((d) => addDays(d, -7));
  }
  nextWeek() {
    this.currentWeekStart.update((d) => addDays(d, 7));
  }
  goToday() {
    this.currentWeekStart.set(weekStart(new Date()));
  }

  onEventClick(event: CalendarEvent) {
    this.dialog.open(EventDetailDialogComponent, { data: event, width: '480px' });
  }
}
