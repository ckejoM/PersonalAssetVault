import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AssetService } from '../../../core/services/asset.service';

@Component({
  selector: 'app-asset-edit',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './asset-edit.component.html'
})
export class AssetEditComponent implements OnInit {
  private fb = inject(FormBuilder);
  private assetService = inject(AssetService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  assetId = '';
  assetName = signal<string>('Loading...');
  isSubmitting = false;

  // Notice we only allow editing the Value, matching our backend DTO
  editForm = this.fb.nonNullable.group({
    value: [0, [Validators.required, Validators.min(0.01)]]
  });

  ngOnInit() {
    this.assetId = this.route.snapshot.paramMap.get('id') || '';
    
    if (this.assetId) {
      this.assetService.getById(this.assetId).subscribe({
        next: (asset) => {
          this.assetName.set(asset.name);
          this.editForm.patchValue({ value: asset.value });
        },
        error: () => this.router.navigate(['/dashboard'])
      });
    }
  }

  onSubmit() {
    if (this.editForm.invalid) return;

    this.isSubmitting = true;
    this.assetService.update(this.assetId, this.editForm.getRawValue()).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: () => this.isSubmitting = false
    });
  }

  onDelete() {
    if (confirm('Are you sure you want to delete this asset? This cannot be undone.')) {
      this.assetService.delete(this.assetId).subscribe({
        next: () => this.router.navigate(['/dashboard'])
      });
    }
  }
}