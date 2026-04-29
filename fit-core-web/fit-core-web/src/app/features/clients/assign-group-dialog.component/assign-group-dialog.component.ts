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
        s.coachName.toLowerCase().includes(q),
    );
  });

  isEnrolled(sessionId: string): boolean {
    return this.allSessions().find(s => s.id == sessionId)!.enrolledClientIds.find(ec => ec == this.data.clientId) !== undefined;
  }

  isFull(sessionId: string): boolean {
    return this.spotsLeft(this.allSessions().find(s => s.id == sessionId)!) == 0;
  }

  spotsLeft(s: GroupTrainingSession): number {
    return s.capacity - s.enrolledClientIds.length;
  }

  select(id: string) {
    if (this.isEnrolled(id) || this.isFull(id)) return;
    this.selected.set(id === this.selected() ? null : id);
    this.errorMsg.set('');
  }

  enroll() {
    if (!this.selected()) return;
    this.sessionSvc.enrollClientInGroup({
      clientId: this.data.clientId,
      sessionId: this.selected()!,
    }).subscribe({
      next: data => {
        this.success.set(true);
        setTimeout(() => this.dialogRef.close(true), 1100);
      },
      error: error => {
        this.errorMsg.set('Could not enroll — session may be full.');
        return;
      }
    });

  }

  close() {
    this.dialogRef.close(false);
  }
}
