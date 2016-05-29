import { Component } from 'angular2/core';
import { Router } from 'angular2/router';

@Component({
  selector: 'login.gs-com',
  templateUrl: 'pages/login.html'
})
export default class LoginComponent {
  username:string;
  password:string;
  message:string;

  constructor(private router:Router) {}

  login() {
    if (!this.username || !this.password) {
      this.message = '请填写用户名和密码进行登入';
      return;
    } else {
      this.message = '';
    }

    this.router.navigate(['App']);
  }
}
