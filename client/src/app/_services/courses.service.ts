import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {Course} from '../_models/Course';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  baseUrl = environment.baseApi;

  constructor(private http: HttpClient) { }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + 'course').pipe(
      map(courses => {
        return courses;
      })
    );
  }

  getCourse(id: string): Observable<Course> {
    return this.http.get<Course>(this.baseUrl + `course/${id}`);
  }

  updateCourse(course: Course): Observable<Course> {
    return this.http.put<Course>(this.baseUrl + 'course/update', course);
  }

  createNewCourse(course: Course): Observable<Course> {
    return this.http.post<Course>(this.baseUrl + 'course/new', course);
  }

  deleteCourse(course: Course): Observable<Course> {
    return this.http.delete<Course>(this.baseUrl + `course/delete/${course.courseId}`);
  }
}
