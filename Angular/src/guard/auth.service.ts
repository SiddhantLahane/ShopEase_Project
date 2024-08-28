import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn: boolean = false;

  constructor() {}

  login() {
    this.loggedIn = true;
    // Additional logic for logging in, like saving tokens or session details.
  }

  logout() {
    this.loggedIn = false;
    // Additional logic for logging out, like clearing tokens or session details.
  }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  getUserRole(): string {
    // Assuming a simple role management, could be 'user', 'admin', etc.
    return 'user'; // This would typically come from user session data.
  }
}