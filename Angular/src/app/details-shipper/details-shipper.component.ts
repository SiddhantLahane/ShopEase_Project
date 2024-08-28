import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShipperService } from '../shipper/shipper.service';

@Component({
  selector: 'app-details-shipper',
  standalone: true,
  imports: [],
  templateUrl: './details-shipper.component.html',
  styleUrl: './details-shipper.component.css'
})
export class DetailsShipperComponent implements OnInit {
  shipper: any;

  constructor(
    private route: ActivatedRoute,
    private shipperService: ShipperService
  ) {}

  ngOnInit(): void {
    const shipperId = this.route.snapshot.params['id'];
    this.getShipperDetails(shipperId);
  }

  getShipperDetails(id: number): void {
    this.shipperService.getShipperById(id).subscribe((data) => {
      this.shipper = data;
    });
  }
}