import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

@Injectable()
export class OrderService {
  constructor(private http: Http) { }

  createOrder(items) {
    return this.http.post("/api/Order/AddOrder", items);
  }

  getMostRecentOrder() {
    return this.http.get("/api/Order/GetMostRecentOrder/" + sessionStorage.getItem('id')).map(res => res.json());
  }

  payForOrder(orderId) {
    return this.http.put("/api/Order/UpdateOrder/" + orderId, null);
  }

  getOrders() {
    return this.http.get("/api/Order/GetOrder/" + sessionStorage.getItem('id')).map(res => res.json());
  }

  getOrderById(id) {
    return this.http.get("/api/Order/GetOrderDetail/" + id).map(res => res.json());
  }

  deleteOrder(orderId) {
    return this.http.delete("/api/Order/DeleteOrder/" + orderId);
  }
}
