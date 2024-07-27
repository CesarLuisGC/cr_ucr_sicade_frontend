import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { UrlParam } from '../../models/base/urlParam.model';
import { ApiResponse } from '../../models/base/apiResponse.model';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  constructor(private http: HttpClient) { }
  Get(route: string) {
    return this.http.get<ApiResponse>(route)
      .pipe(
        map((resp: ApiResponse) => resp.data)
      )
  }

  Post<T>(route: string, data: T) {
    return this.http.post<ApiResponse>(route, data)
      .pipe(
        map((resp: ApiResponse) => resp.data)
      )
  }

  Put<T>(route: string, data: T) {
    return this.http.put<ApiResponse>(route, data)
      .pipe(
        map((resp: ApiResponse) => resp.data)
      )
  }

  BuildUrl(urlBase: string, subroute: string, ...params: UrlParam[]): string {
    let urlParams: string = ""

    params.forEach((param: UrlParam) => {
      urlParams += `${urlParams != "" ? "&" : ""}${param.name}=${param.value}`
    })

    return `${urlBase}/${subroute}${urlParams != "" ? "?" : ""}${urlParams}`;
  }
}
