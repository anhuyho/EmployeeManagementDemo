import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
 private token!: string;

  constructor() {
    this.generateToken();
  }

  generateToken(): void {
    this.token = 'bearer_token';
  }

  getToken(): string {
    return this.token;
  }
}
