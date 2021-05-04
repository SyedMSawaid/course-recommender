import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Observable} from 'rxjs';
import {Enrollment} from '../../_models/Enrollment';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: Course[];
  enrollments$: Observable<Enrollment[]>;

  constructor(private studentService: StudentsService, private courseService: CoursesService) { }

  ngOnInit(): void {
    this.enrollments$ = this.studentService.showEnrollments();
    this.courseService.getCourses().subscribe(
      next => {
        this.courses = next;
      }
    );
  }

  returnCourseName(courseId: string): string {
    return this.courses.find(x => x.courseId === courseId).courseName;
  }

}
