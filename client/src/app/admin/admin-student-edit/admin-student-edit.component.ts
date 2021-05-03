import { Component, OnInit } from '@angular/core';
import {Student} from '../../_models/Student';
import {ActivatedRoute, Router} from '@angular/router';
import {StudentsService} from '../../_services/students.service';

@Component({
  selector: 'app-admin-student-edit',
  templateUrl: './admin-student-edit.component.html',
  styleUrls: ['./admin-student-edit.component.css']
})
export class AdminStudentEditComponent implements OnInit {
  studentId: number;
  student: Student;

  constructor(private activatedRoute: ActivatedRoute, private studentService: StudentsService, private router: Router) {
    this.activatedRoute.params.subscribe(params => {
      this.studentId = params.studentId;
    });
  }

  ngOnInit(): void {
    this.studentService.getStudent(this.studentId).subscribe(
      student => {
        this.student = student;
      }
    );
  }

  update(): void {
    this.studentService.updateStudent(this.student).subscribe(next => {
      console.log(next);
    }, error => {
      console.error(error);
    });
    this.router.navigateByUrl('/admin/students');
  }

}
