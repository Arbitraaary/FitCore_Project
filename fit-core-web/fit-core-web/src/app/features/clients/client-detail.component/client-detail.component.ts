import { Component, computed, inject, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ClientService } from '../../../core/services/client.service';
import { MembershipService } from '../../../core/services/membership.service';
import { SessionService } from '../../../core/services/session.service';
import { CoachService } from '../../../core/services/coach.service';
import { InfoRowComponent } from '../../../shared/info-row.component/info-row.component';
import { SectionCardComponent } from '../../../shared/section-card.component/section-card.component';
import {
  AssignMembershipDialogComponent
} from '../assign-membership-dialog.component/assign-membership-dialog.component';

@Component({
  selector: 'app-client-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    InfoRowComponent,
    SectionCardComponent,
  ],
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.scss'],
})
export class ClientDetailComponent {
  id = input.required<string>();
  membershipSvc = inject(MembershipService);
  memberships = computed(() => this.membershipSvc.getByClientId(this.id()));
  activeMem = computed(() => this.membershipSvc.getActiveByClientId(this.id()));
  private clientSvc = inject(ClientService);
  client = computed(() => this.clientSvc.getById(this.id()));
  initials = computed(() => {
    const c = this.client();
    return c ? `${c.firstName[0]}${c.lastName[0]}`.toUpperCase() : '?';
  });
  private sessionSvc = inject(SessionService);
  personal = computed(() => this.sessionSvc.getPersonalByClientId(this.id()));
  group = computed(() => this.sessionSvc.getGroupByClientId(this.id()));
  private coachSvc = inject(CoachService);
  private dialog = inject(MatDialog);

  membershipName(typeId: string): string {
    return this.membershipSvc.getMembershipTypeName(typeId);
  }

  coachName(coachId: string): string {
    const c = this.coachSvc.getById(coachId);
    return c ? `${c.firstName} ${c.lastName}` : '—';
  }

  statusClass(status: string): string {
    return status === 'active'
      ? 'chip--success'
      : status === 'suspended'
        ? 'chip--warn'
        : 'chip--neutral';
  }

  openAssignMembership() {
    this.dialog.open(AssignMembershipDialogComponent, {
      width: '480px',
      data: { clientId: this.id() },
    });
  }
}
