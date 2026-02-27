import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { RegisterFormModel } from '../../models/register.model';

@Component({
  selector: 'app-register-page',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register-page.html',
  styleUrls: ['./register-page.css','../../styles/auth.styles.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterPage {

  private readonly fb = inject(FormBuilder);

  protected readonly isSubmitting = signal(false);

  readonly form = this.fb.nonNullable.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
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

    // TODO: Integrate with backend registration endpoint
    console.info('Register payload', payload);
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
