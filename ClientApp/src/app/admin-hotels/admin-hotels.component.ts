import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { Hotel } from '../models/hotel';
import { AlertService } from '../services/alert.service';
import { HotelService } from '../services/hotel.service';

@Component({
  selector: 'app-admin-hotels',
  templateUrl: './admin-hotels.component.html',
  styleUrls: ['./admin-hotels.component.scss']
})
export class AdminHotelsComponent implements OnInit {
  hotels$: Observable<Hotel[]>;
  error = '';
  loading = false;
  modalRef: BsModalRef;
  constructor(private hotelService: HotelService,
    private alertService: AlertService,
    private modalService: BsModalService) {}

  ngOnInit(): void {
    this.loadHotels();
  }

  loadHotels() {
    this.hotels$ = this.hotelService.getAllUnconfirmedHotels();
  }

  updateStatus(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(hotelId: number, statusId: number) {
    this.modalRef.hide();
    this.hotelService.updateStatus(hotelId, statusId)
    .subscribe(
      data => {
        this.alertService.success('Admin successfully deleted!', true);
        this.ngOnInit();
      },
      (error: any) => {
        this.error = error;
        this.loading = false;
      }
    )
  }

  decline() {
    this.modalRef.hide();
  }

}
