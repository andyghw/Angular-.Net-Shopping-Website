import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/Product';
import { ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { ProductService } from '../../services/product.service';
import { Item } from '../../models/Item';
import { CartService } from '../../services/cart.service';
import { Http } from '@angular/http';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  public product: Product;
  public count: number=1;
  public existed: string;

  constructor(private route: ActivatedRoute, private productService: ProductService, private cartService: CartService, private http: Http) { }

  ngOnInit() {
    this.route.paramMap.switchMap((map: ParamMap) => this.productService.getProductById(map.get('id'))).subscribe(res => this.product = res);
  }

  addToCart() {
    if (sessionStorage.getItem("id") == undefined) {
      alert("Please login first.");
      return;
    }
    let item = {
      Title: this.product.name,
      UserId: parseInt(sessionStorage.getItem("id")),
      Num: parseInt(this.count.toString()),
      Img: this.product.imgs[0],
      Price: this.product.price
    }
    let existed;
    this.cartService.checkExisted(item).subscribe(res => {
      existed=res;
      if (existed) {
        this.cartService.updateAddCart(item).subscribe();
      }
      else {
        this.cartService.addToCart(item).subscribe();
      }
      alert("Item added to the cart.");
    });
  }
}
