import { Injectable } from '@angular/core'
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { Expense } from '../models/expense';

@Injectable()
export class ExpenseService {
    constructor(private readonly http: Http) { }

    getUser() {
        return this.http.get('/api/expense')
            .map((res: Response) => res.json().response);
    }
}
