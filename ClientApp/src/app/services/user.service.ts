import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { User } from '../models/user';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserService {
    myAppUrl: string;
    myApiUrl: string;
    httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      })
    };
    constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/Account/'
    }

    register(username: string, email: string, password: string) {
      var data = {
        userName: username,
        email: email,
        password: password
      }
      return this.http.post(this.myAppUrl + this.myApiUrl + 'register', data);
    }

    registerAdmin(username: string, email: string, password: string) {
      var data = {
        userName: username,
        email: email,
        password: password
      }
      return this.http.post(this.myAppUrl + this.myApiUrl + 'register-admin', data);
    }

    getAdmins(): Observable<User[]> {
      return this.http.get<User[]>(this.myAppUrl + this.myApiUrl);
    }

    deleteAdmin(id: string) {
      return this.http.delete(this.myAppUrl + this.myApiUrl + id);
    }
}
