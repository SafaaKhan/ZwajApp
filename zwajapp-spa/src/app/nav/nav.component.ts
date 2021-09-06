import { Component, OnInit } from '@angular/core';
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
    private authAlertify: AlertfyService
  ) {}

  ngOnInit(): void {}

  loging() {
    this.authService.login(this.model).subscribe(
      (next) => {
        this.authAlertify.success('تم الدخول بنجاح');
      },
      (error) => {
        this.authAlertify.error(error);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem('token');
    this.authAlertify.error('تم تسجيل الخروج بنجاح');
  }
}
