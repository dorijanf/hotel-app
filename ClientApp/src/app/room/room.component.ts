import { Component, OnInit } from '@angular/core';
import { Room } from '../models/room';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RoomsService } from '../services/rooms.service';
import { User } from '../models/user';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {

  room$: Observable<Room>;
  roomId: number;
  currentUser: User;

  constructor(private roomsService: RoomsService,
    private avRoute: ActivatedRoute,
    private authenticationService: AuthenticationService) {
    const roomIdParam = 'roomId';
    if(this.avRoute.snapshot.params[roomIdParam]) {
      this.roomId = this.avRoute.snapshot.params[roomIdParam];
    }
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    this.loadRooms();
  }

  loadRooms() {
    this.room$ = this.roomsService.getSingleRoom(this.roomId);
  }
}
