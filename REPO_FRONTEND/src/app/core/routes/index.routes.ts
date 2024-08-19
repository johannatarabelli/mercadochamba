import { Routes } from '@angular/router';
import { IndexPageComponent } from '../pages/index-page/index-page.component';
import { ProfilePageComponent } from '../pages/profile-page/profile-page.component';

export const indexRoutes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: IndexPageComponent },
      { path: 'profile/:id', component: ProfilePageComponent }
    ],
  },
];
