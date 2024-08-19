import { Component, inject } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CountryService } from '../../services/country.service';
import { Country } from '../../models/country.inteface';
import { SnackbarService } from '../../../shared/services/snackbar.service';

@Component({
  selector: 'app-country-page',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule],
  templateUrl: './country-page.component.html',
  styleUrl: './country-page.component.scss',
})
export class CountryPageComponent {
  private fb = inject(FormBuilder);
  displayedColumns: string[] = ['name', 'actions'];
  private countryService = inject(CountryService);
  public countries: Country[] = [];
  snackbarService = inject(SnackbarService);

  formCountry = this.fb.group({
    name: [
      '',
      [Validators.required, Validators.minLength(2), Validators.maxLength(50)],
    ],
  });

  constructor() {
    this.countryService.getCountries().subscribe((response) => {
      this.countries = response;
    });
  }

  submit() {
    if (this.formCountry.valid) {
      this.countryService
        .createCountry(this.formCountry.value as Country)
        .subscribe((response) => {
          this.countryService.getCountries().subscribe((response) => {
            this.countries = response;
          });
          this.formCountry.reset();
        });
    }
  }

  deleteCountry(id: number) {
    this.countryService.deleteCountry(id).subscribe(() => {
      this.countries = this.countries.filter((country) => country.id !== id);
      this.snackbarService.openSuccessSnackBar('País eliminado exitosamente');
    });
  }
}
