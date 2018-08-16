import { TestBed, inject } from '@angular/core/testing';

import { XpService } from './xp.service';

describe('XpService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [XpService]
    });
  });

  it('should be created', inject([XpService], (service: XpService) => {
    expect(service).toBeTruthy();
  }));
});
