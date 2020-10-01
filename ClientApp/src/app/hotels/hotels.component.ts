import { Component, OnInit } from '@angular/core';
import { HotelService } from '../services/hotel.service';
import { Hotel } from '../models/hotel';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-hotels',
  templateUrl: './hotels.component.html',
  styleUrls: ['./hotels.component.scss']
})
export class HotelsComponent implements OnInit {

  hotels$: Observable<Hotel[]>;
  hotelcities: any[];
  constructor(private hotelService: HotelService) {}

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

  loadHotelCityName(hotelId: number, cityId: number) {
    let cityName: string;
    this.hotelService.getHotelCityName(hotelId, cityId).subscribe((res) => cityName = res.name)
    return cityName;
  }

  loadHotels() {
    this.hotels$ = this.hotelService.getAllHotels();
  }




}
