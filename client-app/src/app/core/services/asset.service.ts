import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { toSignal } from '@angular/core/rxjs-interop';

// Match the DTO from your .NET API
export interface AssetResponse {
  id: string;
  name: string;
  value: number;
  categoryId: string;
  acquiredAt: string;
}
export interface CreateAssetRequest {
  name: string;
  value: number;
  categoryId: string;
  acquiredAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class AssetService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7128/api/assets'; // Ensure this matches your API port!

// 1. Create a Writable Signal to hold the state internally
  private assetsState = signal<AssetResponse[]>([]);

  // 2. Expose it as Read-Only so components can't accidentally mutate it
  public assets = this.assetsState.asReadonly();

  // 3. Create a method that forces a network fetch
  public loadAssets() {
    this.http.get<AssetResponse[]>(this.apiUrl).subscribe({
      next: (data) => this.assetsState.set(data),
      error: (err) => console.error('Failed to load assets', err)
    });
  }
  public create(request: CreateAssetRequest) {
    // We return the Observable here so the component can subscribe, 
    // handle loading states, and redirect the user upon success.
    return this.http.post<AssetResponse>(this.apiUrl, request);
  }
}