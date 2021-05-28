import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {ReplaySubject} from 'rxjs';
import {User} from '../_models/User';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.baseApi;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  isLoggedIn = false;

  constructor(private http: HttpClient) {}

  login(model: any): any {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User)  => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
          this.isLoggedIn = true;
        }
      })
    );
  }

  register(model): any {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
          this.isLoggedIn = true;
        }
        return user;
      })
    );
  }

  setCurrentUser(user: User): void {
    this.currentUserSource.next(user);
  }

  logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.isLoggedIn = false;
  }

  changePassword(oldPassword: string, newPassword: string): any {
    const userId = JSON.parse(localStorage.getItem('user')).id;
    return this.http.put(this.baseUrl + 'account/change-password', {userId, oldPassword, newPassword}).pipe(
      map((response: User)  => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
          console.log(user);
        }
      })
    );
  }
}
