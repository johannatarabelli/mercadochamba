import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResponse, Person, Review } from '../models/review.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private apiUrl = 'https://fakerapi.it/api/v1/custom'; // Endpoint para solicitud personalizada

  constructor(private http: HttpClient) {}

  getReviews(count: number): Observable<Review[]> {
    const url = `${this.apiUrl}?_quantity=${count}&name=name&date=date&text=text&image=image`;
    return this.http.get<ApiResponse>(url).pipe(
      map(response => response.data.map(person => this.transformToReview(person)))
    );
  }

  private transformToReview(person: Person): Review {
    
    const date = new Date(person.date).toLocaleDateString(); 
    const stars = Math.floor(Math.random() * 5) + 1;
    const message = person.text;

    return {
      name: person.name, 
      date: date,
      stars: stars,
      message: message,
      photoUrl: person.image
    };
  }
}
