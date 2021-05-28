import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../_services/students.service';
import {Observable} from 'rxjs';
import {Student} from '../_models/Student';
import {AccountService} from '../_services/account.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  student: Observable<Student>;
  studentId: number;

  constructor(private studentService: StudentsService, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.studentId = JSON.parse(localStorage.getItem('user')).id;
    this.student = this.studentService.getStudent(this.studentId);
  }

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
