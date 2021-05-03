import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './login/login.component';
import {HomeComponent} from './home/home.component';
import {SignupComponent} from './signup/signup.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {CourseListComponent} from './courses/course-list/course-list.component';
import {CourseDetailComponent} from './courses/course-detail/course-detail.component';
import {CourseSelectionListComponent} from './courses/course-selection-list/course-selection-list.component';
import {CourseSelectionMarksListComponent} from './courses/course-selection-marks-list/course-selection-marks-list.component';
import {RecommendedComponent} from './courses/recommended/recommended.component';
import {AdminDashboardComponent} from './admin/admin-dashboard/admin-dashboard.component';
import {AdminCourseListComponent} from './admin/admin-course-list/admin-course-list.component';
import {AdminStudentListComponent} from './admin/admin-student-list/admin-student-list.component';
import {AdminCourseEditComponent} from './admin/admin-course-edit/admin-course-edit.component';
import {AdminCourseNewComponent} from './admin/admin-course-new/admin-course-new.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'history', component: CourseListComponent },
  { path: 'course-detail', component: CourseDetailComponent },
  { path: 'selection', component: CourseSelectionListComponent },
  { path: 'enter-marks', component: CourseSelectionMarksListComponent },
  { path: 'recommended', component: RecommendedComponent },
  {
    path: 'admin',
    children: [
      { path: 'dashboard', component: AdminDashboardComponent },
      {
        path: 'courses',
        children: [
          { path: '', component: AdminCourseListComponent },
          { path: 'edit/:courseId', component: AdminCourseEditComponent },
          { path: 'new', component: AdminCourseNewComponent },
        ]
      },
      { path: 'students', component: AdminStudentListComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
