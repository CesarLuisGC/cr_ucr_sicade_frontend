import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { BaseService } from '../../../core/services/base/base.service';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private _base: BaseService) { }

  Login(login: Login): Observable<any> {
    console.log(environment.apiUrl)
    const route = this._base.BuildUrl(
      environment.apiUrl,
      "login"
    );

    return this._base.Post(route, login);
  }
}
