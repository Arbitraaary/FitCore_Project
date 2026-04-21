import { Injectable, signal } from '@angular/core';
import { GroupTrainingSession, PersonalTrainingSession } from '../models/types';
import { MOCK_GROUP_SESSIONS, MOCK_PERSONAL_SESSIONS } from '../data/mock.data';

@Injectable({ providedIn: 'root' })
export class SessionService {
  private _personal = signal<PersonalTrainingSession[]>([...MOCK_PERSONAL_SESSIONS]);
  readonly personalSessions = this._personal.asReadonly();
  private _group = signal<GroupTrainingSession[]>([...MOCK_GROUP_SESSIONS]);
  readonly groupSessions = this._group.asReadonly();

  getPersonalByClientId(clientId: string): PersonalTrainingSession[] {
    return this._personal().filter((s) => s.clientId === clientId);
  }

  getGroupByClientId(clientId: string): GroupTrainingSession[] {
    return this._group().filter((s) => s.enrolledClientIds.includes(clientId));
  }

  getPersonalByCoachId(coachId: string): PersonalTrainingSession[] {
    return this._personal().filter((s) => s.coachId === coachId);
  }

  getGroupByCoachId(coachId: string): GroupTrainingSession[] {
    return this._group().filter((s) => s.coachId === coachId);
  }

  getGroupById(id: string): GroupTrainingSession | undefined {
    return this._group().find((s) => s.id === id);
  }

  getPersonalById(id: string): PersonalTrainingSession | undefined {
    return this._personal().find((s) => s.id === id);
  }
}
