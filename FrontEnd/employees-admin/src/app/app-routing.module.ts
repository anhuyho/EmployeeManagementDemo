import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { employeeCreateComponent } from './Pages/employee/create/employee-create.component';
import { EmployeeListComponent } from './Pages/employee/get/employee-list.component';
import { employeeEditComponent } from './Pages/employee/edit/employee-edit.component';

const routes: Routes = [

  { path: '', component: EmployeeListComponent, title: 'Home page' },
  { path: 'employee', component: EmployeeListComponent, title: 'employee' },
  { path: 'employee/create', component: employeeCreateComponent, title: 'Create employee' },
  { path: 'employee/:id', component: employeeEditComponent, title: 'Edit employee' },
  { path: '**', component: EmployeeListComponent, title: 'Employee' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
