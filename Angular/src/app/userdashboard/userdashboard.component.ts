import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { NavbarComponent } from '../navbar/navbar.component';
import { Router } from '@angular/router';
import { UserNavbarComponent } from "../user-navbar/user-navbar.component";
import { UserdashboardService } from './userdashboard.service';


@Component({
  selector: 'app-userdashboard',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent, UserNavbarComponent],
  templateUrl: './userdashboard.component.html',
  styleUrl: './userdashboard.component.css'
})
export class UserdashboardComponent implements OnInit {

  user: { 
    username: string;
   
  } | null = null;

  isLoggedIn: boolean = false;
  username: string | null = null;

  constructor(private router: Router, private userdashboardService: UserdashboardService) { }

  // Logout method to clear local storage and navigate to login page
  logout() {
    localStorage.clear(); // Clear localStorage

    // Optionally redirect the user to the login page
    this.router.navigate(['userlogin']);
  }

  ngOnInit(): void {
    this.isLoggedIn = this.userdashboardService.isLoggedIn();
    if (this.isLoggedIn) {
      this.username = this.userdashboardService.getUsername();
    }
  }
}

 