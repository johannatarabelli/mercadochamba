import { Component } from '@angular/core';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { MaterialModule } from '../../../material/material.module';
import { CommonModule } from '@angular/common';
import { CardService } from '../../services/card.service';

import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Card, IdCard } from '../../models/card.interface';
import { CategoriesToStringPipe } from '../../pipes/categories-to-string.pipe';
import { Job } from '../../models/job.interface';
import { JobService } from '../../services/job.service';
import { FooterComponent } from '../../../shared/components/footer/footer.component';
import { MatDialog } from '@angular/material/dialog';
import { JobDialogComponent } from '../../components/job-dialog/job-dialog.component';
import { Review } from '../../models/review.interface';
import { ReviewService } from '../../services/review.service';

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    HeaderComponent,
    RouterModule,
    CategoriesToStringPipe,
    FooterComponent,
  ],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.scss',
})
export class ProfilePageComponent {
  public reviews: number = 0;
  public reviewsArray: number[] = [];
  public stars: number = 0;
  public card: Card | null = null;
  public jobs: Job[] = [];
  public reviewsFake: Review[] = [];


  constructor(
    private cardService: CardService,
    private jobService: JobService,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private reviewService: ReviewService,
  ) {
    this.reviews = this.generateRandomNumber(1,20);
    this.stars = this.generateRandomNumber(1, 5);
    this.reviewsArray = this.generateReviews();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.loadCard(Number(id));
      } else {
        this.router.navigate(['/']);
      }
    });
    this.reviewService.getReviews(this.reviews).subscribe(reviews => {
      this.reviewsFake = reviews;
    });
  }

  private loadCard(id: IdCard): void {
    const card = this.cardService.getCardById(id).subscribe(
      (response) => {
        this.card = response.data;
      },
      (error) => {
        console.error('Error loading card:', error);
        this.router.navigate(['/']);
      }
    );
    const jobs = this.jobService.getJobsByUserId(id).subscribe(
      (response) => {
        this.jobs = response;
      },
      (error) => {
        console.error('Error loading jobs:', error);
      }
    );
  }

  openDialog(job: any): void {
    this.dialog.open(JobDialogComponent, {
      width: '400px',
      data: job,
    });
  }

  generateRandomNumber(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  generateReviews(): number[] {
    return Array.from({ length: this.reviews }, (_, i) => i + 1);
  }
}
