import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddXpComponent } from './add-xp.component';

describe('AddXpComponent', () => {
  let component: AddXpComponent;
  let fixture: ComponentFixture<AddXpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddXpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddXpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
