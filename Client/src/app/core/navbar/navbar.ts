import { CommonModule } from '@angular/common';
import { Component, type Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { FaIconComponent } from '@fortawesome/angular-fontawesome';
import { faUser, faCoffee } from '@fortawesome/free-solid-svg-icons';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

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
  cartCount = 0; // TODO: Replace with actual cart count from a service

  private router = inject(Router);

  onSearch() {
    const term = this.searchText.trim();
    if (term) {
      this.router.navigate(['/store'], { queryParams: { search: term } });
    } else {
      this.router.navigate(['/store']);
    }
  }
}
