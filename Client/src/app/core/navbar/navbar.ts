import { CommonModule } from '@angular/common';
import { Component, type Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { FaIconComponent } from '@fortawesome/angular-fontawesome';
import { faUser, faCoffee } from '@fortawesome/free-solid-svg-icons';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { BasketService } from '../../store/services/basket.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, FaIconComponent, CommonModule, FormsModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
  standalone: true,
})
export class Navbar {
  faUser = faUser;
  faCoffee = faCoffee;

  searchText = '';
  private basketService = inject(BasketService);
  private authService = inject(AuthService);

  private router = inject(Router);

  get cartCount() {
    return this.basketService.basketCount();
  }

  onSearch() {
    const term = this.searchText.trim();
    if (term) {
      this.router.navigate(['/store'], { queryParams: { search: term } });
    } else {
      this.router.navigate(['/store']);
    }
  }

  get isLoggedIn():boolean {
    return this.authService.isLoggedIn();
  }

  get userName(): string | null {
    return this.authService.getUserName();
  }

  logout(){
    this.authService.logout();
    this.router.navigateByUrl("/auth/login");
  }
}
