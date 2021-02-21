import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SynonymService {
  protected _http: HttpClient;

  constructor(http: HttpClient) {
    this._http = http;
   }

   addSynonyms(body: any) {
     const url = environment.apiUrl;
     return this._http.post(url, body)
     .pipe(
       map((response) => {
      return response;
     }),
      catchError(this.handleError)
    )
    .toPromise();
   }

   findSynonyms(searchTerm: any) {
    const url = environment.apiUrl + `search/${searchTerm}`;
    return this._http.get(url)
    .pipe(
      map((response) => {
     return response;
    }),
     catchError(this.handleError)
   )
   .toPromise();
  }

  getSynonyms(index: any) {
    const url = environment.apiUrl + index;
    return this._http.get(url)
    .pipe(
      map((response) => {
     return response;
    }),
     catchError(this.handleError)
   )
   .toPromise();
  }

   protected handleError(error: any) {
    if (error instanceof Error) {
      return throwError({ errors: ['Invalid server response.', error.message] });
    }

    const applicationError = error.headers.get('Application-Error');
    if (applicationError) {
      return throwError(applicationError);
    }

    const modelStateErrors = error.error;
    return throwError(modelStateErrors || { errors: ['Server error.'] });
  }
}
