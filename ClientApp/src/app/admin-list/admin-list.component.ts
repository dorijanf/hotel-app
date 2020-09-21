import { Component, Injectable, OnInit, TemplateRef } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AlertService } from '../services/alert.service';
import { UserService } from '../services/user.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.scss']
})

@Injectable({
  providedIn: 'root'
})
export class AdminListComponent implements OnInit {

  admins$: Observable<User[]>;
  error = '';
  loading = false;
  modalRef: BsModalRef;

  constructor(private userService: UserService,
              private alertService: AlertService,
              private modalService: BsModalService) { }

  ngOnInit(): void {
    this.loadAdmins();
  }

  loadAdmins() {
    this.admins$ = this.userService.getAdmins();
  }

  deleteAdmin(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(id: string) {
    this.modalRef.hide();
    this.userService.deleteAdmin(id)
    .subscribe(
      data => {
        this.alertService.success('Admin successfully deleted!', true);
        this.ngOnInit();
      },
      (error: any) => {
        this.error = error;
        this.loading = false;
      }
    )
  }

  decline() {
    this.modalRef.hide();
  }

}
