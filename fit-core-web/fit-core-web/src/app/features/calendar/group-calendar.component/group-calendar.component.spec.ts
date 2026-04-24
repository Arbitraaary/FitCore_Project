import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupCalendarComponent } from './group-calendar.component';

describe('GroupCalendarComponent', () => {
  let component: GroupCalendarComponent;
  let fixture: ComponentFixture<GroupCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GroupCalendarComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GroupCalendarComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
