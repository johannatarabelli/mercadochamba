import { computed, inject, Injectable, signal } from '@angular/core';

import { catchError, map, Observable, of, tap } from 'rxjs';
import {
  Category,
  CategoryResponse,
  IdCategory,
} from '../models/category.interface';
import { environment } from '../../../environments/environment';
import { BaseResponse } from '../../shared/models/response.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private http = inject(HttpClient);
  private readonly API_URL: string = environment.API_URL;
  private _selectedCategory: Category = {
    id: 0,
    name: 'Todos',
    icon: 'public',
  };
  
  private categoriesDefault: Category[] = [
    { id: 0, name: 'Todos', icon: 'public' },
    { id: 1, name: 'Refrigeración', icon: 'ac_unit' },
    { id: 2, name: 'Electricidad', icon: 'electric_bolt' },
    { id: 3, name: 'Vidrios', icon: 'window' },
    { id: 4, name: 'Plomería', icon: 'build' },
    { id: 5, name: 'Carpintería', icon: 'carpenter' },
    { id: 6, name: 'Pintura', icon: 'format_paint' },
    { id: 7, name: 'Mecánica', icon: 'car_crash' },
    { id: 8, name: 'Programador', icon: 'code' }, 
    { id: 9, name: 'Profesor', icon: 'school' }, 
    { id: 10, name: 'Electricista', icon: 'electric_bolt' }, 
    { id: 11, name: 'Plomero', icon: 'plumbing' },
    { id: 12, name: 'Odontólogo', icon: 'local_hospital' }, 
    { id: 13, name: 'Contador', icon: 'attach_money' }, 
    { id: 14, name: 'Abogado', icon: 'gavel' } 
  ];

  getCategories(): Observable<Category[]> {
    const url = `${this.API_URL}/api/Category`;
    return this.http.get<CategoryResponse>(url).pipe(
      map((response) => {
        if (!response.isSuccess) return [];
        return [
          this.categoriesDefault[0],
          ...response.data.map(this.mapCategoryIcon.bind(this)),
        ];
      })
    );
  }

  public selectCategory(id: IdCategory): Observable<Category> {
    return this.getCategories().pipe(
      map((categories) => categories.find((category) => category.id === id)!),
      tap((category) => this._selectedCategory = category)
    );
  }

  public selectedCategory(): Category {
    return this._selectedCategory;
  }

  private mapCategoryIcon(category: Category): Category {
    const defaultCategory = this.categoriesDefault.find(
      (cat) =>
        this.normalizeText(cat.name) === this.normalizeText(category.name)
    );
    return {
      ...category,
      icon: defaultCategory ? defaultCategory.icon : 'build',
    };
  }

  private normalizeText(text: string): string {
    return text
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .toLowerCase();
  }
}
