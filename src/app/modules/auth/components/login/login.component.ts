import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { UserModel } from '../../models/user.model';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Login } from 'src/app/modules/public/models/login.model';
import { LoginService } from 'src/app/modules/public/services/login.service';
import { SesionService } from 'src/app/core/services/sesion/sesion.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
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

        this.router.navigate(["/"])
      });
  }
}
