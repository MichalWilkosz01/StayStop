import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UpdateAccountModel } from '../models/update-account-model';
import { NgForm } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthenticatedResponse } from '../models/authenticated-response';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {

  validationErrors: any = {};
  passwordsMatch: boolean = true;
  confirmNewPassword: string | null = null;
  credentials: UpdateAccountModel = {
    emailAddress: null,
    password: null,
    currentPassword: null,
    confirmedPassword: null,
    phoneNumber: null,
    name: null,
    lastName: null
  };

  constructor(private authService: AuthService, private http: HttpClient, private router: Router) {
    this.credentials.emailAddress = this.authService.getUserEmail();
    this.credentials.name = this.authService.getUserName();
    this.credentials.lastName = this.authService.getUserLastName();
    this.credentials.phoneNumber = this.authService.getUserPhoneNumber();
  }

  update(form: NgForm) {
    if (form.valid && this.passwordsMatch) {
      const headers = new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("accessToken")
      });
      this.http.put<AuthenticatedResponse>("http://localhost:5080/api/account/update", this.credentials, { headers })
        .subscribe({
          next: (response: AuthenticatedResponse) => { 
            const token = response.accessToken;
            const refreshToken = response.refreshToken;
            localStorage.setItem("accessToken", token); 
            localStorage.setItem("refreshToken", refreshToken);
            this.router.navigate(["/"]);
          },
          error: (err: HttpErrorResponse) => {
            this.validationErrors = err.error.errors;
            console.log(this.validationErrors);
          }
        });
    }
  }

  onPasswordChange() {
    this.passwordsMatch = this.credentials.password === this.confirmNewPassword;
  }
}
