import { computed, Injectable, signal, Type } from '@angular/core';
import { GymLocation, User } from '../models/types';

@Injectable({
  providedIn: 'root',
})
export class UserService {


  private _currentUser = signal<User | null>(this.loadFromStorage());

  readonly currentUser = this._currentUser.asReadonly();
  readonly isLoggedIn = computed(() => this._currentUser() !== null);
  readonly isManager = computed(() => this._currentUser()?.userType === 'manager');
  readonly isCoach = computed(() => this._currentUser()?.userType === 'coach');
  setUser(user: User | null): void {
    this._currentUser.set(user);
    if (user) {
      sessionStorage.setItem('auth_user', JSON.stringify(user));
    } else {
      sessionStorage.removeItem('auth_user');
    }
  }
  loadFromStorage<T>(): T | null {
    try {
      const raw = sessionStorage.getItem('auth_user');
      return raw ? (JSON.parse(raw) as T) : null;
    } catch {
      return null;
    }
  }
}
