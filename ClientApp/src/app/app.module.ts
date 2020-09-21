import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { ReactiveFormsModule } from '@angular/forms'
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HotelsComponent } from './hotels/hotels.component';
import { RoomsComponent } from './rooms/rooms.component';
import { RoomsService } from './services/rooms.service';
import { RoomComponent } from './room/room.component';
import { Error404Component } from './errors/404.component'
import { HttpErrorInterceptor } from './http-error.interceptor';
import { PaginationService } from './services/pagination.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReservationComponent } from './reservation/reservation.component';
import { ReservationService } from './services/reservation.service';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { AlertComponent } from './alert/alert.component';
import { AlertService } from './services/alert.service';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { HotelRegisterComponent } from './hotel-register/hotel-register.component';
import { HotelComponent } from './hotel/hotel.component';
import { HotelManagerPanelComponent } from './hotel-manager-panel/hotel-manager-panel.component';
import { HotelEditComponent } from './hotel-edit/hotel-edit.component';
import { RoomRegisterComponent } from './room-register/room-register.component';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { MyReservationListComponent } from './my-reservation-list/my-reservation-list.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AdminListComponent } from './admin-list/admin-list.component';
import { AdminHotelsComponent } from "./admin-hotels/admin-hotels.component";
import { RegisterAdminComponent } from './register-admin/register-admin.component';
import { ModalModule } from 'ngx-bootstrap/modal';


export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    HotelsComponent,
    RoomsComponent,
    RoomComponent,
    Error404Component,
    ReservationComponent,
    NavbarComponent,
    RegisterComponent,
    AlertComponent,
    LoginComponent,
    HotelRegisterComponent,
    HotelComponent,
    HotelManagerPanelComponent,
    HotelEditComponent,
    RoomRegisterComponent,
    ReservationListComponent,
    MyReservationListComponent,
    AdminPanelComponent,
    AdminListComponent,
    AdminHotelsComponent,
    RegisterAdminComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [
    RoomsService,
    PaginationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    AlertService,
    ReservationService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
