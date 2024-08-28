import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const userLoggedIn = this.authService.isLoggedIn();
    const allowedRoutesForUser = ['/', 'shop']; // Routes that a logged-in user can access

    if (userLoggedIn && allowedRoutesForUser.includes(route.url[0].path)) {
      return true;
    } else if (!userLoggedIn && (route.url[0].path === 'userlogin' || route.url[0].path === 'registration')) {
      return true;
    } else {
      // Redirect based on user's session state
      this.router.navigate(userLoggedIn ? [''] : ['/userlogin']);
      return false;
    }
  }
}
