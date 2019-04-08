import { LoginService } from './../services/login.service';
import { Component, OnInit } from '@angular/core';
import { Login } from '../models/Login';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  [x: string]: any;
  login: Login = {
    id: 0,
    userId: "",
    password: "",
    token: ""
  };

  loginForm: FormGroup;

  constructor(private loginService: LoginService,
    private router: Router,
    private formBuilder: FormBuilder, ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      userId: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {

    console.log("login", this.login)

    if (this.loginForm.invalid) {
      console.log("form is invalid", this.loginForm.controls.password.value, this.loginForm.controls.userId.value)
      return;
    }

    this.login.userId = this.loginForm.controls.userId.value;
    this.login.password = this.loginForm.controls.password.value;


    this.loginService.Login(this.login).subscribe(data => {
      console.log(data);

      if (data['success'] == true) {
        this.saveToken(data);
        this.router.navigate(['tasks']);
      }
      else {
        this.form_disable = "";
        console.log('error', data['status']);
      }

    }, err => {
      console.log('error', err);
    });

  }

  saveToken(data: Login) {
    localStorage.setItem('access_token', data['token']);
    localStorage.setItem('userId', this.login.userId);
  }

}
