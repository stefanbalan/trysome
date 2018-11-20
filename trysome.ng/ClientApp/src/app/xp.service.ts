import { Expense } from './model/expense';
import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs/observable';
import { catchError, retry } from 'rxjs/operators';

@Injectable()
export class XpService {
  constructor(private readonly http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'my-auth-token'
    })
  };

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`
      );
    }
    // return an observable with a user-facing error message
    //return throwError('Something bad happened; please try again later.');
  }

  addExpense(expense: Expense): boolean {
    console.log(expense);
    const result = this.http
      .post<Expense>('api/Expense', expense, this.httpOptions)
      .pipe(retry(2))
      .pipe(catchError(this.handleError('addHero', expense)))
      ;

    return true;
  }
}
