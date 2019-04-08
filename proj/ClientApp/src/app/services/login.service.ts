import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from '../models/Login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  Login(login: Login) {
    return this.http.post<Login>(this.baseUrl + 'api/login/LoginUser', login);
  }

}
