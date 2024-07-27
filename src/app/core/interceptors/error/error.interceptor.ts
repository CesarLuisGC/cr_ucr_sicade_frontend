import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { EMPTY, Observable, catchError } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = error.error.message;
          let message = "Por favor contacte al equipo de soporte";

          if (error.status === 401) {
            message = "Se han presentado problemas con el token; " + error.error.message;
          } else if (error.status === 404) {
            message = "No se encontro la ruta indicada en el web service; " + error.error.message;
          } else if (error.status == 400){
            if(errorMessage.includes("SHOW: ")) message = (errorMessage as string).replace("SHOW: ", "")
            
          } else{
            message = "Error conectando al servidor;";
          }

          Swal.fire("Ha ocurrido un error", message, 'info');
          

          return EMPTY;
        })
      );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};