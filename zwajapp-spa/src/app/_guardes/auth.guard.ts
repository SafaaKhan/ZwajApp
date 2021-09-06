import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AlertfyService } from '../_services/alertfy.service';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertfyService
  ) {}
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }
    this.alertify.error('يجب عليك التسجيل');
    this.router.navigate(['']);
    return false;
  }
}
