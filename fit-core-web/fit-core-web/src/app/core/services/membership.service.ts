import { computed, inject, Injectable, signal } from '@angular/core';
import { ClientMembership, MembershipStatus, MembershipType } from '../models/types';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { map, Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class MembershipService {
  private http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/MembershipType`;
  private readonly clientApiUrl = `${environment.apiUrl}/Clients`;

  public getByClientIdRaw(clientId: string): ClientMembership[] {
    let memberships: ClientMembership[] = [];
    this.getByClientId(clientId).subscribe({
      next: (data) => {
        memberships = data;
      },
      error: (err) => console.error(err),
    });
    return memberships;
  }

  public getActiveByClientIdRaw(clientId: string): ClientMembership | undefined {
    let activeMembership: ClientMembership | undefined = undefined;
    this.getActiveByClientId(clientId).subscribe({
      next: (data) => {
        activeMembership = data;
      },
      error: (err) => console.error(err),
    });
    return activeMembership;
  }

  public assignRaw(clientId: string, membershipTypeId: string): void {
    this.assign(clientId, membershipTypeId).subscribe({
      next: () => {
        console.log('Membership assigned successfully');
      },
      error: (err) => console.error(err),
    });
  }
  getMembershipTypesRaw(): MembershipType[] {
    let memberships: MembershipType[] = [];
    this.getMembershipTypes().subscribe({
      next: data => memberships = data,
      error: (err) => console.error(err),
    })
    return memberships;
  }
  getMembershipTypes(): Observable<MembershipType[]> {
    return this.http.get<MembershipType[]>(`${this.apiUrl}/GetMembershipTypes`, {
      withCredentials: true,
    });
  }

  getByClientId(clientId: string): Observable<ClientMembership[]> {
    return this.http.get<ClientMembership[]>(`${this.clientApiUrl}/GetMemberships`, {
      params: { clientId },
      withCredentials: true
    });
  }

  getActiveByClientId(clientId: string): Observable<ClientMembership | undefined> {
    return this.getByClientId(clientId).pipe(
      map((list) => list.find((m) => m.status === 'active')),
    );
  }

  assign(clientId: string, membershipTypeId: string): Observable<any> {
    const payload = {
      clientId: clientId,
      membershipTypeId: membershipTypeId,
    };
    return this.http.post(`${this.clientApiUrl}/AssignMembership`, payload, {
      withCredentials: true,
    });
  }

  getMembershipTypeName(typeId: string, types: MembershipType[]): string {
    return types.find((t) => t.id === typeId)?.name ?? '—';
  }

  resolveStatus(m: ClientMembership): MembershipStatus {
    if (!m.status) return 'expired';
    const end = new Date(m.endDate);
    if (end < new Date()) return 'expired';
    return m.status;
  }
}
