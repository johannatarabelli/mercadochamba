import { Component, inject, OnInit } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserLogin } from '../../models/user.interface';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import { passwordValidator } from '../../models/password.validators';

@Component({
  selector: 'auth-login',
  standalone: true,
  imports: [MaterialModule, RouterModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  public hide = true;
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private snackbarService = inject(SnackbarService);

  public formLogin = this.fb.group({
    userName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
    password: ['', [Validators.required, passwordValidator()]],
  });

  constructor() {}

  ngOnInit(): void {
    this.authService.checkAuthStatus().subscribe({
      next: (response) => {
        if (response) this.router.navigateByUrl('/dashboard');
      },
    });
  }

  submit() {
    if (this.formLogin.valid) {
      const userLogin: UserLogin = this.formLogin.value;
      this.authService.login(userLogin).subscribe({
        next: (response) => {
          if (response.isSuccess) this.router.navigateByUrl('/dashboard');
          if (!response.isSuccess)
            this.snackbarService.openErrorSnackBar(response.message!);
        },
        error: (error) => console.log('Login error', error),
      });
    }
  }
}
