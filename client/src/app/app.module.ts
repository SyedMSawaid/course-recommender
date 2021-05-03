import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CourseDetailComponent } from './courses/course-detail/course-detail.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ModalModule } from 'ngx-bootstrap/modal';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { FooterComponent } from './footer/footer.component';
import { CourseSelectionListComponent } from './courses/course-selection-list/course-selection-list.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { CourseSelectionMarksListComponent } from './courses/course-selection-marks-list/course-selection-marks-list.component';
import { RecommendedComponent } from './courses/recommended/recommended.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminCourseEditComponent } from './admin/admin-course-edit/admin-course-edit.component';
import { AdminStudentEditComponent } from './admin/admin-student-edit/admin-student-edit.component';
import { AdminStudentListComponent } from './admin/admin-student-list/admin-student-list.component';
import { AdminCourseListComponent } from './admin/admin-course-list/admin-course-list.component';
import {HttpClientModule} from '@angular/common/http';
import { AdminCourseNewComponent } from './admin/admin-course-new/admin-course-new.component';
import { AdminStudentNewComponent } from './admin/admin-student-new/admin-student-new.component';

@NgModule({
  declarations: [
    AppComponent,
    CourseDetailComponent,
    NavbarComponent,
    LoginComponent,
    SignupComponent,
    HomeComponent,
    DashboardComponent,
    CourseListComponent,
    FooterComponent,
    CourseSelectionListComponent,
    CourseSelectionMarksListComponent,
    RecommendedComponent,
    AdminDashboardComponent,
    AdminCourseEditComponent,
    AdminStudentEditComponent,
    AdminStudentListComponent,
    AdminCourseListComponent,
    AdminCourseNewComponent,
    AdminStudentNewComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    CollapseModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
