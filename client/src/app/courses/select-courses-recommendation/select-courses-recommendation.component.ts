import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-select-courses-recommendation',
  templateUrl: './select-courses-recommendation.component.html',
  styleUrls: ['./select-courses-recommendation.component.css']
})
export class SelectCoursesRecommendationComponent implements OnInit {
  studentId: number;
  courses$: Observable<Course[]>;
  selectedCourses: Course[] = [];

  constructor(private courseService: CoursesService, private studentService: StudentsService, private router: Router) {
    this.studentId = JSON.parse(localStorage.getItem('user')).id;
  }

  ngOnInit(): void {
    this.courses$ = this.courseService.getCourses();
    this.courses$.subscribe(next => console.log(next) );
  }

  checkedItem(event: any, course: Course): void {
    if (event.target.checked) {
      this.selectedCourses.push(course);
    } else {
      this.selectedCourses = this.selectedCourses.filter(n => n.courseId !== course.courseId);
    }
  }

  proceed(): void {
    localStorage.setItem('courses', JSON.stringify(this.selectedCourses));
    this.router.navigateByUrl('/recommended');
  }


}
