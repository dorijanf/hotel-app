import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class PaginationService {
    temppage: number = 0;
    pageField = [];
    exactPageList: number;

    constructor() {
    }

    pageOnLoad() {
        if (this.temppage == 0) {
            this.pageField = [];
            for (var i = 0; i < this.exactPageList; i++) {
                this.pageField[i] = this.temppage + 1;
                this.temppage = this.temppage + 1;
            }
        }
    }

}
