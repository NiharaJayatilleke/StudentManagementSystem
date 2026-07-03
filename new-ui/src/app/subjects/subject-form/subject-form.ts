import { Component } from '@angular/core';
import { SubjectService } from '../../services/subject.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NotificationService } from '../../services/notification';

@Component({
  selector: 'app-subject-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './subject-form.html',
  styleUrl: './subject-form.css',
})
export class SubjectForm {

  subject = {
    subjectId: '',
    subjectName: ''
  };

  subjectForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private subjectService: SubjectService,
    private router: Router,
  private notification: NotificationService) {

    this.subjectForm = this.fb.group({
      subjectId: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(10)
        ]
      ],

      subjectName: [
        '',
        [
          Validators.required
        ]
      ],

    });
  }

  addSubject() {
    this.subjectService
      .add(this.subjectForm.value)
        .subscribe({
        next: result => {
          this.notification.success('Subject added successfully');
          this.router.navigate(['/subjects']);
          console.log('Subject Saved', result);
        },
        error: err => {
          console.error(err);
          const message = err?.error?.message || 'Unable to add subject';
          this.notification.error(message);
        }
      });
  }
}
