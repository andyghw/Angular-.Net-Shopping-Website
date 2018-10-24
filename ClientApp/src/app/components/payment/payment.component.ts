import { Component, OnInit } from '@angular/core';
import { Item } from '../../models/Item';
import { OrderService } from '../../services/order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  public totalPrice: number=0;
  public orderItems: Item[] = [];

  constructor(private orderService:OrderService,private router:Router) { }

  ngOnInit() {
    this.orderService.getMostRecentOrder().subscribe(res => {
      this.orderItems = res;
      for (let i in this.orderItems) {
        this.totalPrice += this.orderItems[i].price * this.orderItems[i].num;
      }
    }); 
  }

  payForOrder() {
    this.orderService.payForOrder(this.orderItems[0].orderId).subscribe(() => {
      this.router.navigate(['/order']);
    });
  }

}
