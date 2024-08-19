import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import {
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Category, IdCategory } from '../../models/category.interface';
import { ListCardsService } from '../../services/list-cards.service';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [MaterialModule, CommonModule, ReactiveFormsModule],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.scss',
})
export class FiltersComponent implements OnInit {
  private fb = inject(FormBuilder);

  public formSearch = this.fb.group({
    search: [
      this.listCardsService.text,
      [Validators.required, Validators.minLength(3), Validators.maxLength(50)],
    ],
  });

  @Input()
  public categories!: Category[];
  @Input()
  public idSelectedCategory?: IdCategory;

  @Output() selectCategory = new EventEmitter<IdCategory>();

  @Output() search = new EventEmitter<string>();

  constructor(private listCardsService: ListCardsService) {}

  ngOnInit(): void {}

  emitSelectCategory(id: IdCategory) {
    this.selectCategory.emit(id);
  }

  emitSearch() {
    if (this.formSearch.valid) {
      const searchText = this.formSearch.value.search!;
      this.search.emit(searchText);
    }
  }

  disabled(event: Event) {
    event.preventDefault();
  }

  resetText() {
    this.search.emit('');
    this.formSearch.reset();
  }

  onKeyUpReset(){
    if (this.formSearch.value.search?.trim() === '') {
      this.search.emit('');
    }
  }
}
