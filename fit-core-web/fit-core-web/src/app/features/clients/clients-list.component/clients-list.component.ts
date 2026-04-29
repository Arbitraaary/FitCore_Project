import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ClientService } from '../../../core/services/client.service';
import { MembershipService } from '../../../core/services/membership.service';
import { RouterLink } from '@angular/router';
import { Client, Coach, MembershipType } from '../../../core/models/types';

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
export class ClientsListComponent implements OnInit {
  search = signal('');
  private clientSvc = inject(ClientService);
  private membershipSvc = inject(MembershipService);
  private allClients = signal<Client[]>([]);
  private membershipTypes = signal<MembershipType[]>([]);
  ngOnInit() {
    this.clientSvc.getAllAndMembership().subscribe((data) => {
      this.allClients.set(data);
    });
    this.membershipSvc.getMembershipTypes().subscribe((types) => this.membershipTypes.set(types));
  }
  clients = computed(() => {
    const q = this.search().toLowerCase();
    return this.allClients().filter(
      (c) =>
        !q ||
        c.firstName.toLowerCase().includes(q) ||
        c.lastName.toLowerCase().includes(q) ||
        c.email.toLowerCase().includes(q),
    );
  });

  activeMembership(clientId: string) {
    return this.membershipSvc.getActiveByClientId(clientId);
  }

  membershipName(typeId: string): string {
    return this.membershipSvc.getMembershipTypeName(typeId, this.membershipTypes());
  }

  initials(f: string, l: string): string {
    return `${f[0]}${l[0]}`.toUpperCase();
  }
}
