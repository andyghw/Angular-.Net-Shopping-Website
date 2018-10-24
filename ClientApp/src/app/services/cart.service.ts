import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";


@Injectable()
export class CartService {
  constructor(private http: Http) { }

  addToCart(item) {
    return this.http.post("/api/Cart/AddCartItem", item);
  }

  checkExisted(item) {
    return this.http.post("/api/Cart/CheckCartItem", item).map(res => res.json());
  }

  updateAddCart(item) {
    return this.http.post("/api/Cart/UpdateAddCartItem", item);
  }

  getCart() {
    let userId = sessionStorage.getItem("id");
    return this.http.get("/api/Cart/GetCart/" + userId).map(res=>res.json());
  }

  updateCart(item) {
    return this.http.put("/api/Cart/UpdateCartItem", item);
  }

  deleteCart(id) {
    return this.http.delete("/api/Cart/DeleteCartItem/" + id);
  }

  cleanCart(id) {
    return this.http.delete("/api/Cart/CleanCart/" + id);
  }
}
