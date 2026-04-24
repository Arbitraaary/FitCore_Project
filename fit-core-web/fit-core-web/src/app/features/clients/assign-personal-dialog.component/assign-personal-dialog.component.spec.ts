import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignPersonalDialogComponent } from './assign-personal-dialog.component';

describe('AssignPersonalDialogComponent', () => {
  let component: AssignPersonalDialogComponent;
  let fixture: ComponentFixture<AssignPersonalDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignPersonalDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AssignPersonalDialogComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
