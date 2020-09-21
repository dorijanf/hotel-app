import { NumberSymbol } from '@angular/common'

export class Reservation {
  id: number;
  creationDate: string;
  dateFrom: string;
  dateTo: string;
  note: string;
  roomId: number;
  reservationStatusId: number;
}
