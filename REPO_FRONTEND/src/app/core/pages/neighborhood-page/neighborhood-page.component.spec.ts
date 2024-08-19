import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NeighborhoodPageComponent } from './neighborhood-page.component';

describe('NeighborhoodPageComponent', () => {
  let component: NeighborhoodPageComponent;
  let fixture: ComponentFixture<NeighborhoodPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NeighborhoodPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NeighborhoodPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
