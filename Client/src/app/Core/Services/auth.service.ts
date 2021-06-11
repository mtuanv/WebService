import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(public jwtHelper: JwtHelperService) {}
  public isAuthenticate(): boolean {
    if (localStorage.getItem('jwt') != null) return true;
    else {
      return false;
    }
  }
}
