import { Component, computed, effect, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CoachService, CreateCoachDto } from '../../../core/services/coach.service';
import { GymLocation, SpecializationType } from '../../../core/models/types';
import { MatDivider } from '@angular/material/list';
import { MatCard } from '@angular/material/card';
import {firstValueFrom} from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';
import { disabled } from '@angular/forms/signals';
import { ManagerService } from '../../../core/services/manager.service';

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
export class CoachRegisterComponent implements OnInit {
  specializations: SpecializationType[] = ['Box', 'Karate', 'Swim', 'Dance', 'Yoga', 'Stretching'];
  loading = signal(false);
  success = signal(false);
  userLocation = signal<GymLocation | null>(null);
  private fb = inject(FormBuilder);
  form = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(50)]],
    lastName: ['', [Validators.required, Validators.maxLength(50)]],
    email: ['', [Validators.required, Validators.email], [this.uniqueEmailValidator.bind(this)]],
    phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[\d\s\-()]{7,15}$/)]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    specialization: [null as SpecializationType | null, Validators.required],
    locationName: [{ value: '', disabled: true }, [Validators.required]],
  });

  managerSvc = inject(ManagerService);
  private router = inject(Router);
  private coachSvc = inject(CoachService);

  uniqueEmailValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    return firstValueFrom(this.coachSvc.emailExists(control.value)).then((exists) =>
      exists ? { emailTaken: true } : null,
    );
  }

  ngOnInit() {
    this.userLocation.set(this.managerSvc.getMyLocation());
    this.form.controls.locationName.setValue(this.userLocation()?.name ?? '');
  }
  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.loading.set(true);

    const payload: CreateCoachDto = this.form.getRawValue() as CreateCoachDto;

    this.coachSvc.create(payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.success.set(true);
        setTimeout(() => this.router.navigate(['/coaches']), 1500);
      },
      error: (err) => {
        this.loading.set(false);
        console.error('Registration failed:', err);
      },
    });
  }

  cancel() {
    this.router.navigate(['/coaches']);
  }
}
