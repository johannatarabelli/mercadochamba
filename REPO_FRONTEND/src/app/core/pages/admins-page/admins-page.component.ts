import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MaterialModule } from '../../../material/material.module';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { numericValidator } from '../../../auth/models/ndocumento.validators';
import { passwordValidator } from '../../../auth/models/password.validators';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import { AdminService } from '../../services/admin.service';
import { UserAdmin } from '../../../auth/models/user.interface';

@Component({
  selector: 'app-admins-page',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './admins-page.component.html',
  styleUrl: './admins-page.component.scss',
})
export class AdminsPageComponent {
  public hide = true;
  private fb = inject(FormBuilder);
  private adminService = inject(AdminService);
  private snackbarService = inject(SnackbarService);
  public errors = [];
  public admins: UserAdmin[] = [];
  displayedColumns: string[] = [
    'userName',
    'firstName',
    'lastName',
    'email',
    'actions',
  ];

  constructor() {
    this.adminService.getAdmins().subscribe((response) => {
      this.admins = response;
    });
  }

  public formAdmin = this.fb.group({
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
    password: ['', [Validators.required, passwordValidator()]],

    // countryId : [''],
    // provinceId: [''],
    // neighborhoodId : [''],
  });

  submit() {
    if (this.formAdmin.valid) {
      const admin = this.formAdmin.value;
      this.adminService.register(admin as UserAdmin).subscribe(
        (response) => {
          if (response !== null) {
            this.snackbarService.openSuccessSnackBar(
              'Administrador registrado exitosamente'
            );
            this.formAdmin.reset();
            this.adminService.getAdmins().subscribe((response) => {
              this.admins = response;
            });
          } else {
            this.snackbarService.openErrorSnackBar(
              'Error al registrar el administrador'
            );
          }
        },
        (error) => {
          this.errors = error.error.errors;
        }
      );
    }
  }

  onDelete(id: number) {
    this.adminService.delete(id).subscribe((response) => {
      this.snackbarService.openSuccessSnackBar(
        'Administrador eliminado exitosamente'
      );
      this.adminService.getAdmins().subscribe((response) => {
        this.admins = response;
      });
    });
  }
}
