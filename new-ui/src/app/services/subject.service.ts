import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface PagedResult<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private apiUrl =
    'http://localhost:5000/api/subjects';

  constructor(
    private http: HttpClient) {
  }

  getAll(): Observable<any[]> {
   return this.http.get<any[]>(`${this.apiUrl}/all`);
  }

  getPaged(page: number, pageSize: number): Observable<PagedResult<any>> {
    return this.http.get<PagedResult<any>>(
      `${this.apiUrl}/paged?page=${page}&pageSize=${pageSize}`
    );
  }

  getById(id: number) {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  add(subject: any): Observable<any> {

    return this.http.post(
      this.apiUrl,
      subject);
  }

  update(
    id: number,
    subject: any): Observable<any> {

    return this.http.put(
      `${this.apiUrl}/${id}`,
      subject);
  }

  delete(id: number): Observable<any> {

    return this.http.delete(
      `${this.apiUrl}/${id}`);
  }
}