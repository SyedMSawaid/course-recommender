import { Component, OnInit } from '@angular/core';
import {Course} from '../_models/Course';
import {StudentsService} from '../_services/students.service';
import {stringify} from 'querystring';
import { Router} from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  courses: Course[];
  studentId: number;

  constructor(private studentService: StudentsService, private router: Router) {
    this.studentId = JSON.parse(localStorage.getItem('user')).id;
  }

  ngOnInit(): void {
    this.studentService.dashboard(this.studentId).subscribe(
      next => {
        this.courses = next;
      }
    );
  }

  givefeedback(): void {
    localStorage.setItem('courses', JSON.stringify(this.courses));
    this.router.navigateByUrl('/give-feedback');
  }

}
