import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HotelsComponent } from './hotels/hotels.component';
import { RoomsComponent } from './rooms/rooms.component';
import { RoomComponent } from './room/room.component';
import { Error404Component } from './errors/404.component';

const routes: Routes = [
  { path: '', component: RoomsComponent, pathMatch: 'full'},
  { path: 'rooms/:roomId', component: RoomComponent, pathMatch: 'full'},
  { path: 'hotels', component: HotelsComponent, pathMatch: 'full'},
  { path: '404', component: Error404Component },
  { path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
