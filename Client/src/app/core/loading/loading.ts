import { CommonModule } from '@angular/common';
import { Component, computed, inject } from '@angular/core';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-loading',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading.html',
  styleUrls: ['./loading.scss'],
})
export class Loading {
  private loadingService = inject(LoadingService);
  isLoading =   this.loadingService.loading;
}
