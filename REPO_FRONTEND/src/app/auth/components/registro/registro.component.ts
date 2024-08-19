import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../material/material.module';
import { Component, inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserRegister } from '../../models/user.interface';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import { passwordValidator } from '../../models/password.validators';
import { numericValidator } from '../../models/ndocumento.validators';

@Component({
  selector: 'auth-registro',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.scss',
})
export class RegistroComponent implements OnInit {
  public hide = true;
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private snackbarService = inject(SnackbarService);
  public errors = [];

  ngOnInit(): void {
    this.authService.checkAuthStatus().subscribe({
      next: (response) => {
        if (response) this.router.navigateByUrl('/dashboard');
      },
    });
  }

  public formRegister = this.fb.group({
    userName: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(50)],
    ],
    firstName: [
      '',
      [Validators.required, Validators.minLength(1), Validators.maxLength(50)],
    ],
    lastName: [
      '',
      [Validators.required, Validators.minLength(1), Validators.maxLength(50)],
    ],
    email: [
      '',
      [Validators.required, Validators.email, Validators.maxLength(100)],
    ],
    dni: [
      '',
      [Validators.required, Validators.minLength(7), Validators.maxLength(15), numericValidator()],
    ],
    phoneNumber: [
      '',
      [Validators.required, Validators.minLength(7), Validators.maxLength(15)],
    ],
    password: ['', [Validators.required, passwordValidator()]],
    address: [
      '',
      [Validators.required, Validators.minLength(5), Validators.maxLength(100)],
    ],
    // countryId : [''],
    // provinceId: [''],
    // neighborhoodId : [''],
  });

  submit() {
    if (this.formRegister.valid) {
      const userRegister: UserRegister = this.formRegister.value;
      this.authService.register(userRegister).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.snackbarService.openSuccessSnackBar(
              'Usuario registrado correctamente'
            );
            this.router.navigateByUrl('/auth/login');
          }
        },
        error: (error) => {
          this.snackbarService.openErrorSnackBar('Usuario no registrado');
          this.errors = error;
        },
      });
    }
  }
}
