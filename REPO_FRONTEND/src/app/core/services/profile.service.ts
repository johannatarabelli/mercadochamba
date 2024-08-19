import { inject, Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';
import { BaseResponse } from '../../shared/models/response.interface';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Profile, ProfileResponse } from '../models/profile.interface';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private readonly API_URL: string = environment.API_URL;
  private http = inject(HttpClient);
  constructor() {}

  /* updateProfile(): Observable<BaseResponse>{
    
  } */

  updateProfile(formData: FormData): Observable<BaseResponse> {
    const url = `${this.API_URL}/api/Profile`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`);
    return this.http.put<BaseResponse>(url, formData, { headers }).pipe(
      map(({ data, message, status, isSuccess }) => {
        if (!isSuccess) throw new Error('Error al actualizar el perfil');
        return { data, message, status, isSuccess };
      }),
      catchError((error) => {
        console.error('Error al actualizar el perfil:', error);
        throw new Error('Error al actualizar el perfil');
      })
    );
  }

  getCurrentProfile(): Observable<Profile> {
    const url = `${this.API_URL}/api/Profile/CurrentUserProfile`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<ProfileResponse>(url, { headers }).pipe(
      map(({ data, isSuccess }) => {
        if (!isSuccess) throw new Error('Error al obtener el perfil');
        return data;
      })
    );
  }
}
