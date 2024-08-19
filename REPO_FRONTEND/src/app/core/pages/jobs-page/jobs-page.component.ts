import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import { CommonModule } from '@angular/common';
import { JobService } from '../../services/job.service';
import { Job } from '../../models/job.interface';

@Component({
  selector: 'app-jobs-page',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule],
  templateUrl: './jobs-page.component.html',
  styleUrl: './jobs-page.component.scss',
})
export class JobsPageComponent {
  private fb = inject(FormBuilder);
  private snackbarService = inject(SnackbarService);
  public imagePreview: string | ArrayBuffer | null = null;
  public jobs: Job[] = [];
  displayedColumns: string[] = ['title', 'description', 'image', 'actions'];
  private jobService = inject(JobService);

  @ViewChild('fileInput') fileInput!: ElementRef;

  constructor() {
    this.jobService.getJobListPersonal().subscribe(
      (jobs) => {
        this.jobs = jobs.reverse();
      },
      (error) => {
        this.snackbarService.openErrorSnackBar(error.error.message);
      }
    );
  }

  formJob = this.fb.group({
    Title: ['', [Validators.required]],
    Description: ['', [Validators.required]],
    Image: [null, [Validators.required]],
  });

  onFileChange(event: any) {
    const file = event.target.files[0];
  
    if (file) {
      if (file.type.startsWith('image/')) {
        const reader = new FileReader();
  
        reader.onload = () => {
          this.imagePreview = reader.result as string;
          this.formJob.controls.Image.setValue(file);
          this.formJob.controls.Image.updateValueAndValidity(); 
        };
  
        reader.readAsDataURL(file);
      } else {
        // Si no es una imagen, limpia la vista previa y establece un error
        this.imagePreview = null;
        this.formJob.controls.Image.setValue(null); // Limpia el valor
        this.formJob.controls.Image.setErrors({ invalidType: true });
      }
    } else {
      
      this.imagePreview = null;
      this.formJob.controls.Image.setValue(null); // Limpia el valor
      this.formJob.controls.Image.setErrors({ required: true });
    }
  }

  onSubmit() {
    if (this.formJob.valid) {
      const formData = new FormData();
      Object.keys(this.formJob.controls).forEach((formControlName) => {
        formData.append(
          formControlName,
          this.formJob.get(formControlName)!.value
        );
      });

      this.jobService.postJob(formData).subscribe(
        (response) => {
          if (response.isSuccess) {
            this.snackbarService.openSuccessSnackBar('Trabajo creado');
            this.imagePreview = null;
            this.formJob.reset();
            this.fileInput.nativeElement.value = '';

            this.jobService.getJobListPersonal().subscribe(
              (jobs) => {
                this.jobs = jobs.reverse();
              },
              (error) => {
                this.snackbarService.openErrorSnackBar(error.error.message);
              }
            );
          }
        },
        (error) => {
          this.snackbarService.openErrorSnackBar(error.error.message);
        }
      );
    }
  }

  onDeleteJob(job: Job) {
    this.jobService.deleteJob(job.id).subscribe(
      (response) => {
        if (response.isSuccess) {
          this.snackbarService.openSuccessSnackBar('Trabajo eliminado');
          this.jobs = this.jobs.filter((j) => j.id !== job.id);
        }
      },
      (error) => {
        this.snackbarService.openErrorSnackBar(error.error.message);
      }
    );
  }
}
