import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HotelsComponent } from './hotels/hotels.component';
import { RoomsComponent } from './rooms/rooms.component';
import { RoomComponent } from './room/room.component';
import { Error404Component } from './errors/404.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HotelRegisterComponent } from './hotel-register/hotel-register.component';
import { HotelComponent } from './hotel/hotel.component';
import { AuthGuard } from './helpers/auth.guard';
import { HotelManagerPanelComponent } from './hotel-manager-panel/hotel-manager-panel.component';
import { HotelEditComponent } from './hotel-edit/hotel-edit.component';
import { RoomRegisterComponent } from './room-register/room-register.component';
import { AuthGuardManager } from './helpers/auth.guard.manager';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { MyReservationListComponent } from './my-reservation-list/my-reservation-list.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AdminListComponent } from './admin-list/admin-list.component';
import { AdminHotelsComponent } from "./admin-hotels/admin-hotels.component";
import { AuthGuardAdmin } from './helpers/auth.guard.admin';
import { RegisterAdminComponent } from './register-admin/register-admin.component';

const routes: Routes = [
  { path: '', component: RoomsComponent, pathMatch: 'full' },
  { path: 'rooms/:roomId', component: RoomComponent, pathMatch: 'full' },
  { path: 'admin-panel', component: AdminPanelComponent, pathMatch: 'full', canActivate: [AuthGuardAdmin]},
  { path: 'admin-panel/admins', component: AdminListComponent, pathMatch: 'full', canActivate: [AuthGuardAdmin]},
  { path: 'admin-panel/admins/register', component: RegisterAdminComponent, pathMatch: 'full', canActivate: [AuthGuardAdmin]},
  { path: 'admin-panel/hotels', component: AdminHotelsComponent, pathMatch: 'full', canActivate: [AuthGuardAdmin]},
  { path: 'my-reservations', component: MyReservationListComponent, pathMatch: 'full', canActivate: [AuthGuard]},
  { path: 'manager-panel', component: HotelManagerPanelComponent, pathMatch: 'full', canActivate: [AuthGuardManager]},
  { path: 'manager-panel/reservations', component: ReservationListComponent, pathMatch: 'full', canActivate: [AuthGuardManager]},
  { path: 'manager-panel/hotels', component: HotelsComponent, pathMatch: 'full', canActivate: [AuthGuardManager]},
  { path: 'manager-panel/hotels/:hotelId', component:HotelEditComponent, pathMatch: 'full', canActivate: [AuthGuardManager]},
  { path: 'manager-panel/hotels/:hotelId/rooms', component: RoomRegisterComponent, canActivate: [AuthGuardManager]},
  { path: 'manager-panel/hotels/:hotelId/rooms/:roomId', component: RoomRegisterComponent, canActivate: [AuthGuardManager]},
  { path: 'register/hotel', component: HotelRegisterComponent, pathMatch: 'full', canActivate: [AuthGuard]},
  { path: 'hotels/:hotelId', component: HotelComponent},
  { path: 'register', component: RegisterComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent, pathMatch: 'full'},
  { path: '404', component: Error404Component },
  { path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
