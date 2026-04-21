import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignMembershipDialogComponent } from './assign-membership-dialog.component';

describe('AssignMembershipDialogComponent', () => {
  let component: AssignMembershipDialogComponent;
  let fixture: ComponentFixture<AssignMembershipDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignMembershipDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AssignMembershipDialogComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
