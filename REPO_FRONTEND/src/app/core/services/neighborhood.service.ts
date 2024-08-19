import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Neighborhood } from '../models/neighborhood.interface';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NeighborhoodService {
  private readonly API_URL: string = environment.API_URL;
  private http = inject(HttpClient);
  constructor() {}

  getNeighborhoods(): Observable<Neighborhood[]> {
    const url = `${this.API_URL}/api/Neighborhood`;
    return this.http.get<Neighborhood[]>(url).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        return of([]);
      })
    );
  }

  /* deleteNeighborhood(id: number): Observable<boolean> {
    const url = `${this.API_URL}/api/Neighborhood/${id}`;
    return this.http.delete(url).pipe(
      map((response) => {
        return true;
      }),
      catchError((error) => {
        return of(false);
      })
    );
  } */

  createNeighborhood(neighborhood: Neighborhood): Observable<boolean> {
    const url = `${this.API_URL}/api/Neighborhood`;
    return this.http.post(url, neighborhood).pipe(
      map((response) => {
        return true;
      }),
      catchError((error) => {
        return of(false);
      })
    );
  }

}
