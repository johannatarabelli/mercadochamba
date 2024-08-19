import { inject, Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { Country } from '../models/country.inteface';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private readonly API_URL: string = environment.API_URL;
  private http = inject(HttpClient);
  constructor() { }

  getCountries(): Observable<Country[]> {
    const url = `${this.API_URL}/api/Country`;
    return this.http.get<Country[]>(url).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        return of([]);
      })
    );
  }

  createCountry(country: Country): Observable<Country | null> {
    const url = `${this.API_URL}/api/Country`;
    return this.http.post<Country>(url, country).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        return of(null);
      })
    );
  }

  deleteCountry(id: number): Observable<boolean> {
    const url = `${this.API_URL}/api/Country/${id}`;
    return this.http.delete(url).pipe(
      map((response) => {
        return true;
      }),
      catchError((error) => {
        return of(false);
      })
    );
  }
}
