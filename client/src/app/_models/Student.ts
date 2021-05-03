import {Enrollment} from './Enrollment';

export interface Student {
  studentId: string;
  enrollments: Enrollment[];
}
