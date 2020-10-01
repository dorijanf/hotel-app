import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { City } from '../models/city';
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
  hotelcities: any[];
  error = '';
  loading = false;
  modalRef: BsModalRef;
  constructor(private hotelService: HotelService,
    private alertService: AlertService,
    private modalService: BsModalService) {
    }

  ngOnInit(): void {
    this.loadHotels();
    this.populateCities();
  }

  populateCities() {
    this.hotels$.subscribe(hotels => {
      this.hotelcities = [hotels.length]
      for(var i = 0; i < hotels.length; i++){
        this.hotelcities.push({
          id: hotels[i].id,
          name: hotels[i].name,
          contactNumber: hotels[i].contactNumber,
          email: hotels[i].email,
          address: hotels[i].address,
          statusId: hotels[i].statusId,
          cityName: this.hotelService.getHotelCityName(hotels[i].id, hotels[i].cityId)
            .pipe(
              map(city => {
              return city.name;
              })
            ),
        })
      }
    })
  }

  refresh() {
    console.log("Refreshing page...");
    this.loadHotels();
    this.populateCities();
  }

  loadHotels() {
    this.hotels$ = this.hotelService.getAllUnconfirmedHotels();
  }

  updateStatus(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(hotelId: number, statusId: number) {
    this.modalRef.hide();
    this.hotelService.updateStatus(hotelId, statusId).subscribe(
      data => {
        this.alertService.success('Hotel status successfully updated!', true);
      },
      (error: any) => {
        this.error = error;
        this.loading = false;
      }
    )
    window.location.reload();
  }

  decline() {
    this.modalRef.hide();
  }

}
