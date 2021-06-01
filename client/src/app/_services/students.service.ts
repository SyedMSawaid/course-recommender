import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {Student} from '../_models/Student';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Enrollment} from '../_models/Enrollment';
import {map} from 'rxjs/operators';
import {Course} from '../_models/Course';
import {NewEnrollment} from '../_models/NewEnrollment';
import {Question} from '../_models/Question';
import {Reply} from '../_models/Reply';
import {RecommendationDto} from '../_models/RecommendationDto';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  baseUrl = environment.baseApi;

  constructor(private http: HttpClient) {
  }

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseUrl + 'student').pipe(
      map(students => {
        return students;
      })
    );
  }

  getStudent(id: number): Observable<Student> {
    return this.http.get<Student>(this.baseUrl + `student/${id}`).pipe(
      map(student => {
        return student;
      })
    );
  }

  updateStudent(student: Student): Observable<Student> {
    return this.http.put<Student>(this.baseUrl + 'student/update', student);
  }

  createNewStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(this.baseUrl + 'student/new', student);
  }

  deleteStudent(student: Student): Observable<Student> {
    return this.http.delete<Student>(this.baseUrl + `student/delete/${student.id}`);
  }

  showEnrollments(): Observable<Enrollment[]> {
    const user = JSON.parse(localStorage.getItem('user'));
    return this.http.get<Enrollment[]>(this.baseUrl + `student/enrollments/${user.id}`);
  }

  getEnrollment(id: number): Observable<Enrollment> {
    return this.http.get<Enrollment>(this.baseUrl + 'student/enrollment/' + id);
  }

  dashboard(id: number): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + `student/dashboard/${id}`).pipe(
      map(courses => {
        return courses;
      })
    );
  }

  editEnrollment(enrollment: Enrollment): Observable<Enrollment> {
    return this.http.put<Enrollment>(this.baseUrl + 'student/enrollment/singleupdate', enrollment);
  }

  deleteEnrollment(id: number): Observable<Enrollment> {
    return this.http.delete<Enrollment>(this.baseUrl + `student/enrollment/delete/${id}`);
  }

  studentNotEnrolledIn(id: number): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + `student/list-of-courses/${id}`);
  }

  createNewEnrollment(enrollment: NewEnrollment): Observable<Enrollment> {
    return this.http.post<Enrollment>(this.baseUrl + `student/enroll`, enrollment);
  }

  updateEnrollment(enrollment: NewEnrollment[]): Observable<Enrollment> {
    return this.http.put<Enrollment>(this.baseUrl + `student/enrollment/update`, enrollment);
  }

  // Discussion Boards
  getAllQuestions(courseId: string): Observable<Question[]> {
    return this.http.get<Question[]>(this.baseUrl + `course/question/${courseId}`);
  }

  getAllQuestionsOfStudent(): any {
    const user = JSON.parse(localStorage.getItem('user'));
    return this.http.get(this.baseUrl + `student/question/${user.id}`);
  }

  getRepliesOfQuestion(questionId: number): Observable<Reply[]> {
    return this.http.get<Reply[]>(this.baseUrl + `course/question/replies/${questionId}`);
  }

  newQuestion(question: Question): Observable<Question> {
    return this.http.post<Question>(this.baseUrl + `course/question/new`, question);
  }

  newReply(reply: Reply): Observable<Reply> {
    return this.http.post<Reply>(this.baseUrl + `course/question/reply/new`, reply);
  }

  getRecommendation(dto: RecommendationDto): any {
    return this.http.post(this.baseUrl + `student/getrecommendation`, dto);
  }
}
