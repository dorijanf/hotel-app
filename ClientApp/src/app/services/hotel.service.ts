import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Hotel } from '../models/hotel';
import { City } from '../models/city';
import { ignoreElements } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HotelService {

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/Hotels/'
  }

  registerHotel (name: string, contactnumber: string, email: string, address: string, cityName: string) {
    let statusId = 1;
    let cityId = 1;
    return this.http.post(this.myAppUrl + this.myApiUrl, { name, contactnumber, email, address, cityName, statusId, cityId});
  }

  getHotelById (id: number): Observable<Hotel> {
    return this.http.get<Hotel>(this.myAppUrl + this.myApiUrl + id);
  }

  getAllHotels (): Observable<Hotel[]> {
    return this.http.get<Hotel[]>(this.myAppUrl + this.myApiUrl);
  }

  getAllUnconfirmedHotels (): Observable<Hotel[]> {
    return this.http.get<Hotel[]>(this.myAppUrl + this.myApiUrl + 'pending');
  }

  updateHotel (name: string, contactnumber: string, email: string, address: string, city: string, id: number) {
    return this.http.put<Hotel>(this.myAppUrl + this.myApiUrl + id, { name, contactnumber, email, address, city});
  }

  updateStatus (hotelId: number, statusId: number) {
    return this.http.put(this.myAppUrl + this.myApiUrl + hotelId + '/status', Number(statusId));
  }

  getHotelCityName(hotelId: number, cityId: number) : Observable<City>  {
    return this.http.get<City>(this.myAppUrl + this.myApiUrl + hotelId + '/city-name/' + cityId);
  }

  getHotelNameById(id: number) {
    let hotelName: string
     this.http.get<Hotel>(this.myAppUrl + this.myApiUrl + id)
      .subscribe(res => { hotelName = res.name;
      console.log(res.name)});
      return hotelName;
  }
}
