import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ShipperService } from './shipper.service';

import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-shipper',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule, HttpClientModule],
  templateUrl: './shipper.component.html',
  styleUrl: './shipper.component.css'
})
export class ShipperComponent implements OnInit {
  shipperForm: FormGroup;
  dataList: any[] = []; // Replace with your actual data type

  constructor(
    private fb: FormBuilder,
    private shipperService: ShipperService,
    private router: Router
  ) {
    this.shipperForm = this.fb.group({
      name: ['', Validators.required],
      contactNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]], // Assuming a 10-digit phone number
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit() {
    this.getShippers(); // Fetch the data when the component initializes
  }

  onSubmit() {
    if (this.shipperForm.valid) {
      console.log('Form Submitted!', this.shipperForm.value);
      this.shipperService.addShipper(this.shipperForm.value).subscribe(
        (response: any) => {
          console.log(response);
          if (response && response.message === "ShipperDTO added successfully.") {
            console.log('Shipper added successfully', response);
            alert("Shipper added successfully");
            this.getShippers(); // Refresh the list after adding a new shipper
            this.shipperForm.reset(); // Optionally reset the form
          } else {
            console.log('Unexpected response', response);
          }
        },
        (error: any) => {
          console.error('Error adding shipper', error);
        }
      );
    } else {
      this.shipperForm.markAllAsTouched(); // Trigger validation messages
    }
  }

  getShippers() {
    this.shipperService.getShippers().subscribe(
      (data: any[]) => {
        this.dataList = data;
        console.log('Shippers fetched successfully', data);
      },
      (error: any) => {
        console.error('Error fetching shippers', error);
      }
    );
  }

  viewDetails(id: number) {
    this.shipperService.getShipperById(id).subscribe(
      (data: any) => {
        console.log('Shipper details:', data);
        // Handle the details view logic, e.g., navigate to a details page or show in a modal
        this.router.navigate([`/details-shipper/${id}`]); // Adjust route as necessary
      },
      (error: any) => {
        console.error('Error fetching shipper details', error);
      }
    );
  }

  editItem(id: number) {
    this.router.navigate([`/edit-shipper/${id}`]); // Adjust route as necessary
  }

  deleteItem(id: number) {
    if (confirm('Are you sure you want to delete this shipper?')) {
      this.shipperService.deleteShipper(id).subscribe(
        (response: any) => {
          console.log(response);
          if (response && response.message === "ShipperDTO deleted successfully.") {
            console.log('Shipper deleted successfully', response);
            alert("Shipper deleted successfully");
            this.getShippers(); // Refresh the list after deletion
          } else {
            console.log('Unexpected response', response);
          }
        },
        (error: any) => {
          console.error('Error deleting shipper', error);
        }
      );
    }
  }
}
