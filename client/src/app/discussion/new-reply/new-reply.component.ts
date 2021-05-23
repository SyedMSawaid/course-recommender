import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-new-reply',
  templateUrl: './new-reply.component.html',
  styleUrls: ['./new-reply.component.css']
})
export class NewReplyComponent implements OnInit {
  question: string;
  courseId: string;
  reply: any = {};

  constructor(private studentService: StudentsService, private router: Router) { }

  ngOnInit(): void {
    this.reply.studentId = JSON.parse(localStorage.getItem('data')).user;
    this.reply.questionId = JSON.parse(localStorage.getItem('data')).question;
    this.courseId = JSON.parse(localStorage.getItem('data')).courseId;
    this.question = JSON.parse(localStorage.getItem('data')).query;
  }

  submit(): void {
    this.studentService.newReply(this.reply).subscribe(
      next => {
        console.log(next);
      }
    );
    this.router.navigateByUrl('/discussion/' + this.courseId);
  }

}
