import { Pipe, PipeTransform } from '@angular/core';
import { Category } from '../models/category.interface';

@Pipe({
  name: 'categoriesToString',
  standalone: true,
})
export class CategoriesToStringPipe implements PipeTransform {
  transform(categories: Category[]): string {
    return categories.map((category) => category.name).join(', ');
  }
}
