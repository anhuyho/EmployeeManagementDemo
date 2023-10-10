import { Component, OnInit } from '@angular/core';
import { EmployeeService, Employee } from 'src/app/Services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
})
export class EmployeeListComponent implements OnInit {

  employee!: Employee[];
  isLoading: boolean = false;
  loadingTitle!: string;
  constructor(private employeeService: EmployeeService) {
  }
  ngOnInit(): void {
    this.getemployeeues();
  }
  getemployeeues() {
    this.loadingTitle = "loading employee ..."; 
    this.isLoading = true;
    this.employeeService.getemployeeues().subscribe(
      (res: any) => {
        this.employee = res;
        this.isLoading = false;
      }
    );
    
  }
  deleteemployee(employeeId: any) {
    this.employeeService.deleteemployee(employeeId).subscribe(
      (res: any) => {
        this.getemployeeues();
      }
    )
  }
}
