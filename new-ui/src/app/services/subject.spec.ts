import { TestBed } from '@angular/core/testing';

import { Subject } from './subject.service';

describe('Subject', () => {
  let service: Subject;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Subject);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
