import { TestBed } from '@angular/core/testing';

import { ListCardsService } from './list-cards.service';

describe('ListCardsService', () => {
  let service: ListCardsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ListCardsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
