import { CalendarEvent } from './calendar-event.model';

export const HOUR_START = 7; // 07:00
export const HOUR_END = 21; // last slot starts at 20:xx
export const HOURS = Array.from({ length: HOUR_END - HOUR_START }, (_, i) => HOUR_START + i);

/** Returns the Monday of the week containing `date`. */
export function weekStart(date: Date): Date {
  const d = new Date(date);
  const day = d.getDay(); // 0=Sun
  const diff = day === 0 ? -6 : 1 - day;
  d.setDate(d.getDate() + diff);
  d.setHours(0, 0, 0, 0);
  return d;
}

/** Returns an array of 7 Date objects starting from `monday`. */
export function weekDays(monday: Date): Date[] {
  return Array.from({ length: 7 }, (_, i) => {
    const d = new Date(monday);
    d.setDate(d.getDate() + i);
    return d;
  });
}

/** Add `days` to a date, returning a new Date. */
export function addDays(date: Date, days: number): Date {
  const d = new Date(date);
  d.setDate(d.getDate() + days);
  return d;
}

/** Check whether `event` falls (even partially) within a given day. */
export function eventOnDay(event: CalendarEvent, day: Date): boolean {
  const dayStart = new Date(day);
  dayStart.setHours(0, 0, 0, 0);
  const dayEnd = new Date(day);
  dayEnd.setHours(23, 59, 59, 999);
  return event.startTime < dayEnd && event.endTime > dayStart;
}

/** Filter events to only those touching a given day column. */
export function eventsForDay(events: CalendarEvent[], day: Date): CalendarEvent[] {
  return events.filter((e) => eventOnDay(e, day));
}

/**
 * Calculate CSS top% and height% within the visible hour range.
 * top    = minutes from HOUR_START relative to total visible minutes
 * height = duration in minutes relative to total visible minutes
 */
export function eventPosition(event: CalendarEvent): { top: number; height: number } {
  const totalMinutes = (HOUR_END - HOUR_START) * 60;
  const startMinutes = event.startTime.getHours() * 60 + event.startTime.getMinutes();
  const endMinutes = event.endTime.getHours() * 60 + event.endTime.getMinutes();
  const clampedStart = Math.max(startMinutes, HOUR_START * 60);
  const clampedEnd = Math.min(endMinutes, HOUR_END * 60);
  const top = ((clampedStart - HOUR_START * 60) / totalMinutes) * 100;
  const height = ((clampedEnd - clampedStart) / totalMinutes) * 100;
  return { top, height: Math.max(height, 2) };
}

/** Format a Date as "HH:MM". */
export function fmtTime(date: Date): string {
  return date.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' });
}

/** Returns true if `date` is today. */
export function isToday(date: Date): boolean {
  const now = new Date();
  return (
    date.getFullYear() === now.getFullYear() &&
    date.getMonth() === now.getMonth() &&
    date.getDate() === now.getDate()
  );
}
