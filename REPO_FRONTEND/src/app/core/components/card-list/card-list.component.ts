import { Component, inject, Input } from '@angular/core';
import { Card, IdCard } from '../../models/card.interface';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../material/material.module';
import { Router, RouterModule } from '@angular/router';
import { RandomNumberPipe } from "../../pipes/random-number.pipe";

@Component({
  selector: 'app-card-list',
  standalone: true,
  imports: [CommonModule, MaterialModule, RouterModule, RandomNumberPipe],
  templateUrl: './card-list.component.html',
  styleUrl: './card-list.component.scss',
})
export class CardListComponent {
  
  @Input()
  professionals: Card[] = [];

}
