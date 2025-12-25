import { ResolveFn } from '@angular/router';
import { inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export const productsResolver: ResolveFn<any[]> = () => {
  const http = inject(HttpClient);
  return http.get<any>('https://localhost:5001/api/products');
};
