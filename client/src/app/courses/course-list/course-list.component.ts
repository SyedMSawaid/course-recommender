import {Component, OnInit, TemplateRef} from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Observable} from 'rxjs';
import {Enrollment} from '../../_models/Enrollment';
import {Course} from '../../_models/Course';
import {CoursesService} from '../../_services/courses.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {Student} from '../../_models/Student';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: Course[];
  enrollments$: Observable<Enrollment[]>;
  modalRef: BsModalRef;
  enrollmentToDelete: Enrollment;

  constructor(private studentService: StudentsService, private courseService: CoursesService, private modalService: BsModalService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.enrollments$ = this.studentService.showEnrollments();
    this.courseService.getCourses().subscribe(
      next => {
        this.courses = next;
      }
    );
  }

  openModal(template: TemplateRef<any>, enrollment: Enrollment): any {
    this.enrollmentToDelete = enrollment;
    this.modalRef = this.modalService.show(template);
  }

  returnCourseName(courseId: string): string {
    return this.courses.find(x => x.courseId === courseId).courseName;
  }

  sureDelete(enrollment: Enrollment): any {
    this.studentService.deleteEnrollment(enrollment.enrollmentId).subscribe(
      next => {
        console.log(next);
        this.modalRef.hide();
        window.location.reload();
        this.toastr.success('Enrollment Successfully Deleted');
      }, error => {
        console.error(error);
      }
    );
  }

}
