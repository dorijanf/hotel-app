import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { User } from '../models/user';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  currentUser: User;
  navbarOpen = false;
  constructor(public authenticationService: AuthenticationService, private router: Router) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }
  ngOnInit(): void {
  }

  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/']);
  }
}
