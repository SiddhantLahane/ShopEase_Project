import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShipperService } from '../shipper/shipper.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-edit-shipper',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './edit-shipper.component.html',
  styleUrl: './edit-shipper.component.css'
})
export class EditShipperComponent implements OnInit {
  editForm: FormGroup;
  shipperId: number = 0;

  constructor(
    private fb: FormBuilder,
    private shipperService: ShipperService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.editForm = this.fb.group({
      id: [''],
      name: [''],
      contactNumber: [''],
      email: ['']
    });
  }

  ngOnInit(): void {
    this.shipperId = this.route.snapshot.params['id'];
    this.loadShipperDetails();
  }

  loadShipperDetails(): void {
    this.shipperService.getShipperById(this.shipperId).subscribe((data) => {
      this.editForm.patchValue({
        name: data.name,
        contactNumber: data.contactNumber,
        email: data.email
      });
    });
  }

  onUpdate(): void {
    if (this.editForm.valid) {
      this.shipperService.updateShipper(this.shipperId, this.editForm.value).subscribe(() => {
        this.router.navigate(['/shipper']);
      });
    }
  }

  cancelEdit(): void {
    this.router.navigate(['/shipper']);
  }
}
