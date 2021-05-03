import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {Student} from '../_models/Student';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  baseUrl = environment.baseApi;
  students: Student[] = [];

  constructor(private http: HttpClient) { }

  getStudents(): Observable<Student[]> {
    if (this.students.length > 0) { return of(this.students); }

    return this.http.get<Student[]>(this.baseUrl + 'student').pipe(
      map(students => {
        this.students = students;
        return students;
      })
    );
  }
}
