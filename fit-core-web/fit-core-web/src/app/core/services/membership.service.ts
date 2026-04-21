import { Injectable, signal } from '@angular/core';
import { ClientMembership, MembershipStatus } from '../models/types';
import { MOCK_CLIENT_MEMBERSHIPS, MOCK_MEMBERSHIP_TYPES } from '../data/mock.data';

@Injectable({ providedIn: 'root' })
export class MembershipService {
  readonly membershipTypes = MOCK_MEMBERSHIP_TYPES;

  private _memberships = signal<ClientMembership[]>([...MOCK_CLIENT_MEMBERSHIPS]);
  readonly memberships = this._memberships.asReadonly();

  getByClientId(clientId: string): ClientMembership[] {
    return this._memberships().filter((m) => m.clientId === clientId);
  }

  getActiveByClientId(clientId: string): ClientMembership | undefined {
    return this._memberships().find((m) => m.clientId === clientId && m.status === 'active');
  }

  getMembershipTypeName(typeId: string): string {
    return this.membershipTypes.find((t) => t.id === typeId)?.name ?? '—';
  }

  assign(clientId: string, membershipTypeId: string): ClientMembership {
    const type = this.membershipTypes.find((t) => t.id === membershipTypeId)!;
    const start = new Date();
    const end = new Date(start);
    end.setDate(end.getDate() + type.duration);

    const membership: ClientMembership = {
      id: `cm-${Date.now()}`,
      membershipTypeId,
      clientId,
      startDate: start.toISOString().split('T')[0],
      endDate: end.toISOString().split('T')[0],
      status: 'active',
    };

    this._memberships.update((list) => [...list, membership]);
    return membership;
  }

  resolveStatus(m: ClientMembership): MembershipStatus {
    if (!m.status) return 'expired';
    const end = new Date(m.endDate);
    if (end < new Date()) return 'expired';
    return m.status;
  }
}
