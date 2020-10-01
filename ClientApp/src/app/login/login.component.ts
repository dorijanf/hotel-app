import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    statusMessage: string;

    constructor(
        private formBuilder: FormBuilder,
        private avRoute: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {}

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
        if(this.router.url === '/login') {
          this.returnUrl = '/';
        }
        else {
          this.returnUrl = this.router.url;
        }
        if(this.avRoute.snapshot.queryParams["returnUrl"] === '/register/hotel') {
          this.statusMessage = "You need to login before registering a hotel.";
        }
    }

    // convenience getter for easy access to form fields
    get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(first())
            .subscribe(
                data => {
                  this.router.navigateByUrl(this.returnUrl);
                },
                error => {
                  this.error = error;
                  this.loading = false;
                });
    }
}
