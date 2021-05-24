import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Course} from '../../_models/Course';
import {NewEnrollment} from '../../_models/NewEnrollment';
import {Router} from '@angular/router';
import {CoursesService} from '../../_services/courses.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-course-selection-marks-list',
  templateUrl: './course-selection-marks-list.component.html',
  styleUrls: ['./course-selection-marks-list.component.css']
})
export class CourseSelectionMarksListComponent implements OnInit {
  studentId: number;
  selectedCourses: Course[];
  enrollmentList: NewEnrollment[] = [];
  x: any;

  constructor(private studentService: StudentsService, private coursesService: CoursesService, private router: Router) { }

  ngOnInit(): void {
    this.selectedCourses = JSON.parse(localStorage.getItem('courses'));
    this.studentId = JSON.parse(localStorage.getItem('user')).id;
    console.log(this.selectedCourses);
    for (const course of this.selectedCourses) {
      const enrollment: NewEnrollment = {
        courseId: course.courseId,
        studentId: this.studentId,
        marks: 0
      };
      this.enrollmentList.push(enrollment);
    }
  }

  getCourseName(courseId: string): string {
    return this.selectedCourses.find(x => x.courseId === courseId).courseName;
  }

  onSubmit(): void {
    console.log(this.enrollmentList);
    for (const enrollment of this.enrollmentList) {
      this.studentService.createNewEnrollment(enrollment);
      console.log('Sending Enrollment!!!');
    }
    localStorage.removeItem('courses');
    this.router.navigateByUrl('dashboard');
  }

}
