import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { NotificationService } from '../../services/notification';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-edit',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './student-edit.html',
  styleUrls: ['./student-edit.css']
})
export class StudentEdit implements OnInit {

  studentForm: FormGroup;
  studentDbId!: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private studentService: StudentService,
    private cdr: ChangeDetectorRef,
    private notification: NotificationService) {

    this.studentForm = this.fb.group({
      studentId: [
        { value: '', disabled: true },
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(10)
        ]
      ],
      name: [
        '',
        [
          Validators.required
        ]
      ],
      age: [
        null,
        [
          Validators.required,
          Validators.min(18),
          Validators.max(100)
        ]
      ],
      dateOfBirth: [
        '',
        Validators.required
      ],
      address: [
        '',
        [
          Validators.required,
          Validators.minLength(5)
        ]
      ]
    });
  }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.studentDbId = id;

    this.studentService
      .getById(id)
      .subscribe(result => {
        this.studentForm.patchValue({
          studentId: result.studentId,
          name: result.name,
          age: result.age,
          dateOfBirth: result.dateOfBirth?.split('T')[0],
          address: result.address
        });
        this.cdr.detectChanges();
      });
  }

  save() {
    if (this.studentForm.invalid) {
      this.studentForm.markAllAsTouched();
      return;
    }

    const updatedStudent = {
      ...this.studentForm.getRawValue(),
      studentId: this.studentForm.get('studentId')?.value
    };

    this.studentService
      .update(this.studentDbId, updatedStudent)
      .subscribe(() => {
        this.notification.success('Student updated successfully');
        this.router.navigate(['/students']);
      });
  }

  get studentId() {
    return this.studentForm.get('studentId');
  }

  get name() {
    return this.studentForm.get('name');
  }

  get age() {
    return this.studentForm.get('age');
  }

  get dateOfBirth() {
    return this.studentForm.get('dateOfBirth');
  }

  get address() {
    return this.studentForm.get('address');
  }
}




