import { Component, inject, OnInit } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
import { User } from '../../../auth/models/user.interface';

@Component({
  selector: 'shared-header',
  standalone: true,
  imports: [MaterialModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  public isAuthRoute: boolean = false;
  private authService = inject(AuthService);
  public user : User | null = null;

  constructor(private router : Router){
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isAuthRoute = event.urlAfterRedirects.includes('auth');
      }
    });
    this.authService.checkAuthStatus().subscribe(
      (status) => {
        if(status){
          this.user =  this.authService.currentUser();
        }
      }
    );
  }

  navigateTo(route : string){
    this.router.navigate([route]);
  }
  goToIndex() {
    this.navigateTo("/");
  }
}
