import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { environment } from '../../environments/environment';

export interface Employee{
  id: string
  name: string
  position: string
  hiringDate: number
  salary: number
}

@Injectable({ providedIn: 'root' })
  
export class EmployeeService {

  //private apiUrl = 'https://localhost:7089/api/employees';
  private apiUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }
  
  
  createemployee = (createdData: Employee) => this.httpClient.post<Employee>(`${this.apiUrl}`, createdData);

  getemployeeues = () => this.httpClient.get<Employee[]>(`${this.apiUrl}`);

  getemployee = (employeeId: string) => this.httpClient.get<Employee>(`${this.apiUrl}/${employeeId}`);

  updateemployee = (updateData: Employee, employeeId: string) => this.httpClient.put<Employee>(`${this.apiUrl}/${employeeId}`, updateData);

  deleteemployee = (employeeId: string) => this.httpClient.delete(`${this.apiUrl}/${employeeId}`);

}