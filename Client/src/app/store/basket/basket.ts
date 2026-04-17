import { CommonModule } from '@angular/common';
import { Component, inject, signal, type OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BasketService } from '../services/basket.service';
import type { Basket, BasketItem } from '../models/Basket';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './basket.html',
  styleUrl: './basket.scss',
})
export class BasketComponent implements OnInit {

  private basketService = inject(BasketService);

  basket = signal<Basket | null>(null);

  ngOnInit(): void {
    const localBasket = this.basketService.basket;
    if (localBasket) {
      this.basket.set(localBasket);
    }

    // refresh basket from backend to sync latest
    // TODO: replace user with name. use JWT lib tofetch
    this.basketService.getBasket('ergin').subscribe({
      next: (res) => {
        this.basket.set(res);
        this.basketService.setBasket(res);
      },
      error: (err) => console.error('Error loading basket:', err)
    });
  }

  increment(item: BasketItem) {
    item.quantity++;
    this.updateBasket();
  }

  decrement(item: BasketItem) {
    if (item.quantity > 1) {
      item.quantity--;
      this.updateBasket();
    }
    else {
      this.removeItem(item);
    }
  }

  removeItem(item: BasketItem) {
    const updatedItems = this.basket()?.items.filter(i => i.productId !== item.productId) || [];
    this.basket.set({
      userName: 'ergin',
      items: updatedItems,
      totalPrice: updatedItems.reduce((sum, i) => sum + i.price * i.quantity, 0)
    });
    this.updateBasket();
  }

  private updateBasket() {
    const updated = this.basket();
    if (updated) {
      updated.totalPrice = updated.items.reduce((sum, i) => sum + i.price * i.quantity, 0);
      this.basketService.updateBasket(updated).subscribe({
        next:(res) => this.basketService.setBasket(res),
        error: (err) => console.error('Error updating basket', err)
      });
    } 
  }

  checkout(){
    alert("Proceeding to checkout with total " + this.basket()?.totalPrice);
  }

}
