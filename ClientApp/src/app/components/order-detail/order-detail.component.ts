import { Component, OnInit } from '@angular/core';
import { Item } from '../../models/Item';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { switchMap } from 'rxjs/operator/switchMap';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit {
  public totalPrice: number = 0;
  public orderItems: Item[] = [];
  public orderId: string;

  constructor(private activatedRoute: ActivatedRoute, private orderService: OrderService, private router: Router) { }

  ngOnInit() {
    this.activatedRoute.paramMap.switchMap((map: ParamMap) => this.orderService.getOrderById(map.get('id'))).subscribe(res => {
      this.orderItems = res;
      for (let item of this.orderItems) {
        this.totalPrice += item.num * item.price;
      }
    });
  }

  deleteOrder() {
    this.activatedRoute.paramMap.switchMap((map: ParamMap) => this.orderService.deleteOrder(map.get('id'))).subscribe(() => {
      this.router.navigate(['/order']);
    });
  }
}
