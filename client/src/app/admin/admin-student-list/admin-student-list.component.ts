import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Observable} from 'rxjs';
import {Student} from '../../_models/Student';

@Component({
  selector: 'app-admin-student-list',
  templateUrl: './admin-student-list.component.html',
  styleUrls: ['./admin-student-list.component.css']
})
export class AdminStudentListComponent implements OnInit {
  students$: Observable<Student[]>;

  constructor(private studentService: StudentsService) { }

  ngOnInit(): void {
    this.students$ = this.studentService.getStudents();
  }

}
