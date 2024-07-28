import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { Login } from '../../models/login.model';
import { SesionService } from '../../../../core/services/sesion/sesion.service';

@Component({
  selector: 'home-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  login: Login = new Login()

  constructor(
    public _loginService: LoginService,
    public _sesionService: SesionService,
    public router: Router,
  ) { }

  Login() {
    this._loginService.Login(this.login)
      .subscribe((res: any) => {
        this._sesionService.SetStorage(res);
        this._sesionService.sesion = res;

        this.router.navigate(["/app"])
      });
  }
}
