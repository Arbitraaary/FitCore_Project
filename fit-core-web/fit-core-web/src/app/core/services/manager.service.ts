import { inject, Injectable } from '@angular/core';
import { Coach, CoachWithSessionCount, GymLocation, Manager } from '../models/types';
import { UserService } from './user.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface CreateManagerDto {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  password: string;
  locationName: string;
}

@Injectable({
  providedIn: 'root',
})
export class ManagerService {
  private http = inject(HttpClient);
  private userSvc = inject(UserService);
  private readonly apiUrl = environment.apiUrl + '/Manager';

  getMyLocation(): GymLocation | null {
    let user = this.userSvc.loadFromStorage<Manager>();

    return user === null ? null : user.location;
  }

  getMe() {
    return this.userSvc.loadFromStorage<Manager>();
  }

  getMyCoaches(id: string): Observable<Coach[]> {
    return this.http.get<Coach[]>(`${this.apiUrl}/GetMyCoaches/${id}`, { withCredentials: true });
  }
  getMyCoachesWithSessionCount(id: string): Observable<CoachWithSessionCount[]> {
    return this.http.get<CoachWithSessionCount[]>(
      `${this.apiUrl}/GetMyCoachesWithSessionCount/${id}`,
      {
        withCredentials: true,
      },
    );
  }
}
