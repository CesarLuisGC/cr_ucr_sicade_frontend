import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { Login } from '../../models/login.model';

@Component({
  selector: 'home-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  login: Login = new Login()

  constructor(
    public _service: LoginService,
    public router: Router,
  ) { }

  Login() {
    this._service.Login(this.login)
      .subscribe((res: any) => {
        this.router.navigate(["/app"])
      });
  }
}
