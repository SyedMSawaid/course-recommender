import { Component, OnInit } from '@angular/core';
import {Course} from '../../_models/Course';
import {NewEnrollment} from '../../_models/NewEnrollment';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';
import {RecommendationDto} from '../../_models/RecommendationDto';
import {Toast, ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-recommended',
  templateUrl: './recommended.component.html',
  styleUrls: ['./recommended.component.css']
})
export class RecommendedComponent implements OnInit {singleModel = '1';
  studentId: number;
  selectedCourses: Course[];
  recommendationList: RecommendationDto;
  x: any;

  constructor(private studentService: StudentsService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.selectedCourses = JSON.parse(localStorage.getItem('courses'));
    if (this.selectedCourses.length === 0) {
      this.toastr.error('Select courses');
      this.router.navigateByUrl('/select-courses');
    }
    this.studentId = JSON.parse(localStorage.getItem('user')).id;
    this.recommendationList = {
      studentId: this.studentId,
      coursesList: []
    };

    this.selectedCourses.forEach((course) => {
      this.recommendationList.coursesList.push(course.courseId);
    });

    this.x = this.studentService.getRecommendation(this.recommendationList);
  }

  getCourseName(courseId: string): string {
    return this.selectedCourses.find(x => x.courseId === courseId).courseName;
  }

  optcourse(courseId: string): any {
    const course = [
      { courseId, courseName: this.getCourseName(courseId) },
    ];
    localStorage.setItem('courses', JSON.stringify(course));
    this.router.navigateByUrl('/enter-marks');
  }
}
