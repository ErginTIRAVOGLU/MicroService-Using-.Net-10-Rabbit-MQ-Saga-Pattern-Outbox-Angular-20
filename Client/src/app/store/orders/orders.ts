import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, signal, type OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import type { Order } from '../models/Order';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './orders.html',
  styleUrl: './orders.scss',
})
export class Orders implements OnInit {
  private http = inject(HttpClient);
  
  orders = signal<Order[]>([]);
  loading = signal<boolean>(true);


  ngOnInit(): void {
    const userName='ergin'; // TODO: this.authService.getUserName();
    this.http.get<Order[]>(`/Order/${userName}`).subscribe({
      next: (res) => {
        const sorted = res.sort((a, b) => b.id - a.id);
        this.orders.set(sorted);
        this.loading.set(false);
      },
      error: (err) => {
        console.log("Failed to load orders ",err);
        this.loading.set(false);
      },
    });
  }
}
