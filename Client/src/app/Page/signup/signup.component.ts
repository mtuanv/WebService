import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  constructor(public router: Router, public http: HttpClient) {}

  ngOnInit(): void {}

  register(form: NgForm) {
    const taikhoan = {
      email: form.value.Email,
      username: form.value.Username,
      password: form.value.Password,
    };
    this.http
      .post('https://localhost:44360/api/authenticate/register', taikhoan)
      .subscribe(
        (res: any) => {
          if (res.succeeded) {
            alert('Succes');
            this.router.navigate(['/']);
            console.log(this.router);
          }
        },
        (err) => {
          console.log(err);
        }
      );
  }
}
