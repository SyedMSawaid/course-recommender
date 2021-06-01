import { Component, OnInit } from '@angular/core';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-admin-course-new',
  templateUrl: './admin-course-new.component.html',
  styleUrls: ['./admin-course-new.component.css']
})
export class AdminCourseNewComponent implements OnInit {
  model: any = {};

  constructor(private courseService: CoursesService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  createNew(): any {
    return this.courseService.createNewCourse(this.model).subscribe(
      next => {
        this.toastr.success('Course Created');
        this.router.navigateByUrl('/admin/courses');
      }, error => {
        console.log(error);
      }
    );
  }

}
