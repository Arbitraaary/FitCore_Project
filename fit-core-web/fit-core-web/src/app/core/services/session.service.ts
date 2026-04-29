import { inject, Injectable, signal } from '@angular/core';
import {
  PersonalTrainingSession,
  GroupTrainingSession,
  Coach,
  ClientMembership,
} from '../models/types';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

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
  private http = inject(HttpClient);
  private readonly clientApiUrl = `${environment.apiUrl}/Clients`;
  private readonly coachApiUrl = `${environment.apiUrl}/Coaches`;
  private readonly managerApiUrl = `${environment.apiUrl}/Manager`;
  private readonly personalApiUrl = `${environment.apiUrl}/PersonalTrainingSession`;
  private readonly groupApiUrl = `${environment.apiUrl}/GroupTrainingSession`;
  getPersonalByClientIdRaw(clientId: string): PersonalTrainingSession[] {
    let personalTrainings: PersonalTrainingSession[] = [];
    this.getPersonalByClientId(clientId).subscribe({
      next: (data) => {
        personalTrainings = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
    return personalTrainings;
  }
  getPersonalByClientId(clientId: string): Observable<PersonalTrainingSession[]> {
    return this.http.get<PersonalTrainingSession[]>(`${this.clientApiUrl}/GetPersonalSessions`, {
      params: { clientId },
      withCredentials: true
    });
  }

  getGroupByClientIdRaw(clientId: string): GroupTrainingSession[] {
    let groupTrainings: GroupTrainingSession[] = [];
    this.getGroupByClientId(clientId).subscribe({
      next: (data) => {
        groupTrainings = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
    return groupTrainings;
  }
  getGroupByClientId(clientId: string): Observable<GroupTrainingSession[]> {
    return this.http.get<GroupTrainingSession[]>(`${this.clientApiUrl}/GetGroupSessions`, {
      params: { clientId },
      withCredentials: true
    });
  }
  getPersonalByCoachIdRaw(coachId: string): PersonalTrainingSession[] {
    let personal: PersonalTrainingSession[] = [];
    this.getPersonalByCoachId(coachId).subscribe({
      next: (data) => (personal = data),
      error: (err) => console.log(err),
    });
    return personal;
  }
  getPersonalByCoachId(coachId: string): Observable<PersonalTrainingSession[]> {
    return this.http.get<PersonalTrainingSession[]>(`${this.coachApiUrl}/GetPersonalSessions/${coachId}`,
      {withCredentials: true});
  }

  getGroupByCoachIdRaw(coachId: string): GroupTrainingSession[] {
    let group: GroupTrainingSession[] = [];
    this.getGroupByCoachId(coachId).subscribe({
      next: (data) => (group = data),
      error: (err) => console.log(err),
    });
    return group;
  }
  getGroupByCoachId(coachId: string): Observable<GroupTrainingSession[]> {
    return this.http.get<GroupTrainingSession[]>(`${this.coachApiUrl}/GetGroupSessions/${coachId}`, {
      withCredentials: true
    });
  }

  getGroupByIdRaw(id: string): GroupTrainingSession | undefined {
    let groupTraining: GroupTrainingSession | undefined = undefined;
    this.getGroupById(id).subscribe({
      next: (data) => {
        groupTraining = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
    return groupTraining;
  }
  getGroupById(id: string): Observable<GroupTrainingSession> {
    return this.http.get<GroupTrainingSession>(`${this.groupApiUrl}/GetGroupSession/${id}`, {
      withCredentials: true,
    });
  }

  getPersonalByIdRaw(id: string): PersonalTrainingSession | undefined {
    let personalTraining: PersonalTrainingSession | undefined = undefined;
    this.getPersonalById(id).subscribe({
      next: (data) => {
        personalTraining = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
    return personalTraining;
  }

  getPersonalById(id: string): Observable<PersonalTrainingSession> {
    return this.http.get<PersonalTrainingSession>(
      `${this.personalApiUrl}/GetPersonalSession/${id}`,
      { withCredentials: true },
    );
  }

  getAllGroupRaw(): GroupTrainingSession[] {
    let sessions: GroupTrainingSession[] = [];
    this.getAllGroup().subscribe({
      next: (data) => {
        sessions = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
    return sessions;
  }
  getAllGroup(): Observable<GroupTrainingSession[]> {
    return this.http.get<GroupTrainingSession[]>(`${this.groupApiUrl}/GetAllGroupSessions`, {
      withCredentials: true,
    });
  }

  // ── Write ─────────────────────────────────────────────────────────────────

  createPersonalSession(dto: CreatePersonalSessionDto): Observable<any> {
    console.log(dto);
    return this.http.post(`${this.personalApiUrl}/CreatePersonalSession`, dto, {
      withCredentials: true,
    });
  }

  enrollClientInGroup(dto: CreateGroupEnrollmentDto): Observable<any> {
    return this.http.post(`${this.clientApiUrl}/EnrollInGroup`, dto, { withCredentials: true });
  }

  isClientEnrolledInGroupRaw(clientId: string, sessionId: string): boolean {
    let isEnrolled = false;
    this.isClientEnrolledInGroup(clientId, sessionId).subscribe((res) => (isEnrolled = res));
    return isEnrolled;
  }
  isClientEnrolledInGroup(clientId: string, sessionId: string): Observable<boolean> {
    return this.getGroupById(sessionId).pipe(
      map((session) => session?.enrolledClientIds.includes(clientId) ?? false),
    );
  }

  isGroupFullRaw(sessionId: string): boolean {
    let full = false;
    this.isGroupFull(sessionId).subscribe((res) => (full = res));
    return full;
  }
  isGroupFull(sessionId: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.groupApiUrl}/IsGroupFull/${sessionId}`, {
      withCredentials: true,
    });
  }

  getPersonalByManager(userId: string): Observable<PersonalTrainingSession[]> {
    return this.http.get<PersonalTrainingSession[]>(
      `${this.managerApiUrl}/GetPersonalSessions/${userId}`,
      { withCredentials: true },
    );
  }

  getGroupByManager(userId: string): Observable<GroupTrainingSession[]> {
    return this.http.get<GroupTrainingSession[]>(
      `${this.managerApiUrl}/GetGroupSessions/${userId}`,
      { withCredentials: true },
    );
  }
}
