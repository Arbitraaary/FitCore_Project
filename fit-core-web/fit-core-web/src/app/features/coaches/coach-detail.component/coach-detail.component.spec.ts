import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoachDetailComponent } from './coach-detail.component';

describe('CoachDetailComponent', () => {
  let component: CoachDetailComponent;
  let fixture: ComponentFixture<CoachDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoachDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CoachDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
