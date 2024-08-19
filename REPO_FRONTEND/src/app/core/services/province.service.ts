import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { catchError, map, Observable, of } from 'rxjs';
import { Province } from '../models/province.interface';

@Injectable({
  providedIn: 'root',
})
export class ProvinceService {
  private readonly API_URL: string = environment.API_URL;
  private http = inject(HttpClient);
  constructor() {}

  getProvinces(): Observable<Province[]> {
    const url = `${this.API_URL}/api/Province`;
    return this.http.get<Province[]>(url).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        return of([]);
      })
    );
  }

  deleteProvince(id: number): Observable<boolean> {
    const url = `${this.API_URL}/api/Province/${id}`;
    return this.http.delete(url).pipe(
      map((response) => {
        return true;
      }),
      catchError((error) => {
        return of(false);
      })
    );
  }

  createProvince(province: Province): Observable<boolean> {
    const url = `${this.API_URL}/api/Province`;
    return this.http.post(url, province).pipe(
      map((response) => {
        return true;
      }),
      catchError((error) => {
        return of(false);
      })
    );
  }
}
