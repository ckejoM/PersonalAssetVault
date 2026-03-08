import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { toSignal } from '@angular/core/rxjs-interop';

export interface CategoryResponse {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7128/api/categories'; // Check your port!

  // 1. Fetch from the API
  private categories$ = this.http.get<CategoryResponse[]>(this.apiUrl);

  // 2. Convert to a synchronous Signal cache
  public categories = toSignal(this.categories$, { initialValue: [] });
}