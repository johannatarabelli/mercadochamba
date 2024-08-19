import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MaterialModule } from '../../../material/material.module';
import { CommonModule } from '@angular/common';
import { ProvinceService } from '../../services/province.service';
import { Province } from '../../models/province.interface';
import { NeighborhoodService } from '../../services/neighborhood.service';
import { Neighborhood } from '../../models/neighborhood.interface';
import { SnackbarService } from '../../../shared/services/snackbar.service';

@Component({
  selector: 'app-neighborhood-page',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule],
  templateUrl: './neighborhood-page.component.html',
  styleUrl: './neighborhood-page.component.scss',
})
export class NeighborhoodPageComponent {
  private fb = inject(FormBuilder);

  private provinceService = inject(ProvinceService);
  provinces: Province[] = [];
  private neighborhoodService = inject(NeighborhoodService);
  neighborhoods: Neighborhood[] = [];
  private snackbarService = inject(SnackbarService);

  formNeighborhood = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(50)]],
    provinceId: [null, Validators.required],
  });

  constructor() {
    this.provinceService.getProvinces().subscribe((response) => {
      this.provinces = response;
    });
    this.neighborhoodService.getNeighborhoods().subscribe((response) => {
      this.neighborhoods = response;
    });
  }

  submit() {
    if (this.formNeighborhood.valid) {
      this.neighborhoodService
        .createNeighborhood(this.formNeighborhood.value as Neighborhood)
        .subscribe((response) => {
          this.snackbarService.openSuccessSnackBar(
            'Barrio creado correctamente'
          );
          this.neighborhoodService.getNeighborhoods().subscribe((response) => {
            this.neighborhoods = response;
          });
          this.formNeighborhood.reset();
        });
    }
  }

  /* delete(id: number) {
    this.neighborhoodService.deleteNeighborhood(id).subscribe(() => {
      this.neighborhoods = this.neighborhoods.filter(
        (neighborhood) => neighborhood.id !== id
      );
      this.snackbarService.openSuccessSnackBar(
        'Barrio eliminado exitosamente'
      );
    });
  } */


}
