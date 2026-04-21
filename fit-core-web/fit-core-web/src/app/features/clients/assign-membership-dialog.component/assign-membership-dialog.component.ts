import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { FormsModule } from '@angular/forms';
import { MembershipService } from '../../../core/services/membership.service';

@Component({
  selector: 'app-assign-membership-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatRadioModule,
  ],
  templateUrl: './assign-membership-dialog.component.html',
  styleUrls: ['./assign-membership-dialog.component.scss'],
})
export class AssignMembershipDialogComponent {
  data = inject(MAT_DIALOG_DATA) as { clientId: string };
  selected = signal<string | null>(null);
  success = signal(false);
  private membershipSvc = inject(MembershipService);
  types = this.membershipSvc.membershipTypes;
  private dialogRef = inject(MatDialogRef<AssignMembershipDialogComponent>);

  assign() {
    if (!this.selected()) return;
    this.membershipSvc.assign(this.data.clientId, this.selected()!);
    this.success.set(true);
    setTimeout(() => this.dialogRef.close(true), 1000);
  }

  close() {
    this.dialogRef.close(false);
  }
}
