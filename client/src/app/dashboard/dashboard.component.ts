import { Component, OnInit } from '@angular/core';
import {Course} from '../_models/Course';
import {StudentsService} from '../_services/students.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  courses: Course[];
  studentId: number;

  constructor(private studentService: StudentsService) {
    this.studentId = JSON.parse(localStorage.getItem('user')).id;
  }

  ngOnInit(): void {
    this.studentService.dashboard(this.studentId).subscribe(
      next => {
        this.courses = next;
      }
    );
  }

}
