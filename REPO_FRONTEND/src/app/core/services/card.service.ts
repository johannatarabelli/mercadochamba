import { inject, Injectable } from '@angular/core';
import { ListCardsService } from './list-cards.service';
import { Card, CardResponse, IdCard } from '../models/card.interface';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class CardService {
  private readonly API_URL: string = environment.API_URL;
  private http = inject(HttpClient);

  constructor() {}

  getCardById(id: IdCard) {
    const url = `${this.API_URL}/api/Profile/ByUserId/${id}`;
    return this.http.get<CardResponse>(url).pipe(
      (response) => {
        return response;
      },
      (error) => {
        return error;
      }
    );
  }
}
