import { Injectable, signal } from '@angular/core';
import { Client } from '../models/types';
import { MOCK_CLIENTS } from '../data/mock.data';

export interface CreateClientDto {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
}

@Injectable({ providedIn: 'root' })
export class ClientService {
  private _clients = signal<Client[]>([...MOCK_CLIENTS]);
  readonly clients = this._clients.asReadonly();

  getAll(): Client[] {
    return this._clients();
  }

  getById(id: string): Client | undefined {
    return this._clients().find((c) => c.id === id);
  }

  create(dto: CreateClientDto): Client {
    const client: Client = {
      id: `usr-cli-${Date.now()}`,
      userType: 'client',
      ...dto,
    };
    this._clients.update((list) => [...list, client]);
    return client;
  }

  emailExists(email: string, excludeId?: string): boolean {
    return this._clients().some(
      (c) => c.email.toLowerCase() === email.toLowerCase() && c.id !== excludeId,
    );
  }
}
