import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertService } from '../services/alert.service';
import { HotelService } from '../services/hotel.service';
import { Hotel } from '../models/hotel';
import { AuthenticationService } from '../services/authentication.service';
import { User } from '../models/user';

@Component({
  selector: 'app-hotel-register',
  templateUrl: './hotel-register.component.html',
  styleUrls: ['./hotel-register.component.scss']
})
export class HotelRegisterComponent implements OnInit {
    registerHotel: FormGroup;
    @Input() hotel$: Hotel
    @Input() hotelId: number;
    cityId: number;
    cityName: string;
    buttonText: string;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    currentUser: User;

    constructor(
        private formBuilder: FormBuilder,
        private avRoute: ActivatedRoute,
        private router: Router,
        private hotelService: HotelService,
        private alertService: AlertService,
        private authenticationService: AuthenticationService
    ) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    ngOnInit() {
      this.changeMethod();
      this.returnUrl = this.avRoute.snapshot.queryParams['returnUrl'] || '/';
    }

    loadHotelCityName() {
      this.hotelService.getHotelCityName(this.hotel$.id, this.hotel$.cityId).subscribe((res) => this.cityName = res.name);
    }

    changeMethod() {
      if(this.hotel$ === undefined || this.hotel$ === null){
        this.registerHotel = this.formBuilder.group({
          name: ['', Validators.required],
          contactnumber: ['', Validators.required],
          email: ['', Validators.required],
          address: ['', Validators.required],
          city: ['', Validators.required]
        });
        this.buttonText = 'Register';
      }
      else {
        this.loadHotelCityName();
        this.registerHotel = this.formBuilder.group({
          name: [this.hotel$.name, Validators.required],
          contactnumber: [this.hotel$.contactNumber, Validators.required],
          email: [this.hotel$.email, Validators.required],
          address: [this.hotel$.address, Validators.required],
          city: [this.cityName, Validators.required]
        });
        this.buttonText = 'Edit';
      }
    }

    get f() { return this.registerHotel.controls; }

    onSubmit() {
      if(this.buttonText === 'Register'){
        this.submitted = true;
        if (this.registerHotel.invalid) {
            return;
        }

        this.loading = true;
        this.hotelService.registerHotel(this.f.name.value,
          this.f.contactnumber.value,
          this.f.email.value,
          this.f.address.value,
          this.f.city.value)
            .subscribe(
                data => {
                  let tempData = JSON.parse(JSON.stringify(data));
                  this.currentUser.isManager = true;
                  this.hotelId = tempData.entityId;
                  this.router.navigate(['/manager-panel/hotels']);
                },
                (error : any) => {
                  this.error = error;
                  this.loading = false;
                });
      }
      else {
        this.submitted = true;

        if (this.registerHotel.invalid) {
            return;
        }

        this.loading = true;
        this.hotelService.updateHotel(this.f.name.value,
          this.f.contactnumber.value,
          this.f.email.value,
          this.f.address.value,
          this.f.city.value,
          this.hotelId)
            .subscribe(
                data => {
                    this.alertService.success('Hotel updated successfully', true);
                    this.router.navigate(['/hotels/', this.hotelId]);
                },
                (error : any) => {
                    this.error = error;
                    this.loading = false;
                });
      }
    }
}
