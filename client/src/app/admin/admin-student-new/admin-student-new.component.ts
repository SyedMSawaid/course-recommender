import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-student-new',
  templateUrl: './admin-student-new.component.html',
  styleUrls: ['./admin-student-new.component.css']
})
export class AdminStudentNewComponent implements OnInit {
  model: any = {};

  constructor(private studentService: StudentsService, private router: Router) { }

  ngOnInit(): void {
  }

  createNew(): any {
    return this.studentService.createNewStudent(this.model).subscribe(
      next => {
        console.log(next);
        this.router.navigateByUrl('/admin/student');
      }, error => {
        console.log(error);
      }
    );
  }

}
