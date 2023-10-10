import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from 'src/app/Services/employee.service';


function validInteger(control : any) {
  const value = control.value;
  if (value === null || value === '') {
    return null; // Allow empty input
  }
  const isValid = !isNaN(value) && Number.isInteger(Number(value));
  return isValid ? null : { invalidIntege: true };
}

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
})
export class employeeEditComponent implements OnInit{
  
  employeeId !: any;
  employee!: any;
  employeeForm!: FormGroup;
  isLoading: boolean = false;
  loadingTitle: string = 'Loading ...';
  errors: any;

  constructor(private activatedRoute: ActivatedRoute,
              private employeeService: EmployeeService,
              private formBuilder: FormBuilder,
    private router: Router) {
    
    this.employeeForm = this.formBuilder.group({
          name: ['', Validators.required],
          position: [],
          salary: ['', [Validators.required, validInteger]],
        });
  }
  ngOnInit(): void {
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('id')
    this.employeeService.getemployee(this.employeeId).subscribe(
      (res: any) => {
        this.employee = res;
        this.employeeForm.patchValue(this.employee);

      }
    );
  }

  

  updateemployee() {
    this.isLoading = true;
    this.loadingTitle = "Creating a new employee ....";
    if (this.employeeForm.valid) { 
      
      this.employeeService.updateemployee(this.employeeForm.value, this.employeeId).subscribe({
        next: (res: any) => {
          this.isLoading = false;
          this.router.navigate(['/employee']);
        },
        error: (err: any) => {
          this.errors = err.error.errors;
          console.log('err', this.errors);
          console.log('Name', this.errors.Name)
        }
      });
      
      
    }
    else {
      this.isLoading = false;
      this.employeeForm.markAllAsTouched();
    }
  }

}
