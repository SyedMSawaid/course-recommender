import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {StudentsService} from '../../_services/students.service';
import {Enrollment} from '../../_models/Enrollment';

@Component({
  selector: 'app-enrollment-edit',
  templateUrl: './enrollment-edit.component.html',
  styleUrls: ['./enrollment-edit.component.css']
})
export class EnrollmentEditComponent implements OnInit {
  enrollmentId: number;
  enrollment: Enrollment;
  model: any = {};

  constructor(private activatedRoute: ActivatedRoute, private studentService: StudentsService, private router: Router) {
    this.activatedRoute.params.subscribe(params => {
      this.enrollmentId = params.enrollmentId;
    });
  }

  ngOnInit(): void {
    console.log(this.studentService.getEnrollment(this.enrollmentId));
  }

  update(): void {
    this.model.enrollmentId = this.enrollmentId;
    this.studentService.editEnrollment(this.model).subscribe(next => {
      console.log(next);
      }, error => {
        console.error(error);
      });
    this.router.navigateByUrl('/history');
  }

}
