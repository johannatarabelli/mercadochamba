import { Pipe, PipeTransform } from '@angular/core';
import { Neighborhood } from '../models/neighborhood.interface';


@Pipe({
  name: 'neighborhoodsToString',
  standalone: true
})
export class NeighborhoodsToStringPipe implements PipeTransform {

  transform(neighborhoods: Neighborhood[]): string {
    return neighborhoods.map((neighborhood) => neighborhood.name).join(', ');
  }

}
