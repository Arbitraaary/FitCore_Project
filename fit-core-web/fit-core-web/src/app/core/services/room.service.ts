import { Injectable } from '@angular/core';
import { Room } from '../models/types';
import { MOCK_ROOMS } from '../data/mock.data';

@Injectable({ providedIn: 'root' })
export class RoomService {
  private _rooms = [...MOCK_ROOMS];

  getAll(): Room[] {
    return this._rooms;
  }

  getById(id: string): Room | undefined {
    return this._rooms.find((r) => r.id === id);
  }

  getName(id: string): string {
    const r = this.getById(id);
    return r ? `${r.roomType} (cap. ${r.capacity})` : '—';
  }
}
