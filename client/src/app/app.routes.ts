import { Routes } from '@angular/router';
import { productsResolver } from './core/resolvers/products.resolver';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./features/home/home').then(m => m.Home),
    resolve: {
      products: productsResolver
    }
  }
];
