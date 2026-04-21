import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CoachService } from '../../../core/services/coach.service';
import { SpecializationType } from '../../../core/models/types';
import { MatDivider } from '@angular/material/list';
import { MatCard } from '@angular/material/card';

@Component({
  selector: 'app-coach-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDivider,
    MatCard,
  ],
  templateUrl: './coach-register.component.html',
  styleUrls: ['./coach-register.component.scss'],
})
export class CoachRegisterComponent {
  specializations: SpecializationType[] = ['Box', 'Karate', 'Swim'];
  loading = signal(false);
  success = signal(false);
  private fb = inject(FormBuilder);
  form = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(50)]],
    lastName: ['', [Validators.required, Validators.maxLength(50)]],
    email: ['', [Validators.required, Validators.email], [this.uniqueEmailValidator.bind(this)]],
    phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[\d\s\-()]{7,15}$/)]],
    specialization: [null as SpecializationType | null, Validators.required],
  });
  private router = inject(Router);
  private coaches = inject(CoachService);

  uniqueEmailValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(this.coaches.emailExists(control.value) ? { emailTaken: true } : null);
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
      this.coaches.create({
        firstName: this.form.value.firstName!,
        lastName: this.form.value.lastName!,
        email: this.form.value.email!,
        phoneNumber: this.form.value.phoneNumber!,
        specialization: this.form.value.specialization!,
      });
      this.loading.set(false);
      this.success.set(true);
      setTimeout(() => this.router.navigate(['/coaches']), 1200);
    }, 600);
  }

  cancel() {
    this.router.navigate(['/coaches']);
  }
}
