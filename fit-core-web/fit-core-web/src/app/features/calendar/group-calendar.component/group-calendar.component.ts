import { Component, inject, signal, computed, OnInit } from '@angular/core';
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
export class GroupCalendarComponent implements OnInit {
  private sessionSvc = inject(SessionService);
  private coachSvc = inject(CoachService);
  private roomSvc = inject(RoomService);
  private dialog = inject(MatDialog);

  currentWeekStart = signal(weekStart(new Date()));
  events = signal<CalendarEvent[]>([]);

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

  ngOnInit() {
    this._updateCalendarEvents();
  }

  private _updateCalendarEvents(){
    this.sessionSvc.getAllGroupWithCoachAndRoom().subscribe({
      next: (gts) => {
        let newData = gts.map((s) : CalendarEvent => {
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
          }
        });

        this.events.set(newData);
      },
      error: (err) => console.log(err),
    });
  }

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
