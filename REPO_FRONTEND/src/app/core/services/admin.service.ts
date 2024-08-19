import { inject, Injectable } from '@angular/core';
import {
  UserAdmin,
  UserAdminResponse,
  UserRegister,
} from '../../auth/models/user.interface';
import { BaseResponse } from '../../shared/models/response.interface';
import { environment } from '../../../environments/environment';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private readonly API_URL: string = environment.API_URL;
  private http = inject(HttpClient);
  constructor() {}

  register(userRegister: UserAdmin): Observable<UserAdmin | null> {
    const url = `${this.API_URL}/api/Administrador`;
    const body = userRegister;
    return this.http.post<UserAdminResponse>(url, body).pipe(
      map((response) => {
        if (response.id) {
          return response;
        }
        return null;
      }),
      catchError((error) => {
        return of(null);
      })
    );
  }

  getAdmins(): Observable<UserAdmin[]> {
    const url = `${this.API_URL}/api/Administrador`;
    return this.http.get<UserAdminResponse[]>(url).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        return of([]);
      })
    );
  }

  delete(id: number) {
    const url = `${this.API_URL}/api/Administrador/${id}`;
    return this.http.delete(url);
  }
}
