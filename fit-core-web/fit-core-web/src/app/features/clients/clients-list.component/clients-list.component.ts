import { Component, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ClientService } from '../../../core/services/client.service';
import { MembershipService } from '../../../core/services/membership.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-clients-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    RouterLink,
  ],
  templateUrl: './clients-list.component.html',
  styleUrls: ['./clients-list.component.scss'],
})
export class ClientsListComponent {
  search = signal('');
  private clientSvc = inject(ClientService);
  clients = computed(() => {
    const q = this.search().toLowerCase();
    return this.clientSvc
      .getAll()
      .filter(
        (c) =>
          !q ||
          c.firstName.toLowerCase().includes(q) ||
          c.lastName.toLowerCase().includes(q) ||
          c.email.toLowerCase().includes(q),
      );
  });
  private membershipSvc = inject(MembershipService);

  activeMembership(clientId: string) {
    return this.membershipSvc.getActiveByClientId(clientId);
  }

  membershipName(typeId: string): string {
    return this.membershipSvc.getMembershipTypeName(typeId);
  }

  initials(f: string, l: string): string {
    return `${f[0]}${l[0]}`.toUpperCase();
  }
}
