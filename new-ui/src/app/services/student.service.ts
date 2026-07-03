import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = 'http://localhost:5000/api/students';

  constructor(private http: HttpClient) { }

  // getAll() {
  //   return this.http.get(this.apiUrl);
  // }

  getAll(): Observable<any[]> {

    return this.http.get<any[]>(
      this.apiUrl);
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // exists(studentId: string) {
  //   return this.http.get<boolean>(`${this.apiUrl}/exists/${studentId}`);
  // }
  exists(studentId: string) {

    const params = new HttpParams()
      .set('studentId', studentId);

    return this.http.get<boolean>(
      `${this.apiUrl}/exists`,
      { params }
    );
  }

  add(student: any) {
    return this.http.post(this.apiUrl, student);
  }

  update(id: number, student: any) {
    return this.http.put(`${this.apiUrl}/${id}`, student);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
