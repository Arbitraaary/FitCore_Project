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
import { ClientService } from '../../../core/services/client.service';
import { MatCard } from '@angular/material/card';

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
  });
  private router = inject(Router);
  private clients = inject(ClientService);

  uniqueEmailValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(this.clients.emailExists(control.value) ? { emailTaken: true } : null);
      }, 300);
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.loading.set(true);
    setTimeout(() => {
      this.clients.create({
        firstName: this.form.value.firstName!,
        lastName: this.form.value.lastName!,
        email: this.form.value.email!,
        phoneNumber: this.form.value.phoneNumber!,
      });
      this.loading.set(false);
      this.success.set(true);
      setTimeout(() => this.router.navigate(['/clients']), 1200);
    }, 600);
  }

  cancel() {
    this.router.navigate(['/clients']);
  }
}
