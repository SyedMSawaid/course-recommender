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
  courses: Course[] = [];

  constructor(private http: HttpClient) { }

  getCourses(): Observable<Course[]> {
    if (this.courses.length > 0 ) { return of(this.courses); }

    return this.http.get<Course[]>(this.baseUrl + 'course').pipe(
      map(courses => {
        this.courses = courses;
        return courses;
      })
    );
  }

  getCourse(id: string): Observable<Course> {
    const course = this.courses.find(x => x.courseId === id);
    if (course !== undefined) { return of(course); }
    return this.http.get<Course>(this.baseUrl + `course/${id}`);
  }

  updateCourse(course: Course): any {
    return this.http.put(this.baseUrl + 'course/update', course).pipe(
      map(() => {
        const index = this.courses.indexOf(course);
        this.courses[index] = course;
      })
    );
  }

  createNewCourse(course: Course): any {
    return this.http.post(this.baseUrl + 'course/new', course).subscribe(
      next => {
        this.courses.push(course);
      }
    );
  }

  deleteCourse(course: Course): any {
    return this.http.delete(this.baseUrl + `course/delete/${course.courseId}`).subscribe(
      next => {
        const index = this.courses.indexOf(course);
        this.courses.splice(index, 1);
        console.log(this.courses);
      }
    );
  }
}
