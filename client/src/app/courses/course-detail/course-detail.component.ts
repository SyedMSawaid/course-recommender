import { Component, OnInit } from '@angular/core';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  course: Course;
  courseId: string;

  constructor(private courseService: CoursesService, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(params => {
      this.courseId = params.courseId;
    });
  }

  ngOnInit(): void {
    this.courseService.getCourse(this.courseId).subscribe(
      next => {
        this.course = next;
      }
    );
  }

}
