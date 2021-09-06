import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertfyService } from '../_services/alertfy.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(
    public authService: AuthService,
    private authAlertify: AlertfyService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  loging() {
    this.authService.login(this.model).subscribe(
      (next) => {
        this.authAlertify.success('تم الدخول بنجاح');
      },
      (error) => {
        this.authAlertify.error(error);
      },
      () => {
        this.router.navigate(['/members']);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem('token');
    this.authAlertify.error('تم تسجيل الخروج بنجاح');
    this.router.navigate(['/home']);
  }
}
