import { Component, inject, computed, OnInit } from '@angular/core';
import { AssetService } from '../../../core/services/asset.service';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CurrencyPipe, DatePipe, RouterLink], // We use Angular's built-in pipes to format the data
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  private assetService = inject(AssetService);

  // We expose the signal to the HTML template
  assets = this.assetService.assets;

  // Angular Theory: `computed` is a Signal that automatically recalculates 
  // ONLY when the signals it depends on (this.assets()) change. 
  // It's highly optimized, just like a computed property in C#!
  totalValue = computed(() => {
    return this.assets().reduce((sum, asset) => sum + asset.value, 0);
  });

  ngOnInit() {
    this.assetService.loadAssets();
  }
}