import { Component } from '@angular/core';
import { RegistroComponent } from "../../components/registro/registro.component";
import { HeaderComponent } from '../../../shared/components/header/header.component';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [RegistroComponent,HeaderComponent],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss'
})
export class RegisterPageComponent {

}
