import { Injectable } from "@angular/core";

@Injectable()
export class DataService {
  public data: any;

  setData(data) {
    this.data = data;
  }

  getData() {
    return this.data;
  }
}
