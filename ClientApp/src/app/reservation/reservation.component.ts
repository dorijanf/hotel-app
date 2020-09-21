import { Component, OnInit, Injectable, ViewChild, HostListener } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertService } from '../services/alert.service';
import { BsDatepickerDirective } from 'ngx-bootstrap/datepicker';
import { ReservationService } from '../services/reservation.service';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})

@Injectable({
  providedIn: 'root'
})
export class ReservationComponent implements OnInit {
  reservationForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';
  minDateFrom: Date;
  minDateTo: Date;
  roomId: number;


  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private reservationService: ReservationService,
    private avRoute: ActivatedRoute
  ) {
    this.minDateFrom = new Date();
    this.minDateFrom.setDate(this.minDateFrom.getDate());
  }

  @ViewChild(BsDatepickerDirective, { static: false }) datepicker: BsDatepickerDirective;
  @HostListener('window:scroll')
  onScrollEvent() {
    this.datepicker.hide();
  }

  ngOnInit(): void {
    const roomIdParam = 'roomId';
    if(this.avRoute.snapshot.params[roomIdParam]) {
      this.roomId = this.avRoute.snapshot.params[roomIdParam];
    }

    this.reservationForm = this.formBuilder.group({
      dateFrom: ['', Validators.required],
      dateTo: ['', Validators.required],
      note: ['', Validators.required]
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() { return this.reservationForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.reservationForm.invalid) {
        return;
    }

    this.loading = true;
    this.reservationService.makeReservation(this.roomId, this.dateFormatter(this.f.dateFrom.value), this.dateFormatter(this.f.dateTo.value), this.f.note.value)
        .subscribe(
            data => {
                this.alertService.success('Registration successful', true);
                this.router.navigateByUrl(this.returnUrl);
            },
            (error : any) => {
                this.error = error;
                this.loading = false;
            });
  }

  dateFormatter(date: Date) {
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    var dayStr = day.toString();
    var monthStr = month.toString();

    if(day < 10) {
      dayStr = `0${day}`
    }
    if(month < 10) {
      monthStr = `0${month}`
    }
    // 2020-09-16T14:37:54.078Z
    return `${year}-${monthStr}-${dayStr}T23:59:59.099Z`;
  }

}
