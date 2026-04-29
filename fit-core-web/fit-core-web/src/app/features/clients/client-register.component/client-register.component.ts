import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { ClientService, CreateClientDto } from '../../../core/services/client.service';
import { MatCard } from '@angular/material/card';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-client-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDividerModule,
    MatCard,
  ],
  templateUrl: './client-register.component.html',
  styleUrls: ['./client-register.component.scss'],
})
export class ClientRegisterComponent {
  loading = signal(false);
  success = signal(false);
  private fb = inject(FormBuilder);
  form = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(50)]],
    lastName: ['', [Validators.required, Validators.maxLength(50)]],
    email: ['', [Validators.required, Validators.email], [this.uniqueEmailValidator.bind(this)]],
    phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[\d\s\-()]{7,15}$/)]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });
  private router = inject(Router);
  private clients = inject(ClientService);

  uniqueEmailValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    return firstValueFrom(this.clients.emailExists(control.value)).then((exists) =>
      exists ? { emailTaken: true } : null,
    );
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.loading.set(true);
    const payload: CreateClientDto = this.form.getRawValue() as CreateClientDto;

    this.clients.create(payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.success.set(true);
        setTimeout(() => this.router.navigate(['/clients']), 1500);
      },
      error: (err) => {
        this.loading.set(false);
        console.error('Registration failed:', err);
      },
    });
  }

  cancel() {
    this.router.navigate(['/clients']);
  }
}
