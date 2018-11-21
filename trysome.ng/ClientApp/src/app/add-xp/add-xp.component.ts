import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { XpService } from '../xp.service';
import { Expense } from '../model/expense';

@Component({
  selector: 'app-add-xp',
  templateUrl: './add-xp.component.html',
  styleUrls: ['./add-xp.component.css']
})
export class AddXpComponent implements OnInit {
  constructor(private readonly xps: XpService) {}

  // model = new Xpence
  date?: Date;
  amount?: number;
  category: string;
  tags: string;

  submitted: boolean;
  ngOnInit() {}

  onSubmit() {
    const xp = new Expense(this.date, this.amount, this.category, this.tags);
    if (!this.xps.addExpense(xp)) {
      return;
    }
    this.date = null;
    this.amount = null;
    this.category = null;
    this.tags = null;
    this.submitted = true;
  }
}
