import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthFacade } from '../../../core/auth/application/auth.facade';
import { ForgotPasswordCommand } from '../../../core/auth/application/auth.commands';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-forget-password',
  imports: [CommonModule, ReactiveFormsModule,RouterLink],
  templateUrl: './forget-password.html',
  styleUrl: './forget-password.css',
})
export class ForgetPassword {
  private fb = inject(FormBuilder);
  private authFacade = inject(AuthFacade);

emailSent = signal(false);
  forgotForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]]
  });

  onSubmit(): void {
    debugger;
   if (this.forgotForm.valid) {
    const command: ForgotPasswordCommand = {
      email: this.forgotForm.getRawValue().email
    };

     this.authFacade.forgotPassword(command).subscribe({
        next: () => this.emailSent.set(true),
        error: (err) => this.emailSent.set(false)
      });
      // this.emailSent.set(true);
    }
  }
}
