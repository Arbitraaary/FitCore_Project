import { Injectable, signal } from '@angular/core';
import { Coach, SpecializationType } from '../models/types';
import { MOCK_COACHES } from '../data/mock.data';

export interface CreateCoachDto {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  specialization: SpecializationType;
}

@Injectable({ providedIn: 'root' })
export class CoachService {
  private _coaches = signal<Coach[]>([...MOCK_COACHES]);
  readonly coaches = this._coaches.asReadonly();

  getAll(): Coach[] {
    return this._coaches();
  }

  getById(id: string): Coach | undefined {
    return this._coaches().find((c) => c.id === id);
  }

  create(dto: CreateCoachDto): Coach {
    const coach: Coach = {
      id: `usr-cch-${Date.now()}`,
      userType: 'coach',
      ...dto,
    };
    this._coaches.update((list) => [...list, coach]);
    return coach;
  }

  emailExists(email: string, excludeId?: string): boolean {
    return this._coaches().some(
      (c) => c.email.toLowerCase() === email.toLowerCase() && c.id !== excludeId,
    );
  }
}
