import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { CommonModule } from '@angular/common';
import { NotificationService } from '../../services/notification';
import { studentIdExistsValidator } from '../../validators/student-id.validator';

@Component({
  selector: 'app-student-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './student-form.html',
  styleUrls: ['./student-form.css']
})

export class StudentForm {

  student = {
    studentId: '',
    name: '',
    age: 0,
    dateOfBirth: '',
    address: ''
  };

  studentForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    private router: Router,
    private notification: NotificationService
  ) {

    this.studentForm = this.fb.group({
      studentId: [
        '',
        {
          validators: [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(10)
          ],
          asyncValidators: [
            studentIdExistsValidator()
          ],
          updateOn: 'blur'
        }
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

  // addStudent() {
  //   console.log('SAVE CLICKED');

  // console.log(this.studentForm.value);
  //   this.studentService
  //     .add(this.student)
  //     .subscribe(() => {
  //       alert('Student added');

  //       this.router.navigate(['/']);
  //     });
  // }

  addStudent() {

    console.log(this.studentForm.value);

    this.studentService
      .add(this.studentForm.value)
      .subscribe({
        next: result => {
          this.notification.success('Student added successfully');
          this.router.navigate(['/students']);
          console.log('Saved', result);
        },
        error: err => {
          this.notification.error('Unable to add student');
          console.error(err);
        }
      });
  }
}