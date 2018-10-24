import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class ProductService {

  constructor(private http: Http) { }

  getFirstThreeProducts() {
    return this.http.get("/api/Product/FindLastThree").map(res => res.json());
  }

  searchProducts(keywords) {
    return this.http.get("/api/Product/Search?keywords=" + keywords).map(res=>res.json());
  }

  getProductList() {
    return this.http.get("/api/Product/Search?keywords=a").map(res => res.json());
  }

  getProductById(id) {
    return this.http.get("/api/Product/FindById/" + id.toString()).map(res=>res.json());
  }
}
