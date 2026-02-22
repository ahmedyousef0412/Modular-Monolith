import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthFacade } from '../../../core/auth/application/auth.facade';
import { ResetPasswordCommand } from '../../../core/auth/application/auth.commands';
import { Router } from '@angular/router';


@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './reset-password.html',
  styleUrls: ['./reset-password.css']
})
export class ResetPassword implements OnInit {
  private fb = inject(FormBuilder);
  private authFacade = inject(AuthFacade);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  showPassword = signal(false);
  email = '';
  token = '';

  // Form only contains password fields as requested
  resetForm = this.fb.nonNullable.group({
    newPassword: ['', [Validators.required, Validators.minLength(6)]],
    confirmNewPassword: ['', [Validators.required]]
  }, { 
    validators: this.passwordMatchValidator 
  });

  ngOnInit() {
    // These come from the email link: ?email=...&token=...
    this.email = this.route.snapshot.queryParams['email'] || '';
    this.token = this.route.snapshot.queryParams['token'] || '';
  }

  private passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('newPassword')?.value;
    const confirm = control.get('confirmNewPassword')?.value;
    return password === confirm ? null : { passwordMismatch: true };
  }

  togglePassword() {
    this.showPassword.update(v => !v);
  }
errorMessage = signal<string | null>(null);
  onReset() {
    if (this.resetForm.valid && this.token && this.email) {
      const formValues = this.resetForm.getRawValue();
      
      
      const command : ResetPasswordCommand = {
        email: this.email,
        token: this.token,
        newPassword: formValues.newPassword,
        confirmNewPassword: formValues.confirmNewPassword
      };

      this.authFacade.resetPassword(command).subscribe({
        next: () => this.router.navigate(['/login']),
        error: (err) => this.errorMessage = err.error
      });
    }
  }
}