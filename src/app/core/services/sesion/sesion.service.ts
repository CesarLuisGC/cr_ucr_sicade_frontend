import { Injectable } from '@angular/core';
import { Sesion } from '../../models/base/sesion.model';

@Injectable({
  providedIn: 'root'
})
export class SesionService {
  public sesion: Sesion = new Sesion();
  private localStorage: string = "sesion"; 

  public GetSession(): void {
    let sessionStorage = this.GetStorage();

    if(sessionStorage != null) {
      this.sesion = JSON.parse(sessionStorage);
    }
  }

  public IsAuthenticated(): boolean {
    if(this.sesion.token == "" ) {
      this.GetSession();
    }
    
    return this.sesion.token != "";
  }

  public SetStorage(value: Sesion): void {
    localStorage.setItem(this.localStorage, btoa(JSON.stringify(value)));
  }

  public GetStorage(): string | null {
    let storage = localStorage.getItem(this.localStorage);

    return storage ? atob(storage) : null;
  }
}
