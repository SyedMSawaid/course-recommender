import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-admin-course-edit',
  templateUrl: './admin-course-edit.component.html',
  styleUrls: ['./admin-course-edit.component.css']
})
export class AdminCourseEditComponent implements OnInit {
  courseId: string;
  course: Course;

  constructor(private activatedRoute: ActivatedRoute, private courseService: CoursesService, private router: Router) {
    this.activatedRoute.params.subscribe(params => {
      this.courseId = params.courseId;
    });
  }

  ngOnInit(): void {
    this.courseService.getCourse(this.courseId).subscribe(
      course => {
        this.course = course;
      }
    );
  }

  update(): void {
    console.log(this.course);
    this.course.newPrimaryKey = this.course.courseId;
    this.course.courseId = this.courseId;
    console.log(this.course);

    this.courseService.updateCourse(this.course).subscribe(next => {
      console.log(next);
    }, error => {
      console.error(error);
    });
    this.router.navigateByUrl('/admin/courses');
  }

}
