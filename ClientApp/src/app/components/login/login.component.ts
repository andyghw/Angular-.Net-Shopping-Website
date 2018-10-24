import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public email: string;
  public password: string;
  public user: User;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
  }

  validate() {
    this.userService.login(this.email, this.password).subscribe(
      (data) => this.success(data),
      (error) => this.handleError(error)
    );
  }

  success(data) {
    this.user = data;
    this.router.navigate([""]);
    sessionStorage.setItem("username", this.user.username);
    sessionStorage.setItem("email", this.user.email);
    sessionStorage.setItem("password", this.user.password);
    sessionStorage.setItem("id", this.user.id.toString());
  }

  handleError(error) {
    alert("Wrong email or password!");
  }

}
