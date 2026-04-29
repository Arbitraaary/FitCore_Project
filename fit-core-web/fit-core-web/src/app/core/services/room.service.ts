import { inject, Injectable } from '@angular/core';
import { Room } from '../models/types';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RoomService {
  private http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/Room`;

  getAllRaw(): Room[]{
    let rooms: Room[] = [];
    this.getAll().subscribe({
      next: data => rooms = data,
      error: err => console.log(err),
    });
    return rooms;
  }
  getAll(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/GetRooms`, { withCredentials: true });
  }

  getByIdRaw(id: string): Room | undefined {
    let room: Room | undefined = undefined;
    this.getById(id).subscribe({
      next: data => room = data,
      error: err => console.log(err),
    });
    return room;
  }
  getById(id: string): Observable<Room> {
    return this.http.get<Room>(`${this.apiUrl}/GetRoom/${id}`, { withCredentials: true });
  }

  getName(room: Room | undefined): string {
    return room ? `${room.roomType} (cap. ${room.capacity})` : '—';
  }
}
