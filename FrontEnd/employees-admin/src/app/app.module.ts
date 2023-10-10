import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Pages/Partials/navbar/navbar.component';
import { employeeCreateComponent } from './Pages/employee/create/employee-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {  HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { LoaderComponent } from './Pages/Partials/loader/loader.component';
import { EmployeeListComponent } from './Pages/employee/get/employee-list.component';
import { employeeEditComponent } from './Pages/employee/edit/employee-edit.component';
import { NgToastModule } from 'ng-angular-popup'

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { AuthService } from './Services/auth.service';
import { AuthInterceptor } from './Services/auth-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    employeeCreateComponent,
    LoaderComponent,
    EmployeeListComponent,
    employeeEditComponent,
    employeeCreateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgToastModule,
    BsDatepickerModule.forRoot()
  ],
  providers: [
    AuthService, {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
