export type CalendarEventType = 'personal' | 'group';

export interface CalendarEvent {
  id: string;
  type: CalendarEventType;
  name: string;
  coachId: string;
  coachName: string;
  startTime: Date;
  endTime: Date;
  roomId: string;
  roomName: string;
  // group-only
  capacity?: number;
  enrolledCount?: number;
  description?: string;
  // personal-only
  clientId?: string;
  clientName?: string;
}
