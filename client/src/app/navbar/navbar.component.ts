import {Component, OnDestroy, OnInit} from '@angular/core';
import {AccountService} from '../_services/account.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  user: any;
  loginModel: any = {};

  constructor(private accountService: AccountService) {
    this.user = this.accountService.currentUser$;
  }


  ngOnInit(): void {
    this.user.subscribe(
      next => {
        this.loginModel = next;
      }
    );
  }
}
