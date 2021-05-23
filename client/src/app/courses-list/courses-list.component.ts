import { Component, OnInit } from '@angular/core';
import {CoursesService} from '../_services/courses.service';
import {Observable} from 'rxjs';
import {Course} from '../_models/Course';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent implements OnInit {
  courses: Observable<Course[]>;

  constructor(private courseService: CoursesService) { }

  ngOnInit(): void {
    this.courses = this.courseService.getCourses();
  }

}
