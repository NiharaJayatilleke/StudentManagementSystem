import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { StudentService } from '../../services/student.service';
import { SubjectService } from '../../services/subject.service';
import { EnrollmentService } from '../../services/enrollment.service';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from '../../services/notification';

@Component({
  selector: 'app-enrollment-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './enrollment-form.html',
  styleUrl: './enrollment-form.css',
})
export class EnrollmentForm {
  students: any[] = [];

  subjects: any[] = [];

  enrollments: any[] = [];

  // studentId = 0;
  // subjectId = 0;

  studentId: number | null = null;
  subjectId: number | null = null;

  constructor(
    private studentService: StudentService,
    private subjectService: SubjectService,
    private enrollmentService: EnrollmentService,
    private cdr: ChangeDetectorRef,
    private notification: NotificationService,
    private dialog: MatDialog) { }

  ngOnInit() {

    this.studentService
      .getAll()
      .subscribe(result => {
        this.students = result;
        // this.cdr.detectChanges();
      });

    this.subjectService
      .getAll()
      .subscribe(result => {
        this.subjects = result || [];
        // this.cdr.detectChanges();
      });

    this.loadEnrollments();
  }

  loadEnrollments() {

    this.enrollmentService
      .getAll()
      .subscribe(result => {
        this.enrollments = result;
        this.cdr.detectChanges();
      });
  }

  assign() {
    this.enrollmentService
      .assign(
        this.studentId!,
        this.subjectId!)
      .subscribe(() => {
        this.notification.success('Subject assigned successfully');
        this.loadEnrollments();
      });
  }

  unassign(studentId: number, subjectId: number) {

    const dialogRef = this.dialog.open(
      ConfirmDialogComponent,
      {
        width: '350px',
        data: {
          message: 'Delete this enrollment?'
        }
      }
    );

    dialogRef.afterClosed().subscribe(result => {

      if (result) {

        this.enrollmentService
          .unassign(studentId, subjectId)
          .subscribe(() => {
            this.notification.success(
              'Subject unassigned successfully');
            this.loadEnrollments();
          });

      }

    });
  }
}

