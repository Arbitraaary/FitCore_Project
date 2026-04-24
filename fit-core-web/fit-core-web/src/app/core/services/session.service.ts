import { Injectable, signal } from '@angular/core';
import { PersonalTrainingSession, GroupTrainingSession } from '../models/types';
import { MOCK_PERSONAL_SESSIONS, MOCK_GROUP_SESSIONS } from '../data/mock.data';

export interface CreatePersonalSessionDto {
  clientId: string;
  coachId: string;
  roomId: string;
  name: string;
  startTime: string;
  endTime: string;
}

export interface CreateGroupEnrollmentDto {
  clientId: string;
  sessionId: string;
}

@Injectable({ providedIn: 'root' })
export class SessionService {
  private _personal = signal<PersonalTrainingSession[]>([...MOCK_PERSONAL_SESSIONS]);
  private _group = signal<GroupTrainingSession[]>([...MOCK_GROUP_SESSIONS]);

  readonly personalSessions = this._personal.asReadonly();
  readonly groupSessions = this._group.asReadonly();

  // ── Read ──────────────────────────────────────────────────────────────────

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

  getAllGroup(): GroupTrainingSession[] {
    return this._group();
  }

  // ── Write ─────────────────────────────────────────────────────────────────

  createPersonalSession(dto: CreatePersonalSessionDto): PersonalTrainingSession {
    const session: PersonalTrainingSession = {
      id: `ps-${Date.now()}`,
      ...dto,
    };
    this._personal.update((list) => [...list, session]);
    return session;
  }

  enrollClientInGroup(dto: CreateGroupEnrollmentDto): boolean {
    let enrolled = false;
    this._group.update((list) =>
      list.map((s) => {
        if (s.id !== dto.sessionId) return s;
        if (s.enrolledClientIds.includes(dto.clientId)) return s;
        if (s.enrolledClientIds.length >= s.capacity) return s;
        enrolled = true;
        return { ...s, enrolledClientIds: [...s.enrolledClientIds, dto.clientId] };
      }),
    );
    return enrolled;
  }

  isClientEnrolledInGroup(clientId: string, sessionId: string): boolean {
    const s = this.getGroupById(sessionId);
    return s?.enrolledClientIds.includes(clientId) ?? false;
  }

  isGroupFull(sessionId: string): boolean {
    const s = this.getGroupById(sessionId);
    return s ? s.enrolledClientIds.length >= s.capacity : true;
  }
}
