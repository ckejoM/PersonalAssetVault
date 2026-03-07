import { Routes } from '@angular/router';
import { MainLayoutComponent } from './core/layout/main-layout/main-layout.component';

export const routes: Routes = [
    // Public Route (No Layout)
  { 
    path: 'login', 
    loadComponent: () => import('./features/auth/login/login.component').then(c => c.LoginComponent) 
  },  
  // Protected Routes (Wrapped in MainLayout)
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { 
        path: 'dashboard', 
        loadComponent: () => import('./features/dashboard/dashboard/dashboard.component').then(c => c.DashboardComponent) 
      },
      { 
        path: 'assets', 
        loadComponent: () => import('./features/assets/asset-list/asset-list.component').then(c => c.AssetListComponent) 
      }
    ]
  },
  { path: '**', redirectTo: 'dashboard' }
];
