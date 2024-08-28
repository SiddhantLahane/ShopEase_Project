import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegistrationService } from './registration.service';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from '../navbar/navbar.component';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule, HttpClientModule,NavbarComponent],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  registrationForm: FormGroup;

  constructor(private fb: FormBuilder,private registrationService: RegistrationService ,private router:Router) {
    // Initialize the form with FormBuilder and set up validators
    this.registrationForm = this.fb.group({
      name: ['', [Validators.required]],
      gender: ['', Validators.required],
      mobile: ['', [Validators.required]],
      address: ['', Validators.required],
      city: ['', Validators.required],
      pincode: ['', [Validators.required]],
      email: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  // Method to handle form submission
  onSubmit() {
    if (this.registrationForm.valid) {
      console.log('Form Submitted!', this.registrationForm.value);

      
  
      // Here you can add the logic to send the form data to the server
      this.registrationService.registerUser(this.registrationForm.value).subscribe(
        (response: any) => {
          console.log(response)
          // Assuming response is an object with a 'message' property
          // Adjust according to the actual structure of your response
          if (response.message === "User created successfully.") {
            console.log('Registration successful', response);
            this.router.navigateByUrl('userlogin');
          } else {
            console.log('Unexpected response', response);
          }
        },
        (error: any) => {
          console.error('Registration error', error);
        }
      );
    } else {
      this.registrationForm.markAllAsTouched(); // Trigger validation messages
    }
  }
}
