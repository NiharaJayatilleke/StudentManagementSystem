import { Routes } from '@angular/router';
import { StudentList } from './students/student-list/student-list';
import { StudentForm } from './students/student-form/student-form';
import { SubjectList } from './subjects/subject-list/subject-list';
import { SubjectForm } from './subjects/subject-form/subject-form';
import { EnrollmentForm } from './enrollments/enrollment-form/enrollment-form';
import { StudentEdit } from './students/student-edit/student-edit';
import { SubjectEdit } from './subjects/subject-edit/subject-edit';
import { LoginComponent } from './auth/login/login';
import { authGuard } from './guards/auth-guard';
import { RegisterComponent } from './auth/register/register';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: '',
        canActivate: [authGuard],
        children: [
            {
                path: '',
                redirectTo: 'students',
                pathMatch: 'full'
            },
            {
                path: 'students',
                component: StudentList,
            },
            {
                path: 'students/add',
                component: StudentForm
            },
            {
                path: 'students/edit/:id',
                component: StudentEdit
            },
            {
                path: 'subjects',
                component: SubjectList
            },
            {
                path: 'subjects/add',
                component: SubjectForm
            },
            {
                path: 'subjects/edit/:id',
                component: SubjectEdit
            },
            {
                path: 'enrollments',
                component: EnrollmentForm
            }
        ]
    }
];
