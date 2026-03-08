import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AssetService } from '../../../core/services/asset.service';
import { CategoryService } from '../../../core/services/category.service';

@Component({
  selector: 'app-asset-create',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './asset-create.component.html'
})
export class AssetCreateComponent {
  private fb = inject(FormBuilder);
  private assetService = inject(AssetService);
  private router = inject(Router);

  private categoryService = inject(CategoryService);

  // Expose the signal to the HTML
  categories = this.categoryService.categories;

  // Strictly typed, non-nullable form
  assetForm = this.fb.nonNullable.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],
    value: [0, [Validators.required, Validators.min(0.01)]],
    categoryId: ['', [Validators.required]], // You will paste a SQLite Category Guid here
    acquiredAt: [new Date().toISOString().split('T')[0], [Validators.required]] // Defaults to today
  });

  isSubmitting = false;

  onSubmit() {
    if (this.assetForm.invalid) {
      this.assetForm.markAllAsTouched(); // Force validation messages to show
      return;
    }

    this.isSubmitting = true;
    
    this.assetService.create(this.assetForm.getRawValue()).subscribe({
      next: () => {
        // Success! Route the user back to the dashboard. 
        // Because of your recent fix, the dashboard will auto-fetch the fresh data!
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        this.isSubmitting = false; // Our global error interceptor handles the alert
      }
    });
  }
}