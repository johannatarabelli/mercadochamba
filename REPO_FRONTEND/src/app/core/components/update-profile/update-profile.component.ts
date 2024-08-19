import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MaterialModule } from '../../../material/material.module';
import { CommonModule } from '@angular/common';
import { Category } from '../../models/category.interface';
import { CategoryService } from '../../services/category.service';
import { ProfileService } from '../../services/profile.service';
import { Profile } from '../../models/profile.interface';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import { Router } from '@angular/router';
import { CategoriesToStringPipe } from '../../pipes/categories-to-string.pipe';
import { imageValidator } from '../../models/image.validators';

@Component({
  selector: 'app-update-profile',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule, CategoriesToStringPipe],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.scss',
})
export class UpdateProfileComponent {
  private fb = inject(FormBuilder);
  private snackbarService = inject(SnackbarService);
  private router = inject(Router);
  public categories: Category[] = [];
  private profileService = inject(ProfileService);
  public profile?: Profile;
  public categoryService = inject(CategoryService);
  public imagePreview: string | ArrayBuffer | null = null;

  formProfile = this.fb.group({
    Specialty: ['', [Validators.required]],
    Experience: ['', [Validators.required]],
    Description: ['', [Validators.required]],
    Image: [null, [Validators.required, imageValidator()]],
    CategoryIds: [[], [Validators.required]],
  });

  constructor() {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories.slice(1);
    });
    this.profileService.getCurrentProfile().subscribe((profile) => {
      this.profile = profile;
    });
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    
    if (file) {
      if (file.type.startsWith('image/')) {
        const reader = new FileReader();
        
        reader.onload = () => {
          this.imagePreview = reader.result as string;
          this.formProfile.controls.Image.setValue(file);
          this.formProfile.controls.Image.updateValueAndValidity();
        };
        
        reader.readAsDataURL(file);
      } else {
        this.imagePreview = null;
        this.formProfile.controls.Image.setValue(null); 
        this.formProfile.controls.Image.setErrors({ invalidType: true });
      }
    }
  }

  onSubmit() {
    if (this.formProfile.valid) {
      console.log(this.formProfile.value);
      const formData = new FormData();

      Object.keys(this.formProfile.controls).forEach((formControlName) => {
        const control = this.formProfile.get(formControlName);
        if (formControlName === 'CategoryIds') {
          const categoryIds = control?.value || [];
          categoryIds.forEach((id: string | number) => {
            formData.append(`${formControlName}[]`, id.toString());
          });
        } else {
          formData.append(
            formControlName,
            control ? control.value : ''
          );
        }
      });


      this.profileService.updateProfile(formData).subscribe({
        next: (response) => {
          if (response.isSuccess) location.reload();
          if (!response.isSuccess)
            this.snackbarService.openErrorSnackBar(response.message!);
        },
        error: (error) => console.log('Error', error),
      });
    }
  }
}
