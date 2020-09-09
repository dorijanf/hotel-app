import { Component, OnInit } from '@angular/core';
import { Room } from '../models/room';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RoomsService } from '../services/rooms.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {

  room$: Observable<Room>;
  roomId: number;

  constructor(private roomsService: RoomsService,
    private avRoute: ActivatedRoute) {
    const roomIdParam = 'roomId';
    if(this.avRoute.snapshot.params[roomIdParam]) {
      this.roomId = this.avRoute.snapshot.params[roomIdParam];
    }
  }

  ngOnInit(): void {
    this.loadRooms();
  }

  loadRooms() {
    this.room$ = this.roomsService.getSingleRoom(this.roomId);
    console.log(this.room$)
  }
}
