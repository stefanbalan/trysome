import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { XpService } from '../xp.service';

@Component({
  selector: 'app-add-xp',
  templateUrl: './add-xp.component.html',
  styleUrls: ['./add-xp.component.css']
})
export class AddXpComponent implements OnInit {

  constructor(private xps: XpService) { }

  ngOnInit() {
  }

}
