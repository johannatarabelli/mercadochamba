import { Routes } from '@angular/router';
import { DashboardLayoutComponent } from '../layouts/dashboard-layout/dashboard-layout.component';
import { DashboardPageComponent } from '../pages/dashboard-page/dashboard-page.component';
import { JobsPageComponent } from '../pages/jobs-page/jobs-page.component';
import { AdminsPageComponent } from '../pages/admins-page/admins-page.component';
import { CountryPageComponent } from '../pages/country-page/country-page.component';
import { ProvincePageComponent } from '../pages/province-page/province-page.component';
import { NeighborhoodPageComponent } from '../pages/neighborhood-page/neighborhood-page.component';

export const dashboardRoutes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    children: [
      { path: '', component: DashboardPageComponent },
      { path: 'jobs', component: JobsPageComponent },
      { path: 'admins', component: AdminsPageComponent },
      { path: 'countries', component: CountryPageComponent },
      { path: 'provinces', component: ProvincePageComponent },
      { path: 'neighborhoods', component: NeighborhoodPageComponent},
      {
        path: '**',
        redirectTo: '',
      },
    ],
  },
];
