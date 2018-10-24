import { Component, OnInit, DoCheck, OnChanges } from '@angular/core';
import { Item } from '../../models/Item';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  public cartItems: Item[] = [];
  public counts: number[] = [];
  public totalPrice: number = 0.0;

  constructor(private cartService: CartService, private orderService: OrderService, private router: Router) { }

  ngOnInit() {
    this.cartService.getCart().subscribe(res => {
      this.cartItems = res;
      for (let i in this.cartItems) {
        this.totalPrice += this.cartItems[i].num * this.cartItems[i].price;
        this.counts.push(this.cartItems[i].num);
      }
    });
  }

  updateCart(item, i) {
    item.num = this.counts[i];
    this.cartService.updateCart(item).subscribe();
    this.totalPrice=0
    for (let i in this.cartItems) {
      this.totalPrice += this.cartItems[i].num * this.cartItems[i].price;
    }
  }

  deleteCart(id) {
    this.cartItems = this.cartItems.filter(_ => _.id != id);
    this.totalPrice = 0;
    this.counts = [];
    for (let i in this.cartItems) {
      this.totalPrice += this.cartItems[i].num * this.cartItems[i].price;
      this.counts.push(this.cartItems[i].num);
    }
    this.cartService.deleteCart(id).subscribe();
  }

  checkout() {
    this.cartService.cleanCart(sessionStorage.getItem('id')).subscribe(() => {
      this.orderService.createOrder(this.cartItems).subscribe(() => {
        this.router.navigate(['/payment']);
      });
    });
  }
}
