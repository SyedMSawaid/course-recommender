import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {AccountService} from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  login(): any {
    this.accountService.login(this.model).subscribe(
      response => {
        if (JSON.parse(localStorage.getItem('user')).username === 'admin') {
          this.router.navigateByUrl('admin/dashboard');
        } else {
          this.router.navigateByUrl('/dashboard');
        }
      }, error => {
        console.error(error);
      }
    );
  }

}
