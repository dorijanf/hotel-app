import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Room } from '../models/room';
import { City } from '../models/city';
import { JsonPipe } from '@angular/common';
import { stringify } from 'querystring';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/rooms/'
   }

    getRooms(pageNumber, pageSize, orderBy, numberOfBeds, city): Observable<Room[]> {
      let searchQuery = this.myAppUrl + this.myApiUrl +
        '?pagenumber=' + pageNumber + '&pageSize=' + pageSize + '&orderBy=' + orderBy;
      if(numberOfBeds != undefined || numberOfBeds != null){
        searchQuery = searchQuery + '&numberOfBeds=' + numberOfBeds;
      }
      if(city != undefined || city != null){
        searchQuery = searchQuery + '&city=' + city;
      }
      return this.http.get<Room[]>(searchQuery);
    }

    getRoomsForHotel(hotelId, pageNumber, pageSize, orderBy, numberOfBeds): Observable<Room[]> {
      let searchQuery = this.myAppUrl + 'api/' + hotelId + '/rooms' + '?pagenumber=' + pageNumber + '&pageSize='
        + pageSize + '&orderby=' + orderBy;
      if(numberOfBeds != undefined || numberOfBeds != null){
        searchQuery = searchQuery + '&numberOfBEds=' + numberOfBeds;
      }

      return this.http.get<Room[]>(searchQuery);
    }

    getRoomsCount(hotelId, pageNumber, pageSize, orderBy, numberOfBeds, city): Observable<number> {
      let searchQuery = this.myAppUrl + this.myApiUrl + 'count?'
      '&pagenumber=' + pageNumber + '&pageSize=' + pageSize + '&orderBy=' + orderBy;
      if(numberOfBeds != undefined || numberOfBeds != null){
        searchQuery = searchQuery + '&numberOfBeds=' + numberOfBeds;
      }
      if(hotelId != undefined || hotelId != null) {
        searchQuery = searchQuery + '&hotelId=' + hotelId;
      }
      if(city != undefined || city != null){
        searchQuery = searchQuery + '&city=' + city;
      }
      return this.http.get<number>(searchQuery);
    }

    getCities() : Observable<City[]> {
      return this.http.get<City[]>(this.myAppUrl + 'cities');
    }

    getSingleRoom(roomId: number): Observable<Room> {
      return this.http.get<Room>(this.myAppUrl + this.myApiUrl +  roomId);
    }

    deleteRoom(hotelId: number, roomId: number) {
      return this.http.delete(this.myAppUrl + 'api/' + hotelId + '/rooms/' + roomId);
    }

    updateRoom(roomId: number, name: string, numberOfBeds: number, price: number, hotelId: number) {
      var data = {
        "name": name,
        "numberOfBeds": numberOfBeds,
        "price": price,
        "hotelId": Number(hotelId)
      }
      return this.http.put(this.myAppUrl + 'api/' + hotelId + '/rooms/' + roomId, data);
    }

    registerRoom(name: string, numberOfBeds: number, price: number, hotelId: number) {
      var data = {
        "name": name,
        "numberOfBeds": numberOfBeds,
        "price": price,
        "hotelId": Number(hotelId)
      }
      return this.http.post(this.myAppUrl + 'api/' + hotelId + '/rooms', data);
    }
}
