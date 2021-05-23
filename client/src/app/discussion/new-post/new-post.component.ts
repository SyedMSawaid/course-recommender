import { Component, OnInit } from '@angular/core';
import {Question} from '../../_models/Question';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent implements OnInit {
  question: any = {};

  constructor(private studentService: StudentsService, private router: Router) { }

  ngOnInit(): void {
    this.question.studentId = JSON.parse(localStorage.getItem('data')).user;
    this.question.courseId = JSON.parse(localStorage.getItem('data')).course;
    console.log(this.question);
  }

  submit(): void {
    this.studentService.newQuestion(this.question).subscribe();
    this.router.navigateByUrl('/discussion/' + this.question.courseId);
  }
}
