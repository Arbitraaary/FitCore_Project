import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuthService, LoginCredentials } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  loading = signal(false);
  errorMessage = signal('');
  showPassword = signal(false);

  features = [
    { icon: 'group', text: 'Client & coach management' },
    { icon: 'calendar_month', text: 'Session scheduling' },
    { icon: 'sports_gymnastics', text: 'Equipment tracking' },
    { icon: 'bar_chart', text: 'Performance analytics' },
  ];

  // Demo hint credentials
  hints = [
    { role: 'Manager', email: 'manager@example.com', password: 'password321' },
    { role: 'Coach', email: 'coach@example.com', password: 'password123' },
  ];

  fillHint(hint: { email: string; password: string }) {
    this.form.patchValue({ email: hint.email, password: hint.password });
    this.errorMessage.set('');
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');

    const credentials = this.form.getRawValue() as LoginCredentials;

    this.auth.login(credentials).subscribe({
      next: (user) => {
        this.loading.set(false);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.loading.set(false);
        console.error('Full error object:', err);

        if (err.status === 401) {
          this.errorMessage.set('Wrong login or password');
        } else if (err.status === 500) {
          const serverMessage = err.error?.message || 'Server Error (500)';
          this.errorMessage.set(serverMessage);
        } else {
          this.errorMessage.set('Server is unavailable');
        }
      },
    });
  }
}
