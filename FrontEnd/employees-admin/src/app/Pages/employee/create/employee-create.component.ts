import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { EmployeeService as EmployeeService } from 'src/app/Services/employee.service';

function validInteger(control : any) {
  const value = control.value;
  if (value === null || value === '') {
    return null; // Allow empty input
  }
  const isValid = !isNaN(value) && Number.isInteger(Number(value));
  return isValid ? null : { invalidInteg: true };
}

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
})
export class employeeCreateComponent implements OnInit {
  
  employeeForm!: FormGroup;
  isLoading: boolean = false;
  loadingTitle: string = 'Loading ...';
  
  
  ngOnInit(): void {
     this.employeeForm = this.formBuilder.group({
        name: ['', Validators.required],
        hiringDate: [new Date(), Validators.required],
        position:[],
        salary: ['', [Validators.required, validInteger]],
                  
        });
  }
  constructor(private employeeService: EmployeeService,
    private formBuilder: FormBuilder,
    private router: Router) { }
  
  name!: string
  description!: string
  price!: number

  errors : any = []

  public createemployee() {
    this.isLoading = true;
    this.loadingTitle = "Creating a new employee ....";
    if (this.employeeForm.valid) { 
      
      this.employeeService.createemployee(this.employeeForm.value).subscribe({
              next: (res: any) => {
                this.router.navigate(['/employee']);
              },
              error: (err: any) => {
                this.errors = err.error.errors;
              }
      });
      
      this.isLoading = false;
    }
    else {
      this.isLoading = false;
      this.employeeForm.markAllAsTouched();
    }
    
  }
}
