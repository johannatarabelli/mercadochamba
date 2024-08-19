import { inject, Injectable } from '@angular/core';

import { catchError, map, Observable, of } from 'rxjs';
import { CategoryService } from './category.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Card, CardResponse, CardsResponse } from '../models/card.interface';

@Injectable({
  providedIn: 'root',
})
export class ListCardsService {
  private readonly API_URL: string = environment.API_URL;
  private _text: string = '';
  private http = inject(HttpClient);
  public listCards: Card[] = [];

  constructor(private categoryService: CategoryService) {}

  public get text(): string {
    return this._text;
  }

  public searchText(text: string): void {
    this._text = text;
  }

  getLisCards(): Observable<Card[]> {
    const url = `${this.API_URL}/api/Profile/AllProfiles`;
    return this.http.get<CardsResponse>(url).pipe(
      map(({ data, isSuccess }) => {
         if (!isSuccess) throw new Error('Error los perfiles');
        return data;
      }),
      catchError((error) => {
        console.error('Error:', error);
        return of([]);
      })
    );
  }
  getListCardsByCategory(id: number): Observable<Card[]> {
    const url = `${this.API_URL}/api/Profile/category/${id}`;
    return this.http.get<CardsResponse>(url).pipe(
      map(({ data, isSuccess }) => {
        if (!isSuccess) throw new Error('Error los perfiles');
        return data;
      }),
      catchError((error) => {
        console.error('Error:', error);
        return of([]);
      })
    );
  }

  public filteredCards(): Observable<Card[]> {
    const category = this.categoryService.selectedCategory().name;
    const text = this._text;

    const updateCategoriesString = (cards: Card[]) => {
      cards.forEach((card) => {
        if (card.categories) {
          card.categoriesString = card.categories
            .map((category) => category.name)
            .join(', ');
        }
      });
    };

    const cards$ =
      category === 'Todos'
        ? this.getLisCards()
        : this.getListCardsByCategory(
            this.categoryService.selectedCategory().id
          );

    return cards$.pipe(
      map((cards) => {
        updateCategoriesString(cards);

        return cards.filter((card) => {
          const matchesText =
            this.normalizeText(card.specialty).includes(
              this.normalizeText(text)
            ) ||
            card.categories?.some((cat) =>
              this.normalizeText(cat.name).includes(this.normalizeText(text))
            );

          const matchesCategory =
            category === 'Todos' ||
            card.categories?.some((cat) => cat.name === category);

          return matchesText && matchesCategory;
        });
      })
    );
  }

  private normalizeText(text: string): string {
    return text
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .toLowerCase();
  }
}
