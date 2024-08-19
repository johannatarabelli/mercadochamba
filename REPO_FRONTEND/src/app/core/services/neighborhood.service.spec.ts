import { TestBed } from '@angular/core/testing';

import { NeighborhoodService } from './neighborhood.service';

describe('NeighborhoodService', () => {
  let service: NeighborhoodService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NeighborhoodService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
