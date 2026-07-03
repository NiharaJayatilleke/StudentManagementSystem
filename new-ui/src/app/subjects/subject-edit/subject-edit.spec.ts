import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectEdit } from './subject-edit';

describe('SubjectEdit', () => {
  let component: SubjectEdit;
  let fixture: ComponentFixture<SubjectEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubjectEdit],
    }).compileComponents();

    fixture = TestBed.createComponent(SubjectEdit);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
