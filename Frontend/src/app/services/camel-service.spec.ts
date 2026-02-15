import { TestBed } from '@angular/core/testing';

import { CamelService } from './camel-service';

describe('CamelService', () => {
  let service: CamelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CamelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
