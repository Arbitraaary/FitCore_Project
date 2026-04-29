import { inject, Injectable } from '@angular/core';
import { Coach, GymLocation, SpecializationType } from '../models/types';
import { HttpClient} from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { UserService } from './user.service';

export interface CreateCoachDto {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  specialization: SpecializationType;
  password: string;
  locationName: string;
}

@Injectable({ providedIn: 'root' })
export class CoachService {
  private http = inject(HttpClient);
  private userSvc = inject(UserService);
  private readonly authUrl = environment.apiUrl + '/Auth';
  private readonly apiUrl = environment.apiUrl + '/Coaches';

  public getByIdRaw(id: string): Coach | undefined {
    let coach: Coach | undefined = undefined;
    this.getById(id).subscribe({
      next: (data) => {
        coach = data;
      },
      error: (err) => {
        console.log(err);
      },
    });

    return coach;
  }

  public getAllRaw(): Coach[] {
    let coaches: Coach[] = [];
    this.getAll().subscribe({
      next: (data) => {
        coaches = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
    return coaches;
  }

  createRaw(dto: CreateCoachDto): any {
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

  getAll(): Observable<Coach[]> {
    return this.http.get<Coach[]>(`${this.apiUrl}/GetCoaches`, { withCredentials: true });
  }
  getById(id: string): Observable<Coach> {
    return this.http.get<Coach>(`${this.apiUrl}/GetCoach/${id}`, { withCredentials: true });
  }
  create(dto: CreateCoachDto): Observable<any> {
    return this.http.post(`${this.authUrl}/RegisterCoach`, dto, { withCredentials: true });
  }

  emailExists(email: string, excludeId?: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.authUrl}/CheckEmail`, { params: { email }, withCredentials: true });
  }

  getMyLocation(): GymLocation | null {
    let user = this.userSvc.loadFromStorage<Coach>();

    return user === null ? null : user.location;
  }

  getMe() {
    return this.userSvc.loadFromStorage<Coach>();
  }
}
