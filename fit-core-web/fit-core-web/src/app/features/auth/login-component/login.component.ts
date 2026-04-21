import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuthService } from '../../../core/services/auth.service';

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
    { role: 'Manager', email: 'manager@gymos.com', password: 'manager123' },
    { role: 'Coach', email: 'ivan.boxing@gymos.com', password: 'coach123' },
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

    // Simulate async (500 ms)
    setTimeout(() => {
      const { email, password } = this.form.value;
      const result = this.auth.login({ email: email!, password: password! });

      if (result.success) {
        this.router.navigate(['/dashboard']);
      } else {
        this.errorMessage.set(result.error ?? 'Login failed.');
      }

      this.loading.set(false);
    }, 500);
  }
}
