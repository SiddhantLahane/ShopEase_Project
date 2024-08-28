import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';


@Component({
  selector: 'app-userlogin',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './userlogin.component.html',
  styleUrls: ['./userlogin.component.css'] // Ensure this matches the filename
})
// export class UserloginComponent {
//   userloginForm: FormGroup;

//   constructor(
//     private fb: FormBuilder,
//     private router: Router,
//     private http: HttpClient
//   ) {
//     this.userloginForm = this.fb.group({
//       username: ['', [Validators.required]],
//       password: ['', Validators.required]
//     });
//   }

//   onLogin() {
//     if (this.userloginForm.valid) {
//       this.http.post<{ message: string; token: string }>(
//         'https://localhost:7247/api/Userlogin/login',
//         this.userloginForm.value,
//         {
//           headers: new HttpHeaders({
//             'Content-Type': 'application/json'
//           })
//         }
//       ).subscribe(response => {
//         if (response.message === "Login successful.") {
//           // Store the token and login state in localStorage
//           localStorage.setItem('user', this.userloginForm.value.username);
//           localStorage.setItem('isLoggedIn', 'true');
//           localStorage.setItem('token', response.token); // Store the token
          
//           alert("Login Success");
          
//           this.router.navigateByUrl('userdashboard');
//         } else {
//           alert("Login Failed");
//         }
//       }, error => {
//         console.error('Login error', error);
//         alert("Login Failed");
//       });
//     } else {
//       alert("Please fill in all fields");
//     }
//   }

//   onLogout() {
//     this.http.post('https://localhost:7247/api/Userlogin/logout', {}, {
//       headers: new HttpHeaders({
//         'Authorization': 'Bearer ' + localStorage.getItem('token') // Include token in header
//       })
//     }).subscribe(() => {
//       // Clear session data from localStorage
//       localStorage.removeItem('username');
//       localStorage.removeItem('isLoggedIn');
//       localStorage.removeItem('token');
      
//       alert("Logout successful");
//       this.router.navigateByUrl('login');
//     }, error => {
//       console.error('Logout error', error);
//       alert("Logout Failed");
//     });
//   }
// }

export class UserloginComponent {
  userloginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private http: HttpClient
  ) {
    this.userloginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', Validators.required]
    });
  }

  onLogin() {
    if (this.userloginForm.valid) {
      this.http.post<{ message: string; token: string }>(
        'https://localhost:7247/api/Userlogin/login',
        this.userloginForm.value,
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json'
          })
        }
      ).subscribe(response => {
        if (response.message === "Login successful.") {
          // Decode the JWT token to extract the role
          const token = response.token;
          const decodedToken = this.decodeToken(token);
          const role = decodedToken.role;

          // Store the token and role in localStorage
          localStorage.setItem('user', this.userloginForm.value.username);
          localStorage.setItem('isLoggedIn', 'true');
          localStorage.setItem('token', token);
          localStorage.setItem('role', role);

          alert("Login Success");

          if (role === 'admin') {
            this.router.navigateByUrl('dashboard');
          } else {
            this.router.navigateByUrl('userdashboard');
          }
        } else {
          alert("Login Failed");
        }
      }, error => {
        console.error('Login error', error);
        alert("Login Failed");
      });
    } else {
      alert("Please fill in all fields");
    }
  }

  decodeToken(token: string) {
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  onLogout() {
    this.http.post('https://localhost:7247/api/Userlogin/logout', {}, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + localStorage.getItem('token') // Include token in header
      })
    }).subscribe(() => {
      // Clear session data from localStorage
      localStorage.removeItem('user');
      localStorage.removeItem('isLoggedIn');
      localStorage.removeItem('token');
      localStorage.removeItem('role');

      alert("Logout successful");
      this.router.navigateByUrl('login');
    }, error => {
      console.error('Logout error', error);
      alert("Logout Failed");
    });
  }
}