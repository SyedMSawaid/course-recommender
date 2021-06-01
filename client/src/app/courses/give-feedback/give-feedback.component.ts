import { Component, OnInit } from '@angular/core';
import {Course} from '../../_models/Course';
import {NewEnrollment} from '../../_models/NewEnrollment';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-give-feedback',
  templateUrl: './give-feedback.component.html',
  styleUrls: ['./give-feedback.component.css']
})
export class GiveFeedbackComponent implements OnInit {
  studentId: number;
  selectedCourses: Course[];
  enrollmentList: NewEnrollment[] = [];
  x: any;

  constructor(private studentService: StudentsService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.selectedCourses = JSON.parse(localStorage.getItem('courses'));
    this.studentId = JSON.parse(localStorage.getItem('user')).id;
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
    this.studentService.updateEnrollment(this.enrollmentList).subscribe(
      next => {
        localStorage.removeItem('courses');
        this.router.navigateByUrl('dashboard');
        this.toastr.success('Successfully Updated');
      }, error => {
        console.log(error);
      }
    );
  }
}
