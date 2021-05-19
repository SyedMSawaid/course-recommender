import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {Student} from '../_models/Student';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Enrollment} from '../_models/Enrollment';
import {map} from 'rxjs/operators';
import {Course} from '../_models/Course';
import {NewEnrollment} from '../_models/NewEnrollment';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  baseUrl = environment.baseApi;

  constructor(private http: HttpClient) { }

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseUrl + 'student').pipe(
      map(students => {
        return students;
      })
    );
  }

  getStudent(id: number): Observable<Student> {
    return this.http.get<Student>(this.baseUrl + `student/${id}`);
  }

  updateStudent(student: Student): any {
    return this.http.put(this.baseUrl + 'student/update', student);
  }

  createNewStudent(student: Student): any {
    return this.http.post(this.baseUrl + 'student/new', student);
  }

  deleteStudent(student: Student): any {
    return this.http.delete(this.baseUrl + `student/delete/${student.id}`);
  }

  showEnrollments(): any {
    const user = JSON.parse(localStorage.getItem('user'));
    return this.http.get(this.baseUrl + `student/enrollments/${user.id}`);
  }

  getEnrollment(id: number): any {
    this.http.get<Enrollment>(this.baseUrl + 'student/enrollment/' + id).subscribe(
      next => {
        return next;
      }
    );
  }

  editEnrollment(enrollment: Enrollment): any {
    return this.http.put(this.baseUrl + 'student/enrollment/update', enrollment);
  }

  deleteEnrollment(id: number): any {
    return this.http.delete(this.baseUrl + `student/enrollment/delete/${id}`);
  }

  studentNotEnrolledIn(id: number): any {
    return this.http.get<Course>(this.baseUrl + `student/list-of-courses/${id}`);
  }

  createNewEnrollment(enrollment: NewEnrollment): any {
    return this.http.post(this.baseUrl + `student/enroll`, enrollment).subscribe(
      next => {
        return next;
      }
    );
  }
}
