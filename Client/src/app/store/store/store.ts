import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal, type OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../services/product.service';
import type { Product } from '../models/Product';
import type { Brand } from '../models/Brand';
import type { Type } from '../models/Type';
import { BasketService } from '../services/basket.service';
import type { Basket, BasketItem } from '../models/Basket';

@Component({
  selector: 'app-store',
  imports: [CommonModule, RouterModule],
  standalone: true,
  templateUrl: './store.html',
  styleUrl: './store.scss',
})
export class Store implements OnInit {
  private productService = inject(ProductService);
  private route = inject(ActivatedRoute);
  private basketService = inject(BasketService);

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
      this.selectedTypeId.set(params['typeId'] || null);
      this.selectedBrandId.set(params['brandId'] || null);
      this.sortOption.set(params['sort'] || 'default');
      this.currentPage.set(1); // Reset to first page on new search
      this.loadProducts();
    });
  }

  loadProducts() {
    this.productService.getAllProducts(
      this.currentPage(),
      this.pageSize,
      this.selectedBrandId(),
      this.selectedTypeId(),
      this.sortOption(),
      this.searchTerm()
    ).subscribe(response => {
      this.products.set(response.data);
      this.totalCount.set(response.count);
    });
  }

  loadBrands() {
    this.productService.getAllBrands().subscribe(response => {
      this.brands.set(response);
    });
  }

  loadTypes() {
    this.productService.getAllTypes().subscribe(response => {
      this.types.set(response);
    });
  }

  // Appluy filters
  applyFilters() {
    this.currentPage.set(1); // Reset to first page on filter change
    this.loadProducts();
  }


  // Filtered products based on search term
  selectedBrandId = signal<string | null>(null);
  selectedTypeId = signal<string | null>(null);
  sortOption = signal('default');

  // Pagination
  pageSize = 10;
  currentPage = signal(1);


  /*
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
      filtered = filtered.filter(p => p.brand.name === this.selectedBrand());
    }

    // Apply type filter
    if (this.selectedType()) {
      filtered = filtered.filter(p => p.type.name === this.selectedType());
    }

    if (this.searchTerm()) {
      const term = this.searchTerm().toLowerCase();
      filtered = filtered.filter(p =>
        p.name.toLowerCase().includes(term) ||
        p.brand.name.toLowerCase().includes(term) ||
        p.type.name.toLowerCase().includes(term) ||
        (p.description && p.description.toLowerCase().includes(term))
      );
    }




    // Apply sorting
    if (this.sortOption() === 'priceAsc') {
      filtered = filtered.slice().sort((a, b) => a.price - b.price);
    } else if (this.sortOption() === 'priceDesc') {
      filtered = filtered.slice().sort((a, b) => b.price - a.price);
    }

    return filtered;
  });
  */
  addToCart(p: Product) {
    const newItem: BasketItem = {
      productId: p.id,
      productName: p.name,
      price: p.price,
      quantity: 1,
      imageFile: p.imageFile
    };

    // First fetch existing basket
    this.basketService.getBasket('ergin').subscribe(current => {
      let items = [...current.items];
      // if product already exists, then increment quantity
      const existing = items.find((i) => i.productId === p.id);
      if (existing) {
        existing.quantity += 1;
      }
      else {
        items.push(newItem);
      }

      const basket: Basket = {
        userName: 'ergin',
        items,
        totalPrice: items.reduce((sum, i) => sum + i.price * i.quantity, 0),
      };

      this.basketService.updateBasket(basket).subscribe({
        next: (res) => {
          this.basketService.setBasket(res);
          console.log("Basket updated", res);
        },
        error: (err) => console.log("Error adding to basket", err)
      });

    });
  }

  // Reset filters when search term changes
  resetFilters() {
    this.searchTerm.set('');
    this.selectedBrandId.set(null);
    this.selectedTypeId.set(null);
    this.sortOption.set('default');
    this.currentPage.set(1);
    this.loadProducts();
  };

  /*
  paginatedProducts = computed(() => {
    const start = (this.currentPage() - 1) * this.pageSize;
    return this.filteredProducts().slice(start, start + this.pageSize);
  });
  */

  totalPages =() => Math.ceil(this.totalCount() / this.pageSize);
  

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages()) {
      this.currentPage.set(page);
      this.loadProducts();
    }
  }
}
