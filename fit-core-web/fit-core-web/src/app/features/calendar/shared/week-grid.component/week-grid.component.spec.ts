import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeekGridComponent } from './week-grid.component';

describe('WeekGridComponent', () => {
  let component: WeekGridComponent;
  let fixture: ComponentFixture<WeekGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeekGridComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(WeekGridComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
