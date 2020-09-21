import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelManagerPanelComponent } from './hotel-manager-panel.component';

describe('HotelManagerPanelComponent', () => {
  let component: HotelManagerPanelComponent;
  let fixture: ComponentFixture<HotelManagerPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HotelManagerPanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HotelManagerPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
