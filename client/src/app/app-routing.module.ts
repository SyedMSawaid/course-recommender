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
import {AdminStudentEditComponent} from './admin/admin-student-edit/admin-student-edit.component';
import {AdminStudentNewComponent} from './admin/admin-student-new/admin-student-new.component';
import {EnrollmentEditComponent} from './courses/enrollment-edit/enrollment-edit.component';
import {GiveFeedbackComponent} from './courses/give-feedback/give-feedback.component';
import {DiscussionBoardComponent} from './discussion/discussion-board/discussion-board.component';
import {NewPostComponent} from './discussion/new-post/new-post.component';
import {NewReplyComponent} from './discussion/new-reply/new-reply.component';
import {CoursesListComponent} from './courses-list/courses-list.component';
import {SelectCoursesRecommendationComponent} from './courses/select-courses-recommendation/select-courses-recommendation.component';
import {ProfileComponent} from './profile/profile.component';
import {ChangePasswordComponent} from './change-password/change-password.component';
import {ResetPasswordComponent} from './reset-password/reset-password.component';
import {ForgetPasswordComponent} from './forget-password/forget-password.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'history', component: CourseListComponent },
  { path: 'course/:courseId', component: CourseDetailComponent },
  { path: 'enrollment/:enrollmentId', component: EnrollmentEditComponent },
  { path: 'selection', component: CourseSelectionListComponent },
  { path: 'enter-marks', component: CourseSelectionMarksListComponent },
  { path: 'give-feedback', component: GiveFeedbackComponent },
  { path: 'recommended', component: RecommendedComponent },
  { path: 'select-courses', component: SelectCoursesRecommendationComponent },
  { path: 'discussion/:courseId', component: DiscussionBoardComponent },
  { path: 'new-post', component: NewPostComponent },
  { path: 'new-reply', component: NewReplyComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'profile/change-password', component: ChangePasswordComponent },
  { path: 'resetpassword', component: ResetPasswordComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  { path: 'all-courses', component: CoursesListComponent },
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
      {
        path: 'students',
        children: [
          { path: '', component: AdminStudentListComponent },
          { path: 'edit/:studentId', component: AdminStudentEditComponent },
          { path: 'new', component: AdminStudentNewComponent }
        ]
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
