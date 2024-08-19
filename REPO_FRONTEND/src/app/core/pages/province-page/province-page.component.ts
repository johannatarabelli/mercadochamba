import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MaterialModule } from '../../../material/material.module';
import { CommonModule } from '@angular/common';
import { Country } from '../../models/country.inteface';
import { CountryService } from '../../services/country.service';
import { ProvinceService } from '../../services/province.service';
import { Province } from '../../models/province.interface';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import { NeighborhoodsToStringPipe } from '../../pipes/neighborhoods-to-string.pipe';

@Component({
  selector: 'app-province-page',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule, NeighborhoodsToStringPipe],
  templateUrl: './province-page.component.html',
  styleUrl: './province-page.component.scss',
})
export class ProvincePageComponent {
  private fb = inject(FormBuilder);
  private countryService = inject(CountryService);
  countries: Country[] = [];
  private provinceService = inject(ProvinceService);
  provinces: Province[] = [];
  snackbarService = inject(SnackbarService);

  formProvince = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(50)]],
    countryId: [null, Validators.required],
  });

  constructor() {
    this.countryService.getCountries().subscribe((countries) => {
      this.countries = countries;
    });
    this.provinceService.getProvinces().subscribe((provinces) => {
      this.provinces = provinces;
    });
  }

  delete(id: number) {
    this.provinceService.deleteProvince(id).subscribe(() => {
      this.provinces = this.provinces.filter((province) => province.id !== id);
      this.snackbarService.openSuccessSnackBar(
        'Provincia eliminada exitosamente'
      );
    });
  }

  submit() {
    if (this.formProvince.valid) {
      this.provinceService
        .createProvince(this.formProvince.value as Province)
        .subscribe((province) => {
          this.provinceService.getProvinces().subscribe((provinces) => {
            this.provinces = provinces;
          });
          this.snackbarService.openSuccessSnackBar(
            'Provincia registrada exitosamente'
          );
          this.formProvince.reset();
        });
    }
  }
}
