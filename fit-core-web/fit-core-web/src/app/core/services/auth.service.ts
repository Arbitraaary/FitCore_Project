import { Injectable, signal, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthUser } from '../models/types';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import {environment} from '../../../environments/environment';
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

  private readonly authUrl = environment.apiUrl + '/Auth/Login';

  private _currentUser = signal<AuthUser | null>(this._loadFromStorage());

  readonly currentUser = this._currentUser.asReadonly();
  readonly isLoggedIn = computed(() => this._currentUser() !== null);
  readonly isManager = computed(() => this._currentUser()?.role === 'manager');
  readonly isCoach = computed(() => this._currentUser()?.role === 'coach');

  constructor() {}

  login(credentials: LoginCredentials): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.authUrl, credentials).pipe(
      tap((response) => {

        const authUser: AuthUser = {
          id: response.userId,
          email: response.email,
          firstName: response.firstName,
          lastName: response.lastName,
          role: response.userType.toLowerCase() as 'manager' | 'coach',
          phoneNumber: response.phoneNumber
        };
        this._setUser(authUser);
      }),
    );
  }

  logout(): void {
    this._setUser(null);
    this.router.navigate(['/login']);
  }

  private _setUser(user: AuthUser | null): void {
    this._currentUser.set(user);
    if (user) {
      sessionStorage.setItem('auth_user', JSON.stringify(user));
    } else {
      sessionStorage.removeItem('auth_user');
    }
  }

  private _loadFromStorage(): AuthUser | null {
    try {
      const raw = sessionStorage.getItem('auth_user');
      return raw ? (JSON.parse(raw) as AuthUser) : null;
    } catch {
      return null;
    }
  }
}
