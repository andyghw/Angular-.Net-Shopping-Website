import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/Product';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public products: Product[] = [];

  constructor(private productService: ProductService ) { }

  ngOnInit() {
    this.productService.getFirstThreeProducts().subscribe(res => this.products=res);
  }
}
