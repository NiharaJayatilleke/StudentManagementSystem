import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentService } from '../../services/student.service';
import { RouterLink } from '@angular/router';
import { NotificationService } from '../../services/notification';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './student-list.html',
  styleUrls: ['./student-list.css']
})
export class StudentList implements OnInit {

  students: any[] = [];

  constructor(
    private studentService: StudentService,
    private cdr: ChangeDetectorRef,
    private notification: NotificationService,
    private dialog: MatDialog) {
  }

  delete(id: number) {

    // if (!confirm('Delete this student?')) {
    //   return;
    // }

    const dialogRef = this.dialog.open(

      ConfirmDialogComponent,

      {

        width: '350px',
        panelClass: 'danger-dialog',
        data: {

          message: 'Delete this student?'

        }

      }

    );

    dialogRef.afterClosed().subscribe(result => {

      if (result) {

        this.studentService.delete(id).subscribe(() => {

          this.notification.success(
            'Student deleted successfully');

          this.loadStudents();

        });

      }

    });

    // this.studentService
    //   .delete(id)
    //   .subscribe(() => {
    //     this.notification.success('Student deleted successfully');
    //     this.loadStudents();
    //   });
  }

  edit(id: number) {

    console.log('Edit student with ID:', id);

    // this.studentService
    //   .edit(id)
    //   .subscribe(() => {
    //     this.loadStudents();
    //   });
  }

  ngOnInit(): void {

    console.log('StudentList initialized');

    this.loadStudents();
  }

  loadStudents() {

    // console.log('BEFORE:', this.students);

    this.studentService
      .getAll()
      .subscribe({
        next: result => {

          // console.log('API RESULT:', result);

          this.students = [...result];

          // Force UI refresh after API response.
          // Required due to Angular hydration/change detection behavior
          this.cdr.detectChanges();

          // console.log('AFTER ASSIGNMENT:', this.students);
        },
        error: err => {

          console.error('ERROR:', err);
        },
        complete: () => {

          console.log('COMPLETE');
        }
      });
  }

  // loadStudents() {

  //   this.studentService
  //     .getAll()
  //     .subscribe(result => {

  //       console.log('Students received:', result);

  //       this.students = result;
  //     });
  // }

  // ngDoCheck(): void {

  //   console.log(
  //     'Current students:',
  //     this.students.length,
  //     this.students);
  // }
}