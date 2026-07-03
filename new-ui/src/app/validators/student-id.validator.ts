import {
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors
} from '@angular/forms';

import { inject } from '@angular/core';

import { StudentService } from '../services/student.service';

import {
  catchError,
  debounceTime,
  map,
  of,
  switchMap,
  timer
} from 'rxjs';

export function studentIdExistsValidator(): AsyncValidatorFn {

  const studentService =
    inject(StudentService);

  return (

    control: AbstractControl

  ) => {

    if (!control.value) {

      return of(null);

    }

    return timer(500).pipe(

      switchMap(() =>

        studentService.exists(control.value)

      ),

      map(exists =>

        exists

          ? { studentIdExists: true }

          : null

      ),

      catchError(() =>

        of(null)

      )

    );

  };

}