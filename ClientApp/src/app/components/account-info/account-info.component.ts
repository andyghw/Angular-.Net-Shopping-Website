import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  public username: string;
  public email: string;
  public userId: string;
  public password: string;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.username = sessionStorage.getItem('username');
    this.email = sessionStorage.getItem('email');
    this.userId = sessionStorage.getItem('id');
    this.password = sessionStorage.getItem('password');
  }

  updateAccountInfo() {
    sessionStorage.setItem('username', this.username);
    sessionStorage.setItem('email', this.email);
    sessionStorage.setItem('password', this.password);
    this.userService.update(this.username, this.password, this.email).subscribe(() => {
      this.router.navigate(['']);
    });
  }
}
