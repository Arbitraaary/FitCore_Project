import { inject, Injectable, signal } from '@angular/core';
import { Coach, SpecializationType } from '../models/types';
import { MOCK_COACHES } from '../data/mock.data';
import { HttpClient} from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

export interface CreateCoachDto {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  specialization: SpecializationType;
  password: string;

}

@Injectable({ providedIn: 'root' })
export class CoachService {
  private http = inject(HttpClient);
  private readonly authUrl = environment.apiUrl + '/Auth';
  private _coaches = signal<Coach[]>([...MOCK_COACHES]);
  readonly coaches = this._coaches.asReadonly();

  getAll(): Coach[] {
    return this._coaches();
  }

  getById(id: string): Coach | undefined {
    return this._coaches().find((c) => c.id === id);
  }

  create(dto: CreateCoachDto): Observable<any> {
    return this.http.post(`${this.authUrl}/RegisterCoach`, dto.email, {withCredentials: true});
  }

  emailExists(email: string, excludeId?: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.authUrl}/CheckEmail`, { params: { email } });
  }
}
