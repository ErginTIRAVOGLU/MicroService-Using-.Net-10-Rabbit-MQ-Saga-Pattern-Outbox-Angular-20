import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal, type OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../services/product.service';
import type { Product } from '../models/Product';
import type { Brand } from '../models/Brand';
import type { Type } from '../models/Type';

@Component({
  selector: 'app-store',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './store.html',
  styleUrl: './store.scss',
})
export class Store implements OnInit {
  private productService = inject(ProductService);
  private route = inject(ActivatedRoute);

  /*
  products = signal(Array.from({ length: 50 }).map((_, i) => ({
    id: i + 1,
    name: `Product ${i + 1}`,
    brand: i % 2 === 0 ? 'Nike' : 'Adidas',
    type: i % 3 === 0 ? 'Shoes' : 'Clothing',
    price: (i + 1) * 100,
    imageUrl: `https://placehold.co/200x200?text=Product+${i + 1}`
  })));
  */
  products = signal<Product[]>([]);
  totalCount = signal(0);
  brands = signal<Brand[]>([]);
  types = signal<Type[]>([]);
  searchTerm = signal<string>('');

  ngOnInit(): void {
    this.loadProducts();
    this.loadBrands();
    this.loadTypes();
    
    // Listen to query params for search term
    this.route.queryParams.subscribe(params => {
      this.searchTerm.set(params['search'] || '');
      this.currentPage.set(1); // Reset to first page on new search
      this.loadProducts();
    });
  }




  // Filtered products based on search term
  selectedBrand = signal<string | null>(null);
  selectedType = signal<string | null>(null);
  sortOption = signal('default');

  // Pagination
  pageSize = 10;
  currentPage = signal(1);



  // Combine filters and sorting to get the final product list
  filteredProducts = computed(() => {
    let filtered = this.products();

    // Apply search filter
    if (this.searchTerm()) {
      filtered = filtered.filter(p =>
        p.name.toLowerCase().includes(this.searchTerm().toLowerCase())
      );
    }

    // Apply brand filter
    if (this.selectedBrand()) {
      filtered = filtered.filter(p => p.brand === this.selectedBrand());
    }

    // Apply type filter
    if (this.selectedType()) {
      filtered = filtered.filter(p => p.type === this.selectedType());
    }

    // Apply sorting
    if (this.sortOption() === 'priceAsc') {
      filtered = filtered.slice().sort((a, b) => a.price - b.price);
    } else if (this.sortOption() === 'priceDesc') {
      filtered = filtered.slice().sort((a, b) => b.price - a.price);
    }

    return filtered;
  });

  // Reset filters when search term changes
  resetFilters() {
    this.searchTerm.set('');
    this.selectedBrand.set(null);
    this.selectedType.set(null);
    this.sortOption.set('default');
    this.currentPage.set(1);
  };

  paginatedProducts = computed(() => {
    const start = (this.currentPage() - 1) * this.pageSize;
    return this.filteredProducts().slice(start, start + this.pageSize);
  });

  totalPages = computed(() => {
    return Math.ceil(this.filteredProducts().length / this.pageSize);
  });

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages()) {
      this.currentPage.set(page);
    }
  }

}
