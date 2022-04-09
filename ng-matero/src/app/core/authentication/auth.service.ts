import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalStorageService } from '@shared';
import { BehaviorSubject, iif, merge, of } from 'rxjs';
import { catchError, map, share, switchMap, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class AuthService {

  baseUrl = 'https://localhost:5001/';
  constructor(public http: HttpClient
            , private store: LocalStorageService ) {}

  login(username: string, password: string, rememberMe = false) {
    return this.http.post(this.baseUrl+'auth/login',{
        username,
        password
    }).pipe(
      tap(token => this.store.set('token',token)),
    );
  }

  // refresh() {
  //   return this.loginService
  //     .refresh(filterObject({ refresh_token: this.tokenService.getRefreshToken() }))
  //     .pipe(
  //       catchError(() => of(undefined)),
  //       tap(token => this.tokenService.set(token)),
  //       map(() => this.check())
  //     );
  // }

  // logout() {
  //   return this.loginService.logout().pipe(
  //     tap(() => this.tokenService.clear()),
  //     map(() => !this.check())
  //   );
  // }
}
