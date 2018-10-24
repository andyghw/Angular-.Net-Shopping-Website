import { Component, OnInit, DoCheck } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/Product';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-search-product',
  templateUrl: './search-product.component.html',
  styleUrls: ['./search-product.component.css']
})
export class SearchProductComponent implements  OnInit {
  public products: Product[]=[];
  public keywords: string;

  constructor(private productService: ProductService, private dataService: DataService) { }

  ngOnInit() {
    this.keywords = this.dataService.getData();
    this.productService.getProductList().subscribe(res => this.products = res);
    console.log(this.keywords);
  }

  ngDoCheck() {
    if (this.keywords != this.dataService.getData()) {
      this.keywords = this.dataService.getData();
      this.productService.getProductList().subscribe(res => this.products = res);
    }
  }
}
