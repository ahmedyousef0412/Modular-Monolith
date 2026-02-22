import { Router } from '@angular/router';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthFacade } from '../../../core/auth/application/auth.facade';
import { RegisterCommand } from '../../../core/auth/application/auth.commands';
import { ToastrService } from 'ngx-toastr';
import { extractErrorMessage } from '../../../core/utils/error-extractor';

@Component({
  selector: 'app-register-component',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  isLoading = signal(false);
  errorMessage = signal<string | null>(null);

  private authFacade = inject(AuthFacade);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private toastr = inject(ToastrService);
  showPassword = signal(false);

  registerForm = this.fb.nonNullable.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  togglePassword() {
    this.showPassword.update((value) => !value);
  }

  onRegister(event: Event) {
    event.preventDefault();

    if (this.registerForm.invalid) return;

    const command = this.registerForm.getRawValue() as RegisterCommand;

    this.authFacade.register(command).subscribe({
      next: () => {
        this.toastr.success('Welcome!', 'Register Success');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.isLoading.set(false);

        const msg = extractErrorMessage(err);
        this.errorMessage.set(msg);
        this.toastr.error(msg, 'Validation Error');
      },
    });
  }
}
