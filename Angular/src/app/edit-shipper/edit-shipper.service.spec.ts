import { TestBed } from '@angular/core/testing';

import { EditShipperService } from './edit-shipper.service';

describe('EditShipperService', () => {
  let service: EditShipperService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditShipperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
