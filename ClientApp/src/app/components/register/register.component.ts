import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public username: string;
  public password: string;
  public email: string;

  constructor(private userService: UserService,private router:Router) { }

  ngOnInit() {
  }

  register() {
    this.userService.register(this.username, this.email, this.password).subscribe(() => {

      this.router.navigate(['/login']);
    });
  }

}
