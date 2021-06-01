import { Component, OnInit } from '@angular/core';
import {Question} from '../../_models/Question';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent implements OnInit {
  question: any = {};

  constructor(private studentService: StudentsService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.question.studentId = JSON.parse(localStorage.getItem('data')).user;
    this.question.courseId = JSON.parse(localStorage.getItem('data')).course;
    console.log(this.question);
  }

  submit(): void {
    this.studentService.newQuestion(this.question).subscribe(
      next => {
        this.router.navigateByUrl('/discussion/' + this.question.courseId);
        this.toastr.success('New Question Added');
      }, error => {
        console.log(error);
      }
    );
  }
}
