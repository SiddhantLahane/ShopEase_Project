import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsShipperComponent } from './details-shipper.component';

describe('DetailsShipperComponent', () => {
  let component: DetailsShipperComponent;
  let fixture: ComponentFixture<DetailsShipperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetailsShipperComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailsShipperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
