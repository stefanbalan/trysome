import * as console from 'console';
import { Expense } from './model/expense';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class XpService {
  constructor(private http: HttpClient) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'my-auth-token'
    })
  };

  addExpense(expense: Expense): boolean {
    // return this.http.post<Expense>('api/Expense', expense, httpOptions)
    // .pipe(
    //   catchError(this.handleError('addHero', hero))
    // );
    // console.log('sent to service ');
    return true;
  }
}
