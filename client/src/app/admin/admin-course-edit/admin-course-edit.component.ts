import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';
import {Observable} from 'rxjs';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-admin-course-edit',
  templateUrl: './admin-course-edit.component.html',
  styleUrls: ['./admin-course-edit.component.css']
})
export class AdminCourseEditComponent implements OnInit {
  courseId: string;
  course: Course = {
    courseId: '',
    courseName: '',
    courseDescription: '',
    credit: 0,
    newPrimaryKey: ''
  };

  constructor(private activatedRoute: ActivatedRoute, private courseService: CoursesService,
              private router: Router, private toastr: ToastrService) {
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
    this.course.newPrimaryKey = this.course.courseId;
    this.course.courseId = this.courseId;

    this.courseService.updateCourse(this.course).subscribe(next => {
      this.router.navigateByUrl('/admin/courses');
      this.toastr.success('Successfully updated');
    }, error => {
      this.toastr.error(error);
    });
  }

}
