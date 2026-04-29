import { inject, Injectable } from '@angular/core';
import { Client } from '../models/types';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface CreateClientDto {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  password: string;
}

@Injectable({ providedIn: 'root' })
export class ClientService {
  private http = inject(HttpClient);
  private readonly authUrl = environment.apiUrl + '/Auth';
  private readonly apiUrl = environment.apiUrl + '/Clients';


  public getByIdRaw(id: string): Client | undefined {
    let client: Client | undefined = undefined;
    this.getById(id).subscribe({
      next: (data) => {
        client = data;
        console.log(client);
      },
      error: (err) => {
        console.log(err);
      },
    });

    return client;
  }

  public getAllRaw(): Client[] {
    let clients: Client[] = [];
    this.getAll().subscribe({
      next: (data) => {
        clients = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
    return clients;
  }

  createRaw(dto: CreateClientDto): any {
    this.create(dto).subscribe({
      next: () => {},
      error: (err) => console.log(err),
    });
  }

  emailExistsRaw(email: string, excludeId?: string): boolean {
    let isEmailExist: boolean = false;
    this.emailExists(email, excludeId).subscribe({
      next: () => {
        isEmailExist = true;
      },
      error: (err) => console.log(err),
    });
    return isEmailExist;
  }
  getAll(): Observable<Client[]> {
    return this.http.get<Client[]>(`${this.apiUrl}/GetClients`, { withCredentials: true });
  }
  getAllAndMembership(): Observable<Client[]> {
    return this.http.get<Client[]>(`${this.apiUrl}/GetClientsAndActiveMembership`, {
      withCredentials: true,
    });
  }

  getById(id: string): Observable<Client> {
    return this.http.get<Client>(`${this.apiUrl}/GetClient/${id}`, { withCredentials: true });
  }

  create(dto: CreateClientDto): Observable<any> {
    return this.http.post(`${this.authUrl}/RegisterClient`, dto, { withCredentials: true });
  }

  emailExists(email: string, excludeId?: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.authUrl}/CheckEmail`, { params: { email }, withCredentials: true});
  }
}
