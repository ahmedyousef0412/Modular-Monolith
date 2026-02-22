import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthFacade } from '../../../core/auth/application/auth.facade';
import { ToastrService } from 'ngx-toastr';
import { extractErrorMessage } from '../../../core/utils/error-extractor';


@Component({
  selector: 'app-login-component',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {


  private fb = inject(FormBuilder);
  private authFacade = inject(AuthFacade);
  private router = inject(Router);
  private toastr = inject(ToastrService);

  isLoading = signal(false);
  errorMessage = signal<string | null>(null);
  showPassword = signal(false);

  togglePassword() {
    this.showPassword.update(value => !value );
  }

  loginForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
    rememberMe: [false]
  });


  onSubmit() {
    if (this.loginForm.invalid) return;

    this.isLoading.set(true);
    this.errorMessage.set(null);

    const loginCommand = this.loginForm.getRawValue();

    this.authFacade.login(loginCommand).subscribe({

      next: () => {
        this.toastr.success('Welcome back!', 'success');
        this.router.navigate(['/register']);
      },
      error: (err) => {
         this.isLoading.set(false);
      
      
      const msg = extractErrorMessage(err);

      this.errorMessage.set(msg);
      this.toastr.error(msg, 'Login Failed');
      
      console.log('Backend message captured:', msg);


      }
    })
  }
}