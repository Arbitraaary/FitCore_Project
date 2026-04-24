import { Component, input, output, computed, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CalendarEvent } from '../calendar-event.model';
import { HOURS, weekDays, eventsForDay, eventPosition, fmtTime, isToday } from '../calendar.utils';

@Component({
  selector: 'app-week-grid',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, DatePipe, MatIconModule, MatButtonModule, MatTooltipModule],
  templateUrl: './week-grid.component.html',
  styleUrls: ['./week-grid.component.scss'],
})
export class WeekGridComponent {
  // ── Inputs ────────────────────────────────────────────────────────────────
  weekStart = input.required<Date>();
  events = input<CalendarEvent[]>([]);
  showTypes = input<('personal' | 'group')[]>(['personal', 'group']);

  // ── Outputs ───────────────────────────────────────────────────────────────
  eventClicked = output<CalendarEvent>();

  // ── Computed ──────────────────────────────────────────────────────────────
  days = computed(() => weekDays(this.weekStart()));
  hours = HOURS;

  filteredEvents = computed(() => this.events().filter((e) => this.showTypes().includes(e.type)));

  dayEvents(day: Date): CalendarEvent[] {
    return eventsForDay(this.filteredEvents(), day);
  }

  position(event: CalendarEvent): { top: string; height: string } {
    const { top, height } = eventPosition(event);
    return { top: `${top}%`, height: `${height}%` };
  }

  startLabel(event: CalendarEvent): string {
    return fmtTime(event.startTime);
  }
  endLabel(event: CalendarEvent): string {
    return fmtTime(event.endTime);
  }

  isToday(day: Date): boolean {
    return isToday(day);
  }

  trackById(_: number, e: CalendarEvent): string {
    return e.id;
  }
}
