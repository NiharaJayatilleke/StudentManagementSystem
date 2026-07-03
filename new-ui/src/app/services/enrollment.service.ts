import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentService {

  private apiUrl =
    'http://localhost:5000/api/enrollments';

  constructor(
    private http: HttpClient)
  {
  }

    getAll(): Observable<any[]> {
  
      return this.http.get<any[]>(
        this.apiUrl);
    }

  assign(
    studentId: number,
    subjectId: number)
  {

    return this.http.post(
      this.apiUrl,
      {
        studentId,
        subjectId
      });
  }

  // unassign(
  //   studentId: number,
  //   subjectId: number)
  // {

  //   return this.http.delete(

  //     `${this.apiUrl}/${studentId}/${subjectId}`);
  // }

  unassign(
  studentId: number,
  subjectId: number)
{
  return this.http.delete(
    this.apiUrl,
    {
      body: {
        studentId,
        subjectId
      }
    });
}
}