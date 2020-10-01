import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertService } from '../services/alert.service';
import { Room } from '../models/room';
import { RoomsService } from '../services/rooms.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-room-register',
  templateUrl: './room-register.component.html',
  styleUrls: ['./room-register.component.scss']
})
export class RoomRegisterComponent implements OnInit {
  registerRoom: FormGroup;
  room$: Observable<Room>;
  hotelId: number;
  roomId: number;
  buttonText: string;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';
  constructor(
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router,
    private roomsService: RoomsService,
    private alertService: AlertService,
  ) {

  }

  ngOnInit() {
    this.registerRoom = this.formBuilder.group({
      name: ['', Validators.required],
      numberOfBeds: ['', Validators.required],
      price: ['', Validators.required],
      hotelId: this.hotelId
    });
    this.buttonText = 'Add';
    this.loadRoom();
    this.returnUrl = this.avRoute.snapshot.queryParams['returnUrl'] || '/';
  }

  loadRoom() {
    const hotelIdParam = 'hotelId';
    if(this.avRoute.snapshot.params[hotelIdParam]) {
      this.hotelId = this.avRoute.snapshot.params[hotelIdParam];
    }

    const roomIdParam = 'roomId';
    if(this.avRoute.snapshot.params[roomIdParam]) {
      this.roomId = this.avRoute.snapshot.params[roomIdParam];
    }

    if(this.roomId != null || this.roomId != undefined) {
      this.room$ = this.roomsService.getSingleRoom(this.roomId);
      this.room$.subscribe(room => {
        this.changeMethod(room);
      });
    }
  }

  changeMethod(room) {
    if(room != undefined || room != null) {
      this.registerRoom = this.formBuilder.group({
        name: [room.name, Validators.required],
        numberOfBeds: [room.numberOfBeds, Validators.required],
        price: [room.price, Validators.required],
        hotelId: this.hotelId
      });
      this.buttonText = 'Edit';
    }
  }

  get f() { return this.registerRoom.controls; }

  onSubmit() {
    if(this.buttonText === 'Add'){
      this.submitted = true;
      if (this.registerRoom.invalid) {
          return;
      }

      this.loading = true;
      this.roomsService.registerRoom(this.f.name.value,
          this.f.numberOfBeds.value,
          this.f.price.value,
          this.hotelId)
          .subscribe(
              data => {
                let tempData = JSON.parse(JSON.stringify(data));
                this.roomId = tempData.entityId;
                this.alertService.success('Room successfully registered!', true);
                this.router.navigate(['manager-panel/hotels/', this.hotelId]);
              },
              (error : any) => {
                this.error = error;
                this.loading = false;
              });
    }
    else {
      this.submitted = true;

      if (this.registerRoom.invalid) {
          return;
      }

      this.loading = true;
      this.roomsService.updateRoom(this.roomId,
          this.f.name.value,
          this.f.numberOfBeds.value,
          this.f.price.value,
          this.hotelId)
          .subscribe(
              data => {
                console.log(data);
                this.alertService.success('Room updated successfully', true)
              },
              (error : any) => {
                this.error = error;
                this.loading = false;
              });
        this.router.navigate(['manager-panel/hotels/', this.hotelId]);
    }
  }
}
