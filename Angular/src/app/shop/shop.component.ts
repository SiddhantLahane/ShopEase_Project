import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { routes } from '../app.routes';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [NavbarComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})
export class ShopComponent {

  constructor(private router: Router) {}

  redirectToLogin() {
    console.log('Redirecting to login');
    this.router.navigate(['/cart']);
  }
}
