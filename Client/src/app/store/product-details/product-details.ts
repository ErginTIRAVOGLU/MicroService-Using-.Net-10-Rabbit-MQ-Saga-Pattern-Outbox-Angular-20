import { CommonModule } from '@angular/common';
import { Component, inject, signal, type OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../services/product.service';
import type { Product } from '../models/Product';
import { BasketService } from '../services/basket.service';
import type { Basket, BasketItem } from '../models/Basket';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule, RouterModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss',
})
export class ProductDetails implements OnInit {

  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private basketService = inject(BasketService);

  product = signal<Product | null>(null);

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.productService.GetProductById(id).subscribe({
          next: res => this.product.set(res),
          error: err => {
            console.error('Failed to laod product : ', err);
          }
        });
      }
    });
  }

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
        next:(res)=> {
          this.basketService.setBasket(res);
          console.log("Basket updated", res);
        },
        error:(err) => console.log("Error adding to basket",err)
      });

    });
  }




}
