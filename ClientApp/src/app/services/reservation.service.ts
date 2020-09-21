import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Reservation } from '../models/reservation';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/rooms'
  }

  makeReservation(roomId: number, dateFrom: string, dateTo: string, note: string) {
    var data = {
      dateFrom: dateFrom,
      dateTo: dateTo,
      note: note
    }
    console.log(data);
    return this.http.post(this.myAppUrl + this.myApiUrl + '/' + roomId + '/reservations', data);
  }

  getManagerReservations(pageNumber, pageSize, orderBy) : Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.myAppUrl + 'api/reservations' + '?pagenumber=' + pageNumber + '&pageSize=' + pageSize + '&orderBy=' + orderBy);
  }

  getManagerReservationsCount(pageNumber, pageSize, orderBy) : Observable<number> {
    return this.http.get<number>(this.myAppUrl + 'api/reservations' +  '/count' + '?pagenumber=' + pageNumber + '&pageSize=' + pageSize + '&orderBy=' + orderBy);
  }

  getUserReservations(pageNumber, pageSize, orderBy) : Observable<Reservation[]> {
    console.log(this.myAppUrl + 'api/user-reservations' + '?pagenumber=' + pageNumber + '&pageSize=' + pageSize + '&orderBy=' + orderBy);
    return this.http.get<Reservation[]>(this.myAppUrl + 'api/user-reservations' + '?pagenumber=' + pageNumber + '&pageSize=' + pageSize + '&orderBy=' + orderBy);
  }

  getUserReservationsCount(pageNumber, pageSize, orderBy) : Observable<number> {
    return this.http.get<number>(this.myAppUrl + 'api/user-reservations' +  '/count' + '?pagenumber=' + pageNumber + '&pageSize=' + pageSize + '&orderBy=' + orderBy);
  }

  updateReservationStatus(statusId, roomId, reservationId) {
    return this.http.put(this.myAppUrl + this.myApiUrl + '/' + roomId + '/reservations/' + reservationId, statusId);
  }
}
