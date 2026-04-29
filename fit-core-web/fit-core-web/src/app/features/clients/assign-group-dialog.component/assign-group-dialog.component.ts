import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SessionService } from '../../../core/services/session.service';
import { CoachService } from '../../../core/services/coach.service';
import { GroupTrainingSession } from '../../../core/models/types';

@Component({
  selector: 'app-assign-group-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    MatTooltipModule,
  ],
  templateUrl: './assign-group-dialog.component.html',
  styleUrls: ['./assign-group-dialog.component.scss'],
})
export class AssignGroupDialogComponent implements OnInit {
  private sessionSvc = inject(SessionService);
  private coachSvc = inject(CoachService);
  private dialogRef = inject(MatDialogRef<AssignGroupDialogComponent>);
  data = inject(MAT_DIALOG_DATA) as { clientId: string; clientName: string };

  search = signal('');
  selected = signal<string | null>(null);
  success = signal(false);
  errorMsg = signal('');
  allSessions = signal<GroupTrainingSession[]>([]);

  ngOnInit() {
    this.sessionSvc.getAllGroup().subscribe({
      next: data => {
        this.allSessions.set(data);
      },
      error: error => console.log(error),
    });
  }

  filtered = computed(() => {
    const q = this.search().toLowerCase();
    return this.allSessions().filter(
      (s) =>
        !q ||
        s.name.toLowerCase().includes(q) ||
        this.coachName(s.coachId).toLowerCase().includes(q),
    );
  });

  isEnrolled(sessionId: string): boolean {
    return this.sessionSvc.isClientEnrolledInGroupRaw(this.data.clientId, sessionId);
  }

  isFull(sessionId: string): boolean {
    return this.sessionSvc.isGroupFullRaw(sessionId);
  }

  spotsLeft(s: GroupTrainingSession): number {
    return s.capacity - s.enrolledClientIds.length;
  }

  coachName(coachId: string): string {
    const c = this.coachSvc.getByIdRaw(coachId);
    return c ? `${c.firstName} ${c.lastName}` : '—';
  }

  select(id: string) {
    if (this.isEnrolled(id) || this.isFull(id)) return;
    this.selected.set(id === this.selected() ? null : id);
    this.errorMsg.set('');
  }

  enroll() {
    if (!this.selected()) return;
    const ok = this.sessionSvc.enrollClientInGroup({
      clientId: this.data.clientId,
      sessionId: this.selected()!,
    });
    if (!ok) {
      this.errorMsg.set('Could not enroll — session may be full.');
      return;
    }
    this.success.set(true);
    setTimeout(() => this.dialogRef.close(true), 1100);
  }

  close() {
    this.dialogRef.close(false);
  }
}
