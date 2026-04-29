import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { FormsModule } from '@angular/forms';
import { MembershipService } from '../../../core/services/membership.service';
import { MembershipType } from '../../../core/models/types';

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
export class AssignMembershipDialogComponent implements OnInit {
  data = inject(MAT_DIALOG_DATA) as { clientId: string };
  selected = signal<string | null>(null);
  success = signal(false);
  private membershipSvc = inject(MembershipService);
  types = signal<MembershipType[]>([]);
  private dialogRef = inject(MatDialogRef<AssignMembershipDialogComponent>);

  ngOnInit() {
    this.membershipSvc.getMembershipTypes().subscribe({
      next: data => {
        this.types.set(data);
      },
      error: err => console.log(err),
    });
  }
  assign() {
    const selectedId = this.selected();
    if (!selectedId) return;

    this.membershipSvc.assign(this.data.clientId, selectedId).subscribe({
      next: () => {
        this.success.set(true);
        setTimeout(() => this.dialogRef.close(true), 1000);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  close() {
    this.dialogRef.close(false);
  }
}
