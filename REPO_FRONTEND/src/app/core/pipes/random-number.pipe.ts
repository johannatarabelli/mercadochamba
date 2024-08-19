import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'randomNumber',
  standalone: true,
})
export class RandomNumberPipe implements PipeTransform {
  transform(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
}
