import { Component, OnInit } from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Question} from '../../_models/Question';
import {Reply} from '../../_models/Reply';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-discussion-board',
  templateUrl: './discussion-board.component.html',
  styleUrls: ['./discussion-board.component.css']
})
export class DiscussionBoardComponent implements OnInit {
  userId: number;
  courseId: string;
  questions: Question[];
  replies: Reply[] = [];

  constructor(private studentService: StudentsService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.params.subscribe(params => {
      this.courseId = params.courseId;
    });
  }

  ngOnInit(): void {
    this.userId = JSON.parse(localStorage.getItem('user')).id;
    this.studentService.getAllQuestions(this.courseId).subscribe(
      next => {
        this.questions = next;
        this.loadReplies();
      }
    );
  }

  getStudentName(id: number): any {
    this.studentService.getStudent(id).subscribe(
      next => {
        return next.userName;
      }
    );
  }

  newpost(): void {
    const item =  {
      user: this.userId,
      course: this.courseId
    };
    localStorage.setItem('data', JSON.stringify(item));
    this.router.navigateByUrl('new-post');
  }

  newreply(question: Question): any {
    const item = {
      question: question.questionId,
      user: this.userId,
      query: question.query,
      courseId: this.courseId
    };
    localStorage.setItem('data', JSON.stringify(item));
    this.router.navigateByUrl('new-reply');
  }

  getReplies(questionId: number): any {
    return this.replies.filter(x => x.questionId === questionId);
  }

  loadReplies(): any {
    this.questions.forEach((question) => {
      this.studentService.getRepliesOfQuestion(question.questionId).subscribe(
        next => {
          if (next.length > 0) {
            this.replies.push(...next);
          }
        }
      );
    });
  }
}
