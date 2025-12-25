import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.html'
})
export class Home {

  products: any[] = [];

  constructor(private route: ActivatedRoute) {
    const res = this.route.snapshot.data['products'];
    this.products = Array.isArray(res) ? res : res.data;
  }
}
