import { Component, OnInit } from '@angular/core';
import {Student} from '../../_models/Student';
import {ActivatedRoute, Router} from '@angular/router';
import {StudentsService} from '../../_services/students.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-admin-student-edit',
  templateUrl: './admin-student-edit.component.html',
  styleUrls: ['./admin-student-edit.component.css']
})
export class AdminStudentEditComponent implements OnInit {
  studentId: number;
  student: Student;

  constructor(private activatedRoute: ActivatedRoute, private studentService: StudentsService,
              private router: Router, private toastr: ToastrService) {
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
      this.toastr.success('Student Updated');
      console.log(next);
      this.router.navigateByUrl('/admin/students');
    }, error => {
      console.error(error);
    });
  }

}
