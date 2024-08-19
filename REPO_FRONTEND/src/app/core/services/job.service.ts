import { inject, Injectable } from '@angular/core';
import { Job, JobResponse } from '../models/job.interface';
import { catchError, map, Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { BaseResponse } from '../../shared/models/response.interface';

@Injectable({
  providedIn: 'root',
})
export class JobService {
  private readonly API_URL: string = environment.API_URL;
  jobList: Job[] = [];
  private http = inject(HttpClient);

  constructor() {}

  getJobListPersonal(): Observable<Job[]> {
    const url = `${this.API_URL}/api/Job`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<JobResponse>(url, { headers: headers }).pipe(
      map(({ data, isSuccess }) => {
        if (!isSuccess) throw new Error('Error al encontrar trabajos');
        return data;
      }),
      catchError((error) => {
        console.error('Error al encontrar trabajos:', error);
        return of([])
      })
    );
  }

  postJob(formData: FormData): Observable<BaseResponse> {
    const url = `${this.API_URL}/api/Job`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http
      .post<BaseResponse>(url, formData, { headers: headers })
      .pipe(
        map((response) => {
          return response;
        }),
        catchError((error) => {
          console.error('Error al crear trabajo:', error);
          throw new Error('Error al crear trabajo');
        })
      );
  }

  deleteJob(id: number): Observable<BaseResponse> {
    const url = `${this.API_URL}/api/Job/${id}`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete<BaseResponse>(url, { headers: headers }).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        console.error('Error al eliminar trabajo:', error);
        throw new Error('Error al eliminar trabajo');
      })
    );
  }

  getJobsByUserId(userId: number): Observable<Job[]> {
    const url = `${this.API_URL}/api/Job/user/${userId}`;
    return this.http.get<JobResponse>(url).pipe(
      map(({ data, isSuccess }) => {
        if (!isSuccess) throw new Error('Error al encontrar trabajos');
        return data;
      }),
      catchError((error) => {
        console.error('Error al encontrar trabajos:', error);
        return of([])
      })
    );
  }
}
