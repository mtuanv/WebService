import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean | undefined;

  constructor(private http: HttpClient, private router: Router) {}

  // login(form: NgForm) {
  //   const credentials = {
  //     username: form.value.Username,
  //     password: form.value.Password,
  //   };
  //   this.http
  //     .post('https://localhost:44360/api/authenticate/login', credentials)
  //     .subscribe(
  //       (res) => {
  //         const token = (<any>res).token;
  //         localStorage.setItem('jwt', token);
  //         this.invalidLogin = false;
  //         this.router.navigate(['/']);
  //       },
  //       (err) => {
  //         this.invalidLogin = true;
  //       }
  //     );
  // }
  ngOnInit() {}

  login(form: NgForm) {
    const taikhoan = {
      username: form.value.Username,
      password: form.value.Password,
    };
    this.http
      .post('https://localhost:44379/api/user/login', taikhoan)
      .subscribe(
        (res) => {
          const token = (<any>res).token;
          localStorage.setItem('jwt', token);
          this.invalidLogin = false;
          this.router.navigate(['']);
          console.log(token);
        },
        (err) => {
          this.invalidLogin = true;
        }
      );
    console.log(form.value);
  }
}
