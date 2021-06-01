import { Component, OnInit } from '@angular/core';
import {AccountService} from '../../_services/account.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }


  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
