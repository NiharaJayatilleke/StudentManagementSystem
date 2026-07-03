import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SubjectService } from '../../services/subject.service';
import { NotificationService } from '../../services/notification';

@Component({
  selector: 'app-subject-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './subject-edit.html',
  styleUrls: ['./subject-edit.css'],
})

export class SubjectEdit implements OnInit {

  subject: any = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private subjectService: SubjectService,
    private cdr: ChangeDetectorRef,
    private notification: NotificationService) {
  }

  ngOnInit() {

    const id =
      Number(
        this.route.snapshot.paramMap.get('id'));

    console.log(id);

    if (!id || Number.isNaN(id)) {
      this.notification.error('Invalid subject id.');
      this.router.navigate(['/subjects']);
      return;
    }

    this.subjectService
      .getById(id)
      .subscribe({
        next: (result) => {
          this.subject = result || {};
          this.cdr.detectChanges();
        },
        error: () => {
          this.notification.error('Unable to load subject for editing.');
          this.router.navigate(['/subjects']);
        }
      });
  }

  save() {
    if (!this.subject?.id) {
      this.notification.error('Subject could not be loaded for update.');
      return;
    }

    const payload = {
      subjectName: this.subject.subjectName
    };

    this.subjectService
      .update(
        this.subject.id,
        payload)
      .subscribe({
        next: () => {
          this.notification.success('Subject updated successfully');
          this.router.navigate(['/subjects']);
        },
        error: () => {
          this.notification.error('Unable to update subject.');
        }
      });
  }
}





