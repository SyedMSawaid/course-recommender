import { Component, OnInit, TemplateRef } from '@angular/core';
import {CoursesService} from '../../_services/courses.service';
import {Observable} from 'rxjs';
import {Course} from '../../_models/Course';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-admin-course-list',
  templateUrl: './admin-course-list.component.html',
  styleUrls: ['./admin-course-list.component.css']
})
export class AdminCourseListComponent implements OnInit {
  courses$: Observable<Course[]>;
  modalRef: BsModalRef;
  courseToDelete: Course;

  constructor(private courseService: CoursesService, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.courses$ = this.courseService.getCourses();
  }

  openModal(template: TemplateRef<any>, course: Course): any {
    this.courseToDelete = course;
    this.modalRef = this.modalService.show(template);
  }

  sureDelete(course: Course): any {
    this.courseService.deleteCourse(course);
  }

}
