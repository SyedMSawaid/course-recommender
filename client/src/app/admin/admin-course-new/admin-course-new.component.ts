import { Component, OnInit } from '@angular/core';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-course-new',
  templateUrl: './admin-course-new.component.html',
  styleUrls: ['./admin-course-new.component.css']
})
export class AdminCourseNewComponent implements OnInit {
  model: any = {};

  constructor(private courseService: CoursesService, private router: Router) { }

  ngOnInit(): void {
  }

  createNew(): any {
    return this.courseService.createNewCourse(this.model).subscribe(
      next => {
        console.log(next);
        this.router.navigateByUrl('/admin/courses');
      }, error => {
        console.log(error);
      }
    );
  }

}
