import { Injectable, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { AuthUser } from '../models/types';
import { MOCK_COACHES, MOCK_MANAGER } from '../data/mock.data';

export interface LoginCredentials {
  email: string;
  password: string;
}

// Mock credential map  email → { password, role }
const MOCK_CREDENTIALS: Record<string, { password: string; role: 'manager' | 'coach' }> = {
  'manager@gymos.com': { password: 'manager123', role: 'manager' },
  'ivan.boxing@gymos.com': { password: 'coach123', role: 'coach' },
  'maria.swim@gymos.com': { password: 'coach123', role: 'coach' },
  'dmytro.karate@gymos.com': { password: 'coach123', role: 'coach' },
};

@Injectable({ providedIn: 'root' })
export class AuthService {
  private _currentUser = signal<AuthUser | null>(this._loadFromStorage());

  readonly currentUser = this._currentUser.asReadonly();
  readonly isLoggedIn = computed(() => this._currentUser() !== null);
  readonly isManager = computed(() => this._currentUser()?.role === 'manager');
  readonly isCoach = computed(() => this._currentUser()?.role === 'coach');

  constructor(private router: Router) {}

  login(credentials: LoginCredentials): { success: boolean; error?: string } {
    const entry = MOCK_CREDENTIALS[credentials.email.toLowerCase()];

    if (!entry) {
      return { success: false, error: 'No account found with this email.' };
    }
    if (entry.password !== credentials.password) {
      return { success: false, error: 'Incorrect password.' };
    }

    const user = this._buildAuthUser(credentials.email, entry.role);
    if (!user) {
      return { success: false, error: 'User data not found.' };
    }

    this._setUser(user);
    return { success: true };
  }

  logout(): void {
    this._setUser(null);
    this.router.navigate(['/login']);
  }

  /** Role switcher for dev/demo purposes */
  switchRole(role: 'manager' | 'coach'): void {
    const email = role === 'manager' ? 'manager@gymos.com' : 'ivan.boxing@gymos.com';
    const user = this._buildAuthUser(email, role);
    if (user) this._setUser(user);
    this.router.navigate(['/dashboard']);
  }

  private _buildAuthUser(email: string, role: 'manager' | 'coach'): AuthUser | null {
    if (role === 'manager') {
      return {
        id: MOCK_MANAGER.id,
        email: MOCK_MANAGER.email,
        firstName: MOCK_MANAGER.firstName,
        lastName: MOCK_MANAGER.lastName,
        role: 'manager',
        locationId: MOCK_MANAGER.locationId,
      };
    }

    const coach = MOCK_COACHES.find((c) => c.email === email);
    if (!coach) return null;
    return {
      id: coach.id,
      email: coach.email,
      firstName: coach.firstName,
      lastName: coach.lastName,
      role: 'coach',
      coachId: coach.id,
    };
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
