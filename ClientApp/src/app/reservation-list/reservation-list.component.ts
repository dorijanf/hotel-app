import { Component, OnInit } from '@angular/core';
import { Reservation } from '../models/reservation';
import { Observable } from 'rxjs';
import { ReservationService } from '../services/reservation.service';
import { faCaretUp, faCaretDown } from '@fortawesome/free-solid-svg-icons';
import { FormBuilder } from '@angular/forms';
import { PaginationService } from '../services/pagination.service';
import { AlertService } from '../services/alert.service';

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.scss']
})
export class ReservationListComponent implements OnInit {
  resultsForm = this.fb.group({
    statusId: [''],
    reservations: ['']
  })

  reservations$: Observable<Reservation[]>;
  pageNo: any = 1;
  pageNumber: boolean[] = [];
  orderBy: any = 'CreationDate';
  dateCreated: string;
  statusId: number;
  faCaretUp = faCaretUp;
  faCaretDown = faCaretDown;

  pageField = [];
  exactPageList: any;
  paginationData: number;
  reservationsPerPage: any = 10;
  reservationsPerPageResults = [10, 25, 50, 100];
  totalReservationsCount: any;


  constructor(private fb: FormBuilder,
              public reservationService: ReservationService,
              public paginationService: PaginationService,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.pageNumber[0] = true;
    this.loadReservations();
  }

  loadReservations() {
    this.reservations$ = this.reservationService.getManagerReservations(this.pageNo,
      this.reservationsPerPage, this.orderBy);
    this.paginationService.temppage = 0;
    this.getReservationsCount();
  }

  totalNoOfPages() {
    this.paginationData = Number(this.totalReservationsCount / this.reservationsPerPage);
    let tempPageData = this.paginationData.toFixed();
    if (Number(tempPageData) < this.paginationData) {
      this.exactPageList = Number(tempPageData) + 1;
      this.paginationService.exactPageList = this.exactPageList;
    } else {
      this.exactPageList = Number(tempPageData);
      this.paginationService.exactPageList = this.exactPageList
    }
    this.paginationService.pageOnLoad();
    this.pageField = this.paginationService.pageField;
  }

  showReservationsByPageNumber(page, i) {
    this.pageNumber = [];
    this.pageNumber[i] = true;
    this.pageNo = page;
    this.loadReservations();
  }

  getReservationsCount() {
    this.reservationService.getManagerReservationsCount(this.pageNo, this.reservationsPerPage,
      this.orderBy).subscribe((res :any) => {
        this.totalReservationsCount = res;
        this.totalNoOfPages();
      })
  }

  setOrderBy(orderBy) {
    if(this.orderBy === orderBy) {
      this.orderBy = orderBy + ' desc';
    }
    else {
      this.orderBy = orderBy;
    }
    this.loadReservations();
  }

  setNumberOfResults(){
    this.reservationsPerPage = this.resultsForm.get('reservations').value;
    this.loadReservations();
  }

  updateStatus(statusId: number, roomId: number, reservationId: number) {
    this.reservationService.updateReservationStatus(statusId, roomId, reservationId)
      .subscribe(
        data => {
        this.alertService.success('Reservation status successfully updated!', true);
        window.location.reload();
        }
    )
  }

}
