import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Room } from '../models/room';
import { isNullOrUndefined } from 'util';

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
      if(city !== undefined){
        searchQuery = searchQuery + '&city=' + city;
      }
      return this.http.get<Room[]>(searchQuery);
    }

    getSingleRoom(roomId: number): Observable<Room> {
      return this.http.get<Room>(this.myAppUrl + this.myApiUrl +  roomId);
    }
}
