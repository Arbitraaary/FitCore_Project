import { Injectable, signal, computed, inject, Type } from '@angular/core';
import { Router } from '@angular/router';
import { AuthUser, Coach, Manager, User, UserType } from '../models/types';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import {environment} from '../../../environments/environment';
import { UserService } from './user.service';
export interface LoginCredentials {
  email: string;
  password: string;
}

export interface LoginResponse {
  userId: string;
  email: string;
  userType: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private userSvc = inject(UserService);
  private readonly apiUrl = environment.apiUrl;
  private readonly authUrl = environment.apiUrl + '/Auth/Login';

  login(credentials: LoginCredentials): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.authUrl, credentials, { withCredentials: true }).pipe(
      tap((response) => {
        response.userType = response.userType.toLowerCase() as UserType;
      }),
    )
  }

  getCompleteUser(loginUser: LoginResponse) : Observable<User> | null {
    if (loginUser.userType === 'admin') {
      return null;
    }

    if (loginUser.userType === 'manager') {
      return this.http.get<Manager>(`${this.apiUrl}/Manager/GetManager/${loginUser.userId}`, { withCredentials: true })
        .pipe(
          tap((data) => {
            data.userType = data.userType.toLowerCase() as 'manager';
            this.userSvc.setUser(data);
          })
        );
    }

    if (loginUser.userType === 'coach') {
      return this.http
        .get<Coach>(`${this.apiUrl}/Coaches/GetCoach/${loginUser.userId}`, { withCredentials: true })
        .pipe(
          tap((data) => {
            data.userType = data.userType.toLowerCase() as 'coach';
            this.userSvc.setUser(data);
          }),
        );
    }
    return null;
  }

  logout(): void {
    this.userSvc.setUser(null);
    this.router.navigate(['/login']);
  }


}
