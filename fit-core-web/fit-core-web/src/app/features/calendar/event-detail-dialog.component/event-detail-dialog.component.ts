import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { CalendarEvent } from '../shared/calendar-event.model';
import { fmtTime } from '../shared/calendar.utils';

@Component({
  selector: 'app-event-detail-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule, MatDividerModule],
  templateUrl: './event-detail-dialog.component.html',
  styleUrls: ['./event-detail-dialog.component.scss'],
})
export class EventDetailDialogComponent {
  event = inject<CalendarEvent>(MAT_DIALOG_DATA);
  dialogRef = inject(MatDialogRef<EventDetailDialogComponent>);

  fmtTime = fmtTime;

  get duration(): string {
    const mins = Math.round(
      (this.event.endTime.getTime() - this.event.startTime.getTime()) / 60000,
    );
    const h = Math.floor(mins / 60);
    const m = mins % 60;
    return h > 0 ? `${h}h ${m > 0 ? m + 'min' : ''}`.trim() : `${m}min`;
  }

  close() {
    this.dialogRef.close();
  }
}
