import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  constructor(private router: Router) {

  }

  isAuthenticated(): boolean {
    let check = localStorage.getItem('access_token');
    if (check) {
      return true;
    }
    else {
      return false;
    }
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('userId');
    this.router.navigate(['home']);
  }
}
