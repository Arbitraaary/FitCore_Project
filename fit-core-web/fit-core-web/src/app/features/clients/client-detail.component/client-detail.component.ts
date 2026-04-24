import { Component, inject, computed, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { ClientService } from '../../../core/services/client.service';
import { MembershipService } from '../../../core/services/membership.service';
import { SessionService } from '../../../core/services/session.service';
import { CoachService } from '../../../core/services/coach.service';
import { InfoRowComponent } from '../../../shared/info-row.component/info-row.component';
import { SectionCardComponent } from '../../../shared/section-card.component/section-card.component';
import { AssignMembershipDialogComponent } from '../assign-membership-dialog.component/assign-membership-dialog.component';
import { AssignPersonalDialogComponent } from '../assign-personal-dialog.component/assign-personal-dialog.component';
import { AssignGroupDialogComponent } from '../assign-group-dialog.component/assign-group-dialog.component';

@Component({
  selector: 'app-client-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
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

  private clientSvc = inject(ClientService);
  membershipSvc = inject(MembershipService);
  private sessionSvc = inject(SessionService);
  private coachSvc = inject(CoachService);
  private dialog = inject(MatDialog);

  client = computed(() => this.clientSvc.getById(this.id()));
  memberships = computed(() => this.membershipSvc.getByClientId(this.id()));
  activeMem = computed(() => this.membershipSvc.getActiveByClientId(this.id()));
  personal = computed(() => this.sessionSvc.getPersonalByClientId(this.id()));
  group = computed(() => this.sessionSvc.getGroupByClientId(this.id()));

  initials = computed(() => {
    const c = this.client();
    return c ? `${c.firstName[0]}${c.lastName[0]}`.toUpperCase() : '?';
  });

  clientFullName = computed(() => {
    const c = this.client();
    return c ? `${c.firstName} ${c.lastName}` : '';
  });

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
      width: '500px',
      data: { clientId: this.id() },
    });
  }

  openAssignPersonal() {
    this.dialog.open(AssignPersonalDialogComponent, {
      width: '560px',
      data: { clientId: this.id(), clientName: this.clientFullName() },
    });
  }

  openAssignGroup() {
    this.dialog.open(AssignGroupDialogComponent, {
      width: '620px',
      maxHeight: '85vh',
      data: { clientId: this.id(), clientName: this.clientFullName() },
    });
  }
}
