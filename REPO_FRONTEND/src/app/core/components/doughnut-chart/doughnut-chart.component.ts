import { Component, inject, OnInit } from '@angular/core';
import { ChartData, ChartOptions } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Category } from '../../models/category.interface';
import { CategoryService } from '../../services/category.service';
import { ListCardsService } from '../../services/list-cards.service';
import { Card } from '../../models/card.interface';

@Component({
  selector: 'app-doughnut-chart',
  standalone: true,
  imports: [BaseChartDirective],
  templateUrl: './doughnut-chart.component.html',
  styleUrl: './doughnut-chart.component.scss',
})
export class DoughnutChartComponent {
  public categories: Category[] = [];
  public listCards: Card[] = [];
  private categoryService = inject(CategoryService);
  private listCardsService = inject(ListCardsService);

  public doughnutChartData: ChartData<'doughnut'> = {
    labels: [],
    datasets: [
      {
        data: [],
        backgroundColor: [],
        hoverBackgroundColor: [],
      },
    ],
  };

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories.slice(1);
      this.updateChartData();
    });

    this.listCardsService.getLisCards().subscribe((cards) => {
      this.listCards = cards;
      this.updateChartData();
    });
  }

  private updateChartData(): void {
    this.doughnutChartData = {
      labels: this.categories.map((category) => category.name),
      datasets: [
        {
          data: this.categories.map((category) => {
            const count = this.listCards.filter((card) =>
              card.categories?.some((cat) => cat.name === category.name)
            ).length;

            return count;
          }),
          backgroundColor: this.getRandomColors(this.categories.length),
          hoverBackgroundColor: this.getRandomColors(this.categories.length),
        },
      ],
    };
  }

  public doughnutChartOptions: ChartOptions<'doughnut'> = {
    responsive: true,
    plugins: {
      legend: {
        title: {
          display: true,
          text: 'Categorías',
        },
      },
      tooltip: {
        callbacks: {
          label: (tooltipItem) => {
            const dataset = tooltipItem.dataset as any;
            const value = dataset.data[tooltipItem.dataIndex];
            return `${dataset.label}: ${value}`;
          },
        },
      },
    },
  };

  public doughnutChartLegend = false;
  public doughnutChartPlugins = [];
  public doughnutChartType: 'doughnut' = 'doughnut';

  private getRandomColors(count: number): string[] {
    const colors = [
      '#FF6384',  // Red
      '#36A2EB',  // Blue
      '#FFCE56',  // Yellow
      '#4BC0C0',  // Teal
      '#9966FF',  // Purple
      '#FF9F40',  // Orange
      '#E7E9ED',  // Light Gray
      '#FFCD56',  // Light Yellow
      '#36A2A2',  // Dark Teal
      '#FF6633',  // Light Orange
      '#C2C2C2',  // Medium Gray
      '#FFB3E6',  // Light Pink
      '#99FF99',  // Light Green
      '#FFB366',  // Light Peach
      '#C2F0C2',  // Light Mint
      '#D9B3E6',  // Light Purple
      '#F2B5D4',  // Light Rose
      '#C9DAF8',  // Light Blue
      '#F6BC0B',  // Golden Yellow
      '#A2C2E3',  // Sky Blue
      '#FF9B77',  // Coral
    ];
    return Array.from({ length: count }, (_, i) => colors[i % colors.length]);
  }
}
