import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {Student} from '../_models/Student';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {map} from 'rxjs/operators';
import {Course} from '../_models/Course';

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

  getStudent(id: number): Observable<Student> {
    const student = this.students.find(x => x.id === id);
    if (student !== undefined) { return of(student); }
    return this.http.get<Student>(this.baseUrl + `student/${id}`);
  }

  updateStudent(student: Student): any {
    return this.http.put(this.baseUrl + 'student/update', student).pipe(
      map(() => {
        const index = this.students.indexOf(student);
        this.students[index] = student;
      })
    );
  }

  createNewStudent(student: Student): any {
    return this.http.post(this.baseUrl + 'student/new', student).subscribe(
      next => {
        this.students.push(student);
      }
    );
  }

  deleteStudent(student: Student): any {
    return this.http.delete(this.baseUrl + `student/delete/${student.id}`).subscribe(
      next => {
        const index = this.students.indexOf(student);
        this.students.splice(index, 1);
        console.log(this.students);
      }
    );
  }
}
