import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginService } from './adminservice.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { subscribe } from 'diagnostics_channel';
import { NavbarComponent } from "../navbar/navbar.component";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule, FormsModule,
    NavbarComponent
],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  
  userObj: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private http: HttpClient
  ) {
    this.userObj = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onLogin() {
    if (this.userObj.valid) {
      this.http.post('https://localhost:7247/api/Adminlogin/login', this.userObj.value, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        }),
        responseType: 'text'  // Expecting a text response instead of JSON
      })
        .subscribe((res: any) => {
          console.log(res);
          console.log(this.userObj.value);
          if (res === "Login successful.") {  // Check if the response is the expected string
            alert("Login Success");
            this.router.navigateByUrl('dashboard');
          } else {
            alert("Login Failed");
          }
        });
    } else {
      alert("Please fill in all fields");
    }
  }
}