import { Component, OnInit, DoCheck } from '@angular/core';
import { DataService } from '../../services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit,DoCheck {
  public keywords: string;
  public username: string;

  constructor(private dataService: DataService, private router: Router) { }

  ngOnInit() {
    this.setData();
    this.username = sessionStorage.getItem("username");
  }

  ngDoCheck() {
    this.username = sessionStorage.getItem("username");
  }

  setData() {
    this.dataService.setData(this.keywords);
  }

  logout() {
    sessionStorage.clear();
    this.router.navigate(['']);
  }
}
