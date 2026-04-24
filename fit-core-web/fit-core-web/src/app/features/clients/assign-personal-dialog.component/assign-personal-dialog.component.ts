import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule, formatDate } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDividerModule } from '@angular/material/divider';
import { CoachService } from '../../../core/services/coach.service';
import { RoomService } from '../../../core/services/room.service';
import { SessionService } from '../../../core/services/session.service';
import { MatDatepicker, MatDatepickerInput, MatDatepickerToggle } from '@angular/material/datepicker';
import { MAT_DATE_LOCALE, provideNativeDateAdapter } from '@angular/material/core';

@Component({
  selector: 'app-assign-personal-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatStepperModule,
    MatDividerModule,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
  ],
  providers: [provideNativeDateAdapter(), { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }],
  templateUrl: './assign-personal-dialog.component.html',
  styleUrls: ['./assign-personal-dialog.component.scss'],
})
export class AssignPersonalDialogComponent {
  private fb = inject(FormBuilder);
  private coachSvc = inject(CoachService);
  private roomSvc = inject(RoomService);
  private sessionSvc = inject(SessionService);
  private dialogRef = inject(MatDialogRef<AssignPersonalDialogComponent>);
  data = inject(MAT_DIALOG_DATA) as { clientId: string; clientName: string };

  coaches = this.coachSvc.getAll();
  rooms = this.roomSvc.getAll();

  success = signal(false);
  error = signal('');

  // Step 1: pick coach + session details
  step1 = this.fb.group({
    coachId: [null as string | null, Validators.required],
    name: ['', [Validators.required, Validators.maxLength(150)]],
    roomId: [null as string | null, Validators.required],
  });

  // Step 2: pick date/time
  step2 = this.fb.group({
    date: [Date.now(), Validators.required],
    startHour: [null as number | null, Validators.required],
    duration: [60, [Validators.required, Validators.min(30), Validators.max(240)]],
  });

  hours = Array.from({ length: 14 }, (_, i) => i + 7); // 07:00–20:00

  coachName(id: string): string {
    const c = this.coaches.find((c) => c.id === id);
    return c ? `${c.firstName} ${c.lastName} (${c.specialization})` : '';
  }

  roomLabel(r: { roomType: string; capacity: number }): string {
    return `${r.roomType} — capacity ${r.capacity}`;
  }

  submit() {
    if (this.step1.invalid || this.step2.invalid) return;
    this.error.set('');

    const { coachId, name, roomId } = this.step1.value;
    const { date, startHour, duration } = this.step2.value;

    const start = new Date(date!);
    start.setHours(startHour!);
    const end = new Date(start.getTime() + duration! * 60000);

    this.sessionSvc.createPersonalSession({
      clientId: this.data.clientId,
      coachId: coachId!,
      roomId: roomId!,
      name: name!,
      startTime: start.toISOString(),
      endTime: end.toISOString(),
    });

    this.success.set(true);
    setTimeout(() => this.dialogRef.close(true), 1100);
  }

  close() {
    this.dialogRef.close(false);
  }
}
