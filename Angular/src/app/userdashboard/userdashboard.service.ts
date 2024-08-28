import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserdashboardService {

  constructor() { }

  // Method to get user information from local storage
  getUserFromLocalStorage(): { isLoggedIn: boolean; username: string; token: string } | null {
    const localStorageData = localStorage.getItem('localstorage');
    return localStorageData ? JSON.parse(localStorageData) : null;
  }

  // Method to set user information in local storage
  setUserInLocalStorage(data: { isLoggedIn: boolean; username: string; token: string }): void {
    localStorage.setItem('localstorage', JSON.stringify(data));
  }

  // Method to check if the user is logged in
  isLoggedIn(): boolean {
    const localStorageData = this.getUserFromLocalStorage();
    return localStorageData ? localStorageData.isLoggedIn : false;
  }

  // Method to get the username
  getUsername(): string | null {
    const localStorageData = this.getUserFromLocalStorage();
    return localStorageData ? localStorageData.username : null;
  }

  // Method to get the token
  getToken(): string | null {
    const localStorageData = this.getUserFromLocalStorage();
    return localStorageData ? localStorageData.token : null;
  }
}