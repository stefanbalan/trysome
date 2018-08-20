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
  constructor(private xps: XpService) {}

  // model = new Xpence
  amount: number;
  category: string;

  submitted: boolean;
  ngOnInit() {}

  public onSubmit() {
    this.submitted = true;
  }
}
