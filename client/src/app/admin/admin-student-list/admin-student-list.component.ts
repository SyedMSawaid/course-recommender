import {Component, OnInit, TemplateRef} from '@angular/core';
import {StudentsService} from '../../_services/students.service';
import {Observable} from 'rxjs';
import {Student} from '../../_models/Student';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-admin-student-list',
  templateUrl: './admin-student-list.component.html',
  styleUrls: ['./admin-student-list.component.css']
})
export class AdminStudentListComponent implements OnInit {
  students$: Observable<Student[]>;
  modalRef: BsModalRef;
  studentToDelete: Student;

  constructor(private studentService: StudentsService, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.students$ = this.studentService.getStudents();
  }

  openModal(template: TemplateRef<any>, student: Student): any {
    this.studentToDelete = student;
    this.modalRef = this.modalService.show(template);
  }

  sureDelete(student: Student): any {
    this.studentService.deleteStudent(student).subscribe(
      next => {
        console.log(next);
      }, error => {
        console.error(error);
      }
    );
  }

}
