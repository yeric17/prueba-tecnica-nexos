import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { RegisterFormModel } from '../../models/register.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-page',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register-page.html',
  styleUrls: ['./register-page.css','../../styles/auth.styles.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterPage {

  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  protected readonly isSubmitting = signal(false);

  readonly form = this.fb.nonNullable.group({
    userName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', [Validators.required]]
  });

  protected register(): void {
    if (this.form.invalid || this.passwordsDoNotMatch()) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    const { confirmPassword, ...payload } = this.form.getRawValue();

    this.authService.register(payload)
    .subscribe({
      next: () => {
        this.form.reset();
        this.isSubmitting.set(false);
        this.router.navigate(['/auth/login']);
      },
      error: (err) => {
        console.error('Error durante el registro:', err);
        this.isSubmitting.set(false);
      }
    })
    
    this.isSubmitting.set(false);
  }

  protected controlInvalid(controlName: keyof RegisterFormModel): boolean {
    const control = this.form.controls[controlName];
    return control.invalid && (control.dirty || control.touched);
  }

  protected passwordsDoNotMatch(): boolean {
    const { password, confirmPassword } = this.form.getRawValue();
    return password !== confirmPassword;
  }



}
