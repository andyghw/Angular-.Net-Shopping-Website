import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

@Injectable()
export class UserService {
  constructor(private http: Http) {}

  login(email, password) {
    return this.http.post("/api/User/Login?email=" + email + "&password=" + password, null).map(res => res.json());
  }

  update(username, password, email) {
    return this.http.put("/api/User/UpdateAccount?email=" + email + "&password=" + password + "&username=" + username, null);
  }

  register(username, email, password) {
    return this.http.post("/api/User/Register?email=" + email + "&password=" + password + "&username=" + username, null);
  }
}
