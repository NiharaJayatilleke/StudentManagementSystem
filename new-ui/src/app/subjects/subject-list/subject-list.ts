import { ChangeDetectorRef, Component } from '@angular/core';
import { PagedResult, SubjectService } from '../../services/subject.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { NotificationService } from '../../services/notification';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-subject-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './subject-list.html',
  styleUrls: ['./subject-list.css'],
})
export class SubjectList {

  subjects: any[] = [];
  page = 1;
  pageSize = 10;
  totalCount = 0;
  totalPages = 0;
  isLoading = false;

  constructor(
    private subjectService: SubjectService,
    private cdr: ChangeDetectorRef,
    private notification: NotificationService,
    private dialog: MatDialog) {
  }

  ngOnInit() {
    this.loadSubjects();
  }

  // loadSubjects() {

  //   this.subjectService
  //     .getAll()
  //     .subscribe(result => {
  //       this.subjects = result;
  //       this.cdr.detectChanges();
  //     });
  // }
    loadSubjects() {
    this.isLoading = true;

    this.subjectService
      .getPaged(this.page, this.pageSize)
      .subscribe({
        next: (result: PagedResult<any>) => {
          this.subjects = result.items;
          this.totalCount = result.totalCount;
          this.totalPages = result.totalPages;
          this.isLoading = false;
          this.cdr.detectChanges();
        },
        error: () => {
          this.isLoading = false;
          this.notification.error('Unable to load subjects.');
        }
      });
  }

  goToPage(page: number) {
    if (page < 1 || page > this.totalPages || page === this.page) {
      return;
    }
    this.page = page;
    this.loadSubjects();
  }

  prevPage() {
    this.goToPage(this.page - 1);
  }

  nextPage() {
    this.goToPage(this.page + 1);
  }

  delete(id: number) {

    const dialogRef = this.dialog.open(

      ConfirmDialogComponent,
      {
        width: '350px',
        data: {
          message: 'Delete this subject?'
        }
      }
    );

    dialogRef.afterClosed().subscribe(result => {

      if (result) {

        this.subjectService.delete(id).subscribe(() => {

          this.notification.success(
            'Subject deleted successfully');

          this.loadSubjects();

        });

      }

    });
  }
}
