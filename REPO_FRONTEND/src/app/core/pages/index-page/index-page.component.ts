import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FooterComponent } from '../../../shared/components/footer/footer.component';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { IndexComponent } from '../../components/index/index.component';
import { CardListComponent } from '../../components/card-list/card-list.component';
import { FiltersComponent } from '../../components/filters/filters.component';
import { ListCardsService } from '../../services/list-cards.service';
import { Card } from '../../models/card.interface';
import { CategoryService } from '../../services/category.service';
import { Category, IdCategory } from '../../models/category.interface';

@Component({
  selector: 'app-index-page',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    IndexComponent,
    FiltersComponent,
    CardListComponent,
  ],
  templateUrl: './index-page.component.html',
  styleUrl: './index-page.component.scss',
})
export class IndexPageComponent {
  public cards: Card[] = [];
  public categories: Category[] = [];
  public idSelectedCategory?: IdCategory;
  public textSearch: string = '';

  constructor(
    private categoryService: CategoryService,
    private listCardService: ListCardsService
  ) {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
    this.idSelectedCategory = this.categoryService.selectedCategory().id;

    this.subscribeToFilteredCards();
  }

  onSelectCategory(id: IdCategory) {
    this.categoryService.selectCategory(id).subscribe((category) => {
      this.idSelectedCategory = category.id;
      this.subscribeToFilteredCards();
    });
  }

  onSearchText(text: string) {
    this.textSearch = text;
    this.listCardService.searchText(text);
    this.subscribeToFilteredCards();
  }

  private subscribeToFilteredCards(): void {
    this.listCardService.filteredCards().subscribe((cards) => {
      this.cards = cards;
    });
  }
}
