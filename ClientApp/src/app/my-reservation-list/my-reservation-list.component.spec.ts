import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyReservationListComponent } from './my-reservation-list.component';

describe('MyReservationListComponent', () => {
  let component: MyReservationListComponent;
  let fixture: ComponentFixture<MyReservationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyReservationListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyReservationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
