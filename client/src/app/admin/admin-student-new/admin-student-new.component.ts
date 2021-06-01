import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-admin-student-new',
  templateUrl: './admin-student-new.component.html',
  styleUrls: ['./admin-student-new.component.css']
})
export class AdminStudentNewComponent implements OnInit {
  model: any = {};

  constructor(private studentService: StudentsService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  createNew(): any {
    return this.studentService.createNewStudent(this.model).subscribe(
      next => {
        this.toastr.success('Student Created');
        this.router.navigateByUrl('/admin/students');
        console.log(next);
      }, error => {
        console.log(error);
      }
    );
  }

}
