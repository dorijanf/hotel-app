import { Component, OnInit } from '@angular/core'

@Component({
  template: `
    <h1 class="errorMessage">404'd</h1>
    <h3 class="errorExplanation">The resource you're looking for doesn't exist.</h3>
    <p class="text-center"><a routerLink="['/']">Return home</a></p>
  `,
  styles: [`
    .errorMessage {
      margin-top:150px;
      font-size: 170px;
      text-align: center;
    }
    .errorExplanation {
      font-size: 30px;
      text-align: center;
      margin-bottom: 20px;
    }`]
})
export class Error404Component implements OnInit {
  constructor() {

  }

  ngOnInit() {

  }

}
